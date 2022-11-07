using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw9.Dto;
using Hw9.ErrorMessages;
using Hw9.Services.MathCalculator.ExpressionParser;
using Hw9.Services.MathCalculator.ExpressionTreeBuilder;
using Hw9.Services.MathCalculator.ExpressionValidator;
using Hw9.Services.MathCalculator.GraphBuilder;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    private readonly IExpressionValidator _validator;
    private readonly IExpressionParser _parser;
    private readonly IExpressionTreeBuilder _treeBuilder;
    private readonly IGraphBuilder _graphBuilder;

    public MathCalculatorService(
        IExpressionValidator validator, 
        IExpressionParser parser,
        IGraphBuilder graphBuilder, 
        IExpressionTreeBuilder treeBuilder)
    {
        _validator = validator;
        _parser = parser;
        _graphBuilder = graphBuilder;
        _treeBuilder = treeBuilder;
    }

    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        var parseResult = _parser.ParseExpressionToTokens(expression);
        if (!parseResult.IsSuccess)
            return new CalculationMathExpressionResultDto(parseResult.ErrorMessage!);
        
        if (!_validator.TryValidateExpression(parseResult.Tokens!, out var errorMessage))
            return new CalculationMathExpressionResultDto(errorMessage);
        
        var convertedExpression = _treeBuilder.ParseTokensToExpressionTree(parseResult.Tokens!);
        var graph = _graphBuilder.BuildGraph(convertedExpression);
        var result = await CalculateAsync(convertedExpression, graph);

        return result is double.NaN ? 
            new CalculationMathExpressionResultDto(MathErrorMessager.DivisionByZero) :
            new CalculationMathExpressionResultDto(result);
    }

    private static async Task<double> CalculateAsync(Expression current,
        IReadOnlyDictionary<Expression, MathExpression> dependencies)
    {
        if (!dependencies.ContainsKey(current))
        {
            return double.Parse(current.ToString());
        }
        
        await Task.Delay(1000);
        var left = Task.Run(() => 
            CalculateAsync(dependencies[current].LeftExpression, dependencies));
        var right = Task.Run(() => 
            CalculateAsync(dependencies[current].RightExpression, dependencies));

        var results = await Task.WhenAll(left, right);
        return CalculateExpression(results[0], current.NodeType, results[1]);
    }

    [ExcludeFromCodeCoverage]
    private static double CalculateExpression(double value1, ExpressionType expressionType, double value2) =>
        expressionType switch
        {
            ExpressionType.Add => value1 + value2,
            ExpressionType.Subtract => value1 - value2,
            ExpressionType.Divide => value2 == 0 ? double.NaN : value1 / value2,
            ExpressionType.Multiply => value1 * value2,
            _ => 0
        };
}
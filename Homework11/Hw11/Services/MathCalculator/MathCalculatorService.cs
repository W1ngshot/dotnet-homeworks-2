using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw11.Dto;
using Hw11.ErrorMessages;
using Hw11.Services.MathCalculator.ExpressionParser;
using Hw11.Services.MathCalculator.ExpressionTreeBuilder;
using Hw11.Services.MathCalculator.ExpressionValidator;
using Hw11.Services.MathCalculator.GraphBuilder;

namespace Hw11.Services.MathCalculator;

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

    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        var tokens = _parser.ParseExpressionToTokens(expression);
        _validator.TryValidateExpression(tokens);
        
        var convertedExpression = _treeBuilder.ParseTokensToExpressionTree(tokens);
        var graph = _graphBuilder.BuildGraph(convertedExpression);
        var result = await CalculateAsync(convertedExpression, graph);

        return result is double.NaN ? 
            throw new DivideByZeroException(MathErrorMessager.DivisionByZero) : result;
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
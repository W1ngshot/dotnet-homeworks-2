using Hw11.Services.MathCalculator;
using Hw11.Services.MathCalculator.ExpressionParser;
using Hw11.Services.MathCalculator.ExpressionTreeBuilder;
using Hw11.Services.MathCalculator.ExpressionValidator;
using Hw11.Services.MathCalculator.GraphBuilder;

namespace Hw11.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMathCalculator(this IServiceCollection services)
    {
        return services.AddTransient<IMathCalculatorService, MathCalculatorService>()
            .AddTransient<IGraphBuilder, GraphBuilder>()
            .AddSingleton<IExpressionValidator, ExpressionValidator>()
            .AddSingleton<IExpressionTreeBuilder, ExpressionTreeBuilder>()
            .AddSingleton<IExpressionParser, ExpressionParser>();
    }
}
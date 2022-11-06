using Hw9.Services.MathCalculator;
using Hw9.Services.MathCalculator.ExpressionParser;
using Hw9.Services.MathCalculator.ExpressionTreeBuilder;
using Hw9.Services.MathCalculator.ExpressionValidator;
using Hw9.Services.MathCalculator.GraphBuilder;

namespace Hw9.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMathCalculator(this IServiceCollection services)
    {
        return services
            .AddTransient<IMathCalculatorService, MathCalculatorService>()
            .AddTransient<IGraphBuilder, GraphBuilder>()
            .AddSingleton<IExpressionValidator, ExpressionValidator>()
            .AddSingleton<IExpressionTreeBuilder, ExpressionTreeBuilder>()
            .AddSingleton<IExpressionParser, ExpressionParser>();
    }
}
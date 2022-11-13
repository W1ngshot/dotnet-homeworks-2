using Hw10.DbModels;
using Hw10.Services;
using Hw10.Services.CachedCalculator;
using Hw10.Services.MathCalculator;
using Hw10.Services.MathCalculator.ExpressionParser;
using Hw10.Services.MathCalculator.ExpressionTreeBuilder;
using Hw10.Services.MathCalculator.ExpressionValidator;
using Hw10.Services.MathCalculator.GraphBuilder;

namespace Hw10.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMathCalculator(this IServiceCollection services)
    {
        return services
            .AddTransient<MathCalculatorService>()
            .AddTransient<IGraphBuilder, GraphBuilder>()
            .AddSingleton<IExpressionValidator, ExpressionValidator>()
            .AddSingleton<IExpressionTreeBuilder, ExpressionTreeBuilder>()
            .AddSingleton<IExpressionParser, ExpressionParser>();
    }
    
    public static IServiceCollection AddCachedMathCalculator(this IServiceCollection services)
    {
        return services.AddScoped<IMathCalculatorService>(s =>
            new MathCachedCalculatorService(
                s.GetRequiredService<ApplicationContext>(), 
                s.GetRequiredService<MathCalculatorService>()));
    }
}
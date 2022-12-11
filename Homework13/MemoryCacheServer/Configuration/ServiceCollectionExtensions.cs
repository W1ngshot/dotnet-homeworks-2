using MemoryCacheServer.Services;
using MemoryCacheServer.Services.CachedCalculator;
using MemoryCacheServer.Services.MathCalculator;
using MemoryCacheServer.Services.MathCalculator.ExpressionParser;
using MemoryCacheServer.Services.MathCalculator.ExpressionTreeBuilder;
using MemoryCacheServer.Services.MathCalculator.ExpressionValidator;
using MemoryCacheServer.Services.MathCalculator.GraphBuilder;

namespace MemoryCacheServer.Configuration;

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
                s.GetRequiredService<MathCalculatorService>()));
    }
}
namespace MemoryCacheServer.Services.MathCalculator.ExpressionParser;

public interface IExpressionParser
{
    ParseResult ParseExpressionToTokens(string? expression);
}
namespace Hw9.Services.MathCalculator.ExpressionParser;

public interface IExpressionParser
{
    ParseResult ParseExpressionToTokens(string? expression);
}
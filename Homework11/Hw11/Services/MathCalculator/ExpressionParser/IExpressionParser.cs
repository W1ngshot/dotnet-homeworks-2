namespace Hw11.Services.MathCalculator.ExpressionParser;

public interface IExpressionParser
{
    List<Token> ParseExpressionToTokens(string? expression);
}
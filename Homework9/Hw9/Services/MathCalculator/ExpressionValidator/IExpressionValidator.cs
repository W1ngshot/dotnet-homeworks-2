namespace Hw9.Services.MathCalculator.ExpressionValidator;

public interface IExpressionValidator
{
    bool TryValidateExpression(List<Token> tokens, out string errorMessage);
}
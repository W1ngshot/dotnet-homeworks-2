namespace Hw11.Services.MathCalculator.ExpressionValidator;

public interface IExpressionValidator
{
    void TryValidateExpression(List<Token> tokens);
}
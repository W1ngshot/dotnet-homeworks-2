using System.Linq.Expressions;

namespace Hw11.Services.MathCalculator.ExpressionTreeBuilder;

public interface IExpressionTreeBuilder
{
    Expression ParseTokensToExpressionTree(IEnumerable<Token> tokens);
}
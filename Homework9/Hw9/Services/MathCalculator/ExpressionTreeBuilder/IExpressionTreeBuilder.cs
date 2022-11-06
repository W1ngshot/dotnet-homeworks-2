using System.Linq.Expressions;

namespace Hw9.Services.MathCalculator.ExpressionTreeBuilder;

public interface IExpressionTreeBuilder
{
    Expression ParseTokensToExpressionTree(IEnumerable<Token> tokens);
}
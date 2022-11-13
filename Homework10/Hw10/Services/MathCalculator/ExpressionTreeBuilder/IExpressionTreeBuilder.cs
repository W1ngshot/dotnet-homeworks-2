using System.Linq.Expressions;

namespace Hw10.Services.MathCalculator.ExpressionTreeBuilder;

public interface IExpressionTreeBuilder
{
    Expression ParseTokensToExpressionTree(IEnumerable<Token> tokens);
}
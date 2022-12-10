using System.Linq.Expressions;

namespace MemoryCacheServer.Services.MathCalculator.ExpressionTreeBuilder;

public interface IExpressionTreeBuilder
{
    Expression ParseTokensToExpressionTree(IEnumerable<Token> tokens);
}
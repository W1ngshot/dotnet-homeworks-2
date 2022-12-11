using System.Linq.Expressions;

namespace MemoryCacheServer.Services.MathCalculator.GraphBuilder;

public interface IGraphBuilder
{
    Dictionary<Expression, MathExpression> BuildGraph(Expression expression);
}
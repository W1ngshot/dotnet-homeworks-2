using System.Linq.Expressions;

namespace Hw11.Services.MathCalculator.GraphBuilder;

public interface IGraphBuilder
{
    Dictionary<Expression, MathExpression> BuildGraph(Expression expression);
}
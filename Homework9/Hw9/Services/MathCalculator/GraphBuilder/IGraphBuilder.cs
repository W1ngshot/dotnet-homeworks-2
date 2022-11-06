using System.Linq.Expressions;

namespace Hw9.Services.MathCalculator.GraphBuilder;

public interface IGraphBuilder
{
    Dictionary<Expression, MathExpression> BuildGraph(Expression expression);
}
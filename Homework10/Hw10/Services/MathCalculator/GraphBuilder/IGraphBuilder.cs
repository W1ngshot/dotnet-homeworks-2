using System.Linq.Expressions;

namespace Hw10.Services.MathCalculator.GraphBuilder;

public interface IGraphBuilder
{
    Dictionary<Expression, MathExpression> BuildGraph(Expression expression);
}
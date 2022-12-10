using System.Linq.Expressions;

namespace MemoryCacheServer.Services.MathCalculator.GraphBuilder;

public class GraphBuilder : ExpressionVisitor, IGraphBuilder
{
    private readonly Dictionary<Expression, MathExpression> _dependencies = new();

    public Dictionary<Expression, MathExpression> 
        BuildGraph(Expression expression)
    {
        Visit(expression);
        return _dependencies;
    }
        
    protected override Expression VisitBinary(BinaryExpression node)
    {
        _dependencies.Add(node, new MathExpression(node.Left, node.Right));
        Visit(node.Left);
        Visit(node.Right);
        return node;
    }
}
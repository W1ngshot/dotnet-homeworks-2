using System.Linq.Expressions;

namespace MemoryCacheServer.Services.MathCalculator.GraphBuilder;

public record MathExpression(Expression LeftExpression, Expression RightExpression);
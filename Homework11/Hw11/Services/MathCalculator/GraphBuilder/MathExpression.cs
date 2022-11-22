using System.Linq.Expressions;

namespace Hw11.Services.MathCalculator.GraphBuilder;

public record MathExpression(Expression LeftExpression, Expression RightExpression);
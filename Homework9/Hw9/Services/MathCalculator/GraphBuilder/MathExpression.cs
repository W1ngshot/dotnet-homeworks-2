using System.Linq.Expressions;

namespace Hw9.Services.MathCalculator.GraphBuilder;

public record MathExpression(Expression LeftExpression, Expression RightExpression);
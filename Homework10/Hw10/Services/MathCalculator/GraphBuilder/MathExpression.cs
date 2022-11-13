using System.Linq.Expressions;

namespace Hw10.Services.MathCalculator.GraphBuilder;

public record MathExpression(Expression LeftExpression, Expression RightExpression);
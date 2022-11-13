using System.Globalization;
using System.Linq.Expressions;

namespace Hw10.Services.MathCalculator.ExpressionTreeBuilder;

public class ExpressionTreeBuilder : IExpressionTreeBuilder
{
    private readonly Dictionary<string, int> _priorities = new()
    {
        {"+", 1},
        {"-", 1},
        {"*", 2},
        {"/", 2},
        {"(", 0}
    };

    private readonly Dictionary<string, ExpressionType> _expressionTypes = new()
    {
        {"+", ExpressionType.Add},
        {"-", ExpressionType.Subtract},
        {"*", ExpressionType.Multiply},
        {"/", ExpressionType.Divide}
    };
    
    public Expression ParseTokensToExpressionTree(IEnumerable<Token> tokens)
    {
        var resultStack = new Stack<Expression>();
        var currentStack = new Stack<Token>();
        var isNegativeNumber = false;
        Token? lastToken = null;
    
        foreach (var currentToken in tokens)
        {
            switch (currentToken.Type)
            {
                case TokenType.Number:
                    resultStack.Push(Expression.Constant(
                        (isNegativeNumber ? -1 : 1) * double.Parse(currentToken.Value, 
                            NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), typeof(double)));
                    isNegativeNumber = false;
                    break;
                case TokenType.Operation:
                    if (lastToken is not null && lastToken.Value.Value == "(" && currentToken.Value == "-")
                        isNegativeNumber = true;
                    else AddOperations(currentToken, resultStack, currentStack);
                    break;
                case TokenType.Bracket:
                    if (currentToken.Value == "(")
                        currentStack.Push(currentToken);
                    else AddOperationsBeforeOpenBracket(resultStack, currentStack);
                    break;
            }

            lastToken = currentToken;
        }

        AddLastOperations(resultStack, currentStack);

        return resultStack.Pop();
    }
    
    private void MakeBinaryExpression(Stack<Expression> outputStack, Token operation)
    {
        var rightNode = outputStack.Pop();
        outputStack.Push(Expression.MakeBinary(_expressionTypes[operation.Value], outputStack.Pop(),
            rightNode));
    }
    
    private void AddOperations(Token operation, Stack<Expression> outputStack, Stack<Token> operatorStack)
    {
        while (operatorStack.Count > 0 && _priorities[operatorStack.Peek().Value] >= _priorities[operation.Value])
            MakeBinaryExpression(outputStack, operatorStack.Pop());

        operatorStack.Push(operation);
    }

    private void AddOperationsBeforeOpenBracket(Stack<Expression> outputStack, Stack<Token> operatorStack)
    {
        var operation = operatorStack.Pop();
        while (operatorStack.Count > 0 && operation.Value != "(")
        {
            MakeBinaryExpression(outputStack, operation);
            operation = operatorStack.Pop();
        }
    }

    private void AddLastOperations(Stack<Expression> outputStack, Stack<Token> operatorStack)
    {
        while (operatorStack.Count > 0)
            MakeBinaryExpression(outputStack, operatorStack.Pop());
    }
}
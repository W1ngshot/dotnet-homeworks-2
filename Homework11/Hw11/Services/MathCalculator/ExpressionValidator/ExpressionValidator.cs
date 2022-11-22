using System.Diagnostics.CodeAnalysis;
using Hw11.ErrorMessages;
using Hw11.Exceptions;

namespace Hw11.Services.MathCalculator.ExpressionValidator;

public class ExpressionValidator: IExpressionValidator
{
    public void TryValidateExpression(List<Token> tokens)
    {
        if (!tokens.Any())
        {
            throw new InvalidSyntaxException(MathErrorMessager.EmptyString);
        }
        
        var soloOpenBracketsCount = 0;
        Token? lastToken = null;
        
        foreach (var currentToken in tokens)
        {
            switch (currentToken.Type)
            {
                case TokenType.Number:
                    break;
                case TokenType.Operation:
                    if (lastToken is null)
                        throw new InvalidSyntaxException(MathErrorMessager.StartingWithOperation);
                    if (lastToken is { Type: TokenType.Operation })
                        throw new InvalidSyntaxException(MathErrorMessager.TwoOperationInRowMessage(lastToken.Value.Value, currentToken.Value));
                    if (lastToken is { Value: "(" } && currentToken.Value != "-")
                        throw new InvalidSyntaxException(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(currentToken.Value));
                    break;
                case TokenType.Bracket:
                    switch (currentToken.Value)
                    {
                        case "(":
                            soloOpenBracketsCount++;
                            break;
                        case ")":
                        {
                            soloOpenBracketsCount--;
                            if (soloOpenBracketsCount < 0)
                            {
                                throw new InvalidSyntaxException(MathErrorMessager.IncorrectBracketsNumber);
                            }

                            break;
                        }
                    }

                    if (lastToken?.Type is TokenType.Operation && currentToken.Value == ")")
                        throw new InvalidSyntaxException(MathErrorMessager.OperationBeforeParenthesisMessage(lastToken.Value.Value));
                    break;
            }
            lastToken = currentToken;
        }

        CheckForEndingWithOperation(lastToken);

        if (soloOpenBracketsCount != 0)
        {
            throw new InvalidSyntaxException(MathErrorMessager.IncorrectBracketsNumber);
        }
    }

    [ExcludeFromCodeCoverage]
    private static void CheckForEndingWithOperation(Token? lastToken)
    {
        if (lastToken?.Type == TokenType.Operation)
        {
            throw new InvalidSyntaxException(MathErrorMessager.EndingWithOperation);
        }
    }
}
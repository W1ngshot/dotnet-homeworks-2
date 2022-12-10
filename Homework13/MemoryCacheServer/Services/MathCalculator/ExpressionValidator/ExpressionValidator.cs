using System.Diagnostics.CodeAnalysis;
using MemoryCacheServer.ErrorMessages;

namespace MemoryCacheServer.Services.MathCalculator.ExpressionValidator;

public class ExpressionValidator: IExpressionValidator
{
    public bool TryValidateExpression(List<Token> tokens, out string errorMessage)
    {
        if (!tokens.Any())
        {
            errorMessage = MathErrorMessager.EmptyString;
            return false;
        }
        
        var soloOpenBracketsCount = 0;
        errorMessage = "";
        Token? lastToken = null;
        
        foreach (var currentToken in tokens)
        {
            switch (currentToken.Type)
            {
                case TokenType.Number:
                    break;
                case TokenType.Operation:
                    if (lastToken is null)
                        errorMessage = MathErrorMessager.StartingWithOperation;
                    if (lastToken is { Type: TokenType.Operation })
                        errorMessage = MathErrorMessager.TwoOperationInRowMessage(lastToken.Value.Value, currentToken.Value);
                    if (lastToken is { Value: "(" } && currentToken.Value != "-")
                        errorMessage = MathErrorMessager.InvalidOperatorAfterParenthesisMessage(currentToken.Value);
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
                                errorMessage = MathErrorMessager.IncorrectBracketsNumber;
                            }

                            break;
                        }
                    }

                    if (lastToken?.Type is TokenType.Operation && currentToken.Value == ")")
                        errorMessage = MathErrorMessager.OperationBeforeParenthesisMessage(lastToken.Value.Value);
                    break;
            }

            if (!string.IsNullOrEmpty(errorMessage))
                return false;
            lastToken = currentToken;
        }

        errorMessage = CheckForEndingWithOperation(lastToken);
        if (errorMessage != "")
            return false;

        if (soloOpenBracketsCount == 0)
        {
            return true;
        }
        
        errorMessage = MathErrorMessager.IncorrectBracketsNumber;
        return false;
    }

    [ExcludeFromCodeCoverage]
    private static string CheckForEndingWithOperation(Token? lastToken) => 
        lastToken?.Type == TokenType.Operation ? MathErrorMessager.EndingWithOperation : "";
}
using System.Globalization;
using Hw11.ErrorMessages;
using Hw11.Exceptions;

namespace Hw11.Services.MathCalculator.ExpressionParser;

public class ExpressionParser: IExpressionParser
{
    private readonly HashSet<char> _brackets = new() {'(', ')'};
    private readonly HashSet<char>  _operations = new() {'+', '-', '/', '*'};

    public List<Token> ParseExpressionToTokens(string? expression)
    {
        if (string.IsNullOrEmpty(expression))
            return new List<Token>();
        
        var tokens = new List<Token>();
        var number = "";
        
        foreach (var symbol in expression.Replace(" ", ""))
        {
            if (_brackets.Contains(symbol))
            {
                TryAddToken(ref number, tokens, symbol, TokenType.Bracket);
            }
            else if (_operations.Contains(symbol))
            {
                if (!TryAddToken(ref number, tokens, symbol, TokenType.Operation))
                    throw new InvalidNumberException(MathErrorMessager.NotNumberMessage(number));
            }
            else if (char.IsDigit(symbol) || symbol == '.')
                number += symbol;
            else
                throw new InvalidSymbolException(MathErrorMessager.UnknownCharacterMessage(symbol));
        }

        if (!string.IsNullOrEmpty(number))
        {
            if (!double.TryParse(number, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                throw new InvalidNumberException(MathErrorMessager.NotNumberMessage(number));
            
            tokens.Add(new Token(TokenType.Number, number));
        }

        return tokens;
    }

    private static bool TryAddToken(ref string num, ICollection<Token> tokens, char tokenValue, TokenType tokenType)
    {
        if (!string.IsNullOrEmpty(num))
        {
            if (!double.TryParse(num, NumberStyles.AllowDecimalPoint,  CultureInfo.InvariantCulture, out _))
                return false;
            tokens.Add(new Token(TokenType.Number, num));
            num = "";
        }

        tokens.Add(new Token(tokenType, tokenValue.ToString()));
        return true;
    }
}
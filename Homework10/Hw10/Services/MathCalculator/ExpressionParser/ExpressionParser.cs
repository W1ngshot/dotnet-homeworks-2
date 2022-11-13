using System.Globalization;
using Hw10.ErrorMessages;

namespace Hw10.Services.MathCalculator.ExpressionParser;

public class ExpressionParser: IExpressionParser
{
    private readonly HashSet<char> _brackets = new() {'(', ')'};
    private readonly HashSet<char>  _operations = new() {'+', '-', '/', '*'};

    public ParseResult ParseExpressionToTokens(string? expression)
    {
        if (string.IsNullOrEmpty(expression))
            return new ParseResult(new List<Token>());
        
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
                    return new ParseResult(MathErrorMessager.NotNumberMessage(number));
            }
            else if (char.IsDigit(symbol) || symbol == '.')
                number += symbol;
            else
                return new ParseResult(MathErrorMessager.UnknownCharacterMessage(symbol));
        }

        if (!string.IsNullOrEmpty(number))
        {
            if (!double.TryParse(number, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                return new ParseResult(MathErrorMessager.NotNumberMessage(number));
            
            tokens.Add(new Token(TokenType.Number, number));
        }
        
        return new ParseResult(tokens);
    }

    private bool TryAddToken(ref string num, ICollection<Token> tokens, char tokenValue, TokenType tokenType)
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
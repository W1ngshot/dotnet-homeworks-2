namespace Hw10.Services.MathCalculator;

public readonly struct Token
{
    public readonly TokenType Type;
    public readonly string Value;

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }
}

public enum TokenType : byte
{
    Bracket,
    Operation,
    Number
}
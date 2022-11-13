namespace Hw10.Services.MathCalculator.ExpressionParser;

public class ParseResult
{
    public ParseResult(List<Token>? tokens)
    {
        IsSuccess = true;
        Tokens = tokens; 
    }
    
    public ParseResult(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
    
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public List<Token>? Tokens { get; }
}
using MemoryCacheServer.Dto;

namespace MemoryCacheServer.Services;

public interface IMathCalculatorService
{
    public Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression);
}
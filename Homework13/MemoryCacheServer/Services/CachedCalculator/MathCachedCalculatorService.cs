using MemoryCacheServer.Dto;

namespace MemoryCacheServer.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private static readonly Lazy<Dictionary<string, double>> CalculationCacheLazy = new (() => new Dictionary<string, double>());
	private static readonly Dictionary<string, double> CalculationCache = CalculationCacheLazy.Value;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(IMathCalculatorService simpleCalculator)
	{
		_simpleCalculator = simpleCalculator;
	}

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		var filteredExpression = expression?.Replace(" ", "");
		var resultFromCache = GetResultFromCache(filteredExpression);
		if (resultFromCache is not null)
		{
			await Task.Delay(1000);
			return new CalculationMathExpressionResultDto((double) resultFromCache);
		}

		var resultDto = await _simpleCalculator.CalculateMathExpressionAsync(expression);

		if (!resultDto.IsSuccess)
			return resultDto;

		CalculationCache.Add(expression!, resultDto.Result);
		return new CalculationMathExpressionResultDto(resultDto.Result);
	}

	private double? GetResultFromCache(string? expression)
	{
		if (expression == null || !CalculationCache.ContainsKey(expression))
			return null;
		return CalculationCache[expression];
	}
}
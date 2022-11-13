using Hw10.DbModels;
using Hw10.Dto;
using Microsoft.EntityFrameworkCore;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly ApplicationContext _dbContext;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(ApplicationContext dbContext, IMathCalculatorService simpleCalculator)
	{
		_dbContext = dbContext;
		_simpleCalculator = simpleCalculator;
	}

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		var filteredExpression = expression?.Replace(" ", "");
		var resultFromDb = await GetResultFromDb(filteredExpression);
		if (resultFromDb is not null)
		{
			await Task.Delay(1000);
			return new CalculationMathExpressionResultDto((double) resultFromDb);
		}

		var resultDto = await _simpleCalculator.CalculateMathExpressionAsync(expression);

		if (!resultDto.IsSuccess)
			return resultDto;

		_dbContext.Add(new SolvingExpression
		{
			Expression = filteredExpression!,
			Result = resultDto.Result
		});
		await _dbContext.SaveChangesAsync();
		return new CalculationMathExpressionResultDto(resultDto.Result);
	}

	private async Task<double?> GetResultFromDb(string? expression)
	{
		var solvedExpression = await _dbContext.SolvingExpressions
			.FirstOrDefaultAsync(exp => exp.Expression == expression);
		return solvedExpression?.Result;
	}
}
using System.Diagnostics.CodeAnalysis;
using MemoryCacheServer.Dto;
using MemoryCacheServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace MemoryCacheServer.Controllers;

public class CalculatorController : Controller
{
    private readonly IMathCalculatorService _mathCalculatorService;

    public CalculatorController(IMathCalculatorService mathCalculatorService)
    {
        _mathCalculatorService = mathCalculatorService;
    }
        
    [HttpGet]
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<CalculationMathExpressionResultDto>> CalculateMathExpression(string expression)
    {
        var result = await _mathCalculatorService.CalculateMathExpressionAsync(expression);
        return Json(result);
    }
}
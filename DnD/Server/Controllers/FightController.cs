using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FightController : ControllerBase
{
    private readonly IFightService _fightService;

    public FightController(IFightService fightService)
    {
        _fightService = fightService;
    }

    [HttpPost("Calculate")]
    public FightResult Calculate(FightingCharacters characters)
    {
        return _fightService.CalculateFightResult(characters.Player, characters.Monster);
    }
}
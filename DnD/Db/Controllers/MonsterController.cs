using Db.Models;
using Db.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Db.Controllers;

[ApiController]
[Route("[controller]")]
public class MonsterController : ControllerBase
{
    private readonly IMonsterRepository _monsterRepository;
    
    public MonsterController(IMonsterRepository monsterRepository)
    {
        _monsterRepository = monsterRepository;
    }

    [HttpGet("Random")]
    public async Task<Monster> GetRandomMonster()
    {
        return await _monsterRepository.GetRandomAsync();
    }
}
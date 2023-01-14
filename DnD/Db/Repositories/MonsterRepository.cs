using System.Data;
using Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Db.Repositories;

public class MonsterRepository : IMonsterRepository
{
    private readonly Context _context;

    public MonsterRepository(Context context)
    {
        _context = context;
    }

    public async Task<Monster> GetByIdAsync(int id) =>
        await _context.Monsters.FirstOrDefaultAsync(monster => monster.Id == id) ??
        throw new ArgumentException("Wrong monster id");

    public async Task<Monster> GetRandomAsync()
    {
        var count = _context.Monsters.Count();

        if (count == 0) 
            throw new DataException("No monsters in db!");

        var rand = new Random();
        var randomAmountToSkip = rand.Next(0, count);
        return await _context.Monsters.Skip(randomAmountToSkip).FirstAsync();
    }
}
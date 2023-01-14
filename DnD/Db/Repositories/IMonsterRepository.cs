using Db.Models;

namespace Db.Repositories;

public interface IMonsterRepository
{
    public Task<Monster> GetByIdAsync(int id);

    public Task<Monster> GetRandomAsync();
}
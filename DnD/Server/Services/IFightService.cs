using Server.Models;

namespace Server.Services;

public interface IFightService
{
    public FightResult CalculateFightResult(Character player, Character monster);
}
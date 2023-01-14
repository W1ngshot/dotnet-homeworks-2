using Db.Models;

namespace Db;

public static class MonsterService
{
    public static IEnumerable<Monster> GetAll() =>
        new List<Monster>
        {
            new()
            {
                Id = 1,
                Name = "Wolf",
                Health = 50,
                AttackModifier = 4,
                RollsCount = 2,
                DiceSize = 8,
                DamageModifier = 2,
                Armor = 13,
                AttacksPerRound = 1
            },
            new()
            {
                Id = 2,
                Name = "Sea elf",
                Health = 50,
                AttackModifier = 2,
                RollsCount = 2,
                DiceSize = 8,
                DamageModifier = 2,
                Armor = 11,
                AttacksPerRound = 1
            },
            new()
            {
                Id = 3,
                Name = "Hyena",
                Health = 35,
                AttackModifier = 2,
                RollsCount = 1,
                DiceSize = 8,
                DamageModifier = 1,
                Armor = 11,
                AttacksPerRound = 1
            },
            new()
            {
                Id = 4,
                Name = "Gnoll",
                Health = 70,
                AttackModifier = 4,
                RollsCount = 5,
                DiceSize = 8,
                DamageModifier = 0,
                Armor = 15,
                AttacksPerRound = 1
            },
            new()
            {
                Id = 5,
                Name = "Flying sword",
                Health = 60,
                AttackModifier = 3,
                RollsCount = 5,
                DiceSize = 6,
                DamageModifier = 0,
                Armor = 17,
                AttacksPerRound = 1
            },
            new()
            {
                Id = 6,
                Name = "Orond Gralhund",
                Health = 50,
                AttackModifier = 3,
                RollsCount = 2,
                DiceSize = 8,
                DamageModifier = 0,
                Armor = 15,
                AttacksPerRound = 1
            }
        };
}
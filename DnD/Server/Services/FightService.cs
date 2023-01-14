using Server.Models;

namespace Server.Services;

public class FightService : IFightService
{
    private readonly Dice _mainDice = new Dice(20);

    public FightResult CalculateFightResult(Character player, Character monster)
    {
        var playerDice = new Dice(player.DiceSize);
        var monsterDice = new Dice(monster.DiceSize);
        var roundNumber = 1;
        var rounds = new List<Round>();

        while (player.Health > 0 && monster.Health > 0)
        {
            var hits = new List<Hit>();
            for (var i = 0; i < Math.Max(player.AttacksPerRound, monster.AttacksPerRound); i++)
            {
                if (monster.Health > 0 && player.Health > 0 && player.AttacksPerRound > i)
                    Attack(player, monster, hits, playerDice, roundNumber);
                if (monster.Health > 0 && player.Health > 0 && monster.AttacksPerRound > i)
                    Attack(monster, player, hits, monsterDice, roundNumber);
            }

            rounds.Add(new Round
            {
                Number = roundNumber,
                Hits = hits
            });
            roundNumber++;
        }

        return new FightResult
        {
            Rounds = rounds,
            IsPlayerWin = player.Health > 0,
            Monster = monster,
            Player = player
        };
    }

    private void Attack(Character attacker, Character target, List<Hit> hits, Dice attackerDice, int roundNum)
    {
        var roll = _mainDice.Roll();
        var isHit = roll != 1 && (roll == 20 || roll + attacker.AttackModifier > target.Armor);
        var damage = 0;

        var hit = new Hit
        {
            AttackerName = attacker.Name,
            TargetName = target.Name,
            AttackerDamageModifier = attacker.DamageModifier,
            AttackerAttackModifier = attacker.AttackModifier,
            TargetArmor = target.Armor,
            DiceRoll = roll,
            IsCritical = false,
            IsHit = false
        };
        
        if (isHit)
        {
            hit.IsHit = true;
            var playerRolls = new List<int>();
            damage = CountDamage(attacker, attackerDice, playerRolls);
            
            if (roll == 20)
            {
                damage *= 2;
                hit.IsCritical = true;
            }

            hit.PlayerDiceRolls = playerRolls;
            target.Health = target.Health - damage < 0 ? 0 : target.Health - damage;
        }

        hit.TargetRemainingHealth = target.Health;
        hit.TotalDamage = damage;
        hits.Add(hit);
    }

    private int CountDamage(Character attacker, Dice attackerDice, List<int> playerRolls)
    {
        var damage = attacker.DamageModifier;
        for (var i = 0; i < attacker.RollsCount; i++)
        {
            var playerRoll = attackerDice.Roll();
            playerRolls.Add(playerRoll);
            damage += playerRoll;
        }

        return damage;
    }
}
namespace Server.Models;

public class Character
{
    public string Name { get; set; } = null!;
    
    public int Health { get; set; }
    
    public int AttackModifier { get; set; }
    
    public int AttacksPerRound { get; set; }

    public int RollsCount { get; set; }

    public int DiceSize { get; set; }

    public int DamageModifier { get; set; }

    public int Armor { get; set; }
}
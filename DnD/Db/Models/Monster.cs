using System.ComponentModel.DataAnnotations;

namespace Db.Models;

public class Monster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
        
    [Range(1, 100)]
    public int Health { get; set; }
        
    [Range(1, 100)]
    public int AttackModifier { get; set; }

    [Range(1, 10)]
    public int AttacksPerRound { get; set; }
    
    [Range(1, 100)]
    public int RollsCount { get; set; }
    
    [Range(1, 100)]
    public int DiceSize { get; set; }
    
    [Range(1, 100)]
    public int DamageModifier { get; set; }
        
    [Range(1, 100)]
    public int Armor { get; set; }
}
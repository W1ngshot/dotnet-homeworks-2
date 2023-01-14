using System.ComponentModel.DataAnnotations;

namespace UI.Models;

public class Character
{
    [Required]
    [MaxLength(25)]
    public string Name { get; set; }
    
    [Required] 
    [Range(1, 100)]
    public int Health { get; set; }
    
    [Required] 
    [Range(0, 100)]
    public int AttackModifier { get; set; }
    
    [Required] 
    [Range(0, 100)]
    public int RollsCount { get; set; }
    
    [Required]
    [Range(0, 10)]
    public int AttacksPerRound { get; set; }
    
    [Required]
    [Range(1, 20)]
    public int DiceSize { get; set; }
    
    [Required]
    [Range(0, 100)]
    public int DamageModifier { get; set; }
    
    [Required]
    [Range(0, 100)]
    public int Armor { get; set; }
}
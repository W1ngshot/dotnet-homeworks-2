namespace UI.Models;

public class Hit
{
    public string AttackerName { get; set; } = null!;
    public string TargetName { get; set; } = null!;
    public int AttackerDamageModifier { get; set; }
    public int AttackerAttackModifier { get; set; }
    public int TargetArmor { get; set; }
    public int DiceRoll { get; set; }
    public List<int> PlayerDiceRolls { get; set; } = new();
    public int TotalDamage { get; set; }
    public int TargetRemainingHealth { get; set; }
    public bool IsCritical { get; set; }
    public bool IsHit { get; set; }
}
namespace UI.Models;

public class FightResult
{
    public bool IsPlayerWin { get; set; }

    public List<Round> Rounds { get; set; } = new();

    public Character Player { get; set; } = null!;

    public Character Monster { get; set; } = null!;
}
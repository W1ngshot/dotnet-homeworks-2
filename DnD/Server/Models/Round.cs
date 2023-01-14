namespace Server.Models;

public class Round
{
    public int Number { get; set; }

    public List<Hit> Hits { get; set; } = null!;
}
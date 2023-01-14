namespace Server;

public class Dice
{
    private int Size { get; }
    private readonly Random _random = new();

    public Dice(int size)
    {
        Size = size;
    }

    public int Roll() => _random.Next(1, Size + 1);
}
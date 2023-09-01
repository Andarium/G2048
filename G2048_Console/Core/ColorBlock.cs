namespace G2048_Console;

public sealed class ColorBlock : IDisposable
{
    public ColorBlock(ConsoleColor color)
    {
        Console.ForegroundColor = color;
    }

    public void Dispose()
    {
        Console.ResetColor();
    }
}
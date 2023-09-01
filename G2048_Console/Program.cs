using G2048;

namespace G2048_Console;

public abstract class Program
{
    private static void Main()
    {
        var random = new ConsoleRandom();
        var input = new ConsoleGameInput();
        var output = new ConsoleGameOutput(Game.BoardSize);
        var score = new LocalScore();
        var game = new Game(input, output, score, random);

        while (true)
        {
            game.Update();
        }
    }
}
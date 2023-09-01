using G2048;

namespace G2048_Console;

public sealed class ConsoleGameInput : IGameInput
{
    public GameCommandType ReadCommand()
    {
        var key = Console.ReadKey(true);

        if (CommandsByKey.TryGetValue(key.Key, out var command))
        {
            return command;
        }

        return GameCommandType.None;
    }

    private static readonly List<CommandBinding> Commands = new()
    {
        new(GameCommandType.Quit, ConsoleKey.Q),
        new(GameCommandType.MoveLeft, ConsoleKey.A, ConsoleKey.LeftArrow),
        new(GameCommandType.MoveRight, ConsoleKey.D, ConsoleKey.RightArrow),
        new(GameCommandType.MoveUp, ConsoleKey.W, ConsoleKey.UpArrow),
        new(GameCommandType.MoveDown, ConsoleKey.S, ConsoleKey.DownArrow),
        new(GameCommandType.Restart, ConsoleKey.R),
        new(GameCommandType.Confirm, ConsoleKey.Y),
        new(GameCommandType.Cancel, ConsoleKey.N)
    };

    private static readonly Dictionary<ConsoleKey, GameCommandType> CommandsByKey =
        Commands
            .SelectMany(x => x.Keys.Select(y => new { Key = y, Command = x.Type }))
            .ToDictionary(x => x.Key, y => y.Command);
}
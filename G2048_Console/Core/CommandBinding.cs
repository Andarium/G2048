using G2048;

namespace G2048_Console;

public sealed class CommandBinding
{
    public readonly GameCommandType Type;
    public readonly IReadOnlyList<ConsoleKey> Keys;

    public CommandBinding(GameCommandType type, params ConsoleKey[] keys)
    {
        Type = type;
        Keys = keys
            .Distinct()
            .OrderBy(x => x)
            .ToList();
    }
}
using G2048;

namespace G2048_Console;

public sealed class ConsoleRandom : IRandom
{
    private readonly Random _random;

    public ConsoleRandom(int seed) => _random = new Random(seed);

    public ConsoleRandom() => _random = new Random();

    public int Next(int maxExclusive) => _random.Next(maxExclusive);
}
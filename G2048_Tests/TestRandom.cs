using G2048;

namespace G2048_Tests;

public sealed class TestRandom : IRandom
{
    private readonly Random _random = new(0);

    public int Next(int maxExclusive) => _random.Next(maxExclusive);
}
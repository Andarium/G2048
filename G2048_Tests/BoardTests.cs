using G2048;

namespace G2048_Tests;

public sealed class BoardTests
{
    private readonly TestRandom _random = new();
    private Board _board = default!;

    [SetUp]
    public void Setup()
    {
        _board = new Board(_random);
    }

    [Test]
    public void Merge_0()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 2),
            new Tile(0, 1, 2),
            new Tile(0, 2, 2),
            new Tile(0, 3, 2)
        );

        var result = _board.Move(1, Direction.Right, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(true));
        Assert.That(output[0, 0], Is.EqualTo(0));
        Assert.That(output[0, 1], Is.EqualTo(0));
        Assert.That(output[0, 2], Is.EqualTo(4));
        Assert.That(output[0, 3], Is.EqualTo(4));
        Assert.That(score, Is.EqualTo(8));
    }

    [Test]
    public void Merge_0_Reverse()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 2),
            new Tile(0, 1, 2),
            new Tile(0, 2, 2),
            new Tile(0, 3, 2)
        );

        var result = _board.Move(1, Direction.Left, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(true));
        Assert.That(output[0, 0], Is.EqualTo(4));
        Assert.That(output[0, 1], Is.EqualTo(4));
        Assert.That(output[0, 2], Is.EqualTo(0));
        Assert.That(output[0, 3], Is.EqualTo(0));
        Assert.That(score, Is.EqualTo(8));
    }

    [Test]
    public void Merge_0_Reverse2()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 4),
            new Tile(0, 1, 16),
            new Tile(0, 2, 16),
            new Tile(0, 3, 2)
        );

        var result = _board.Move(1, Direction.Left, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(true));
        Assert.That(output[0, 0], Is.EqualTo(4));
        Assert.That(output[0, 1], Is.EqualTo(32));
        Assert.That(output[0, 2], Is.EqualTo(2));
        Assert.That(output[0, 3], Is.EqualTo(0));
        Assert.That(score, Is.EqualTo(32));
    }

    [Test]
    public void Merge_0_Reverse3()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(2, 0, 4),
            new Tile(2, 1, 16),
            new Tile(2, 2, 8),
            new Tile(2, 3, 8)
        );

        var result = _board.Move(1, Direction.Left, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(true));
        Assert.That(output[2, 0], Is.EqualTo(4));
        Assert.That(output[2, 1], Is.EqualTo(16));
        Assert.That(output[2, 2], Is.EqualTo(16));
        Assert.That(output[2, 3], Is.EqualTo(0));
        Assert.That(score, Is.EqualTo(16));
    }

    [Test]
    public void Merge_0_Reverse4()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(2, 0, 32),
            new Tile(2, 1, 2),
            new Tile(2, 2, 2),
            new Tile(2, 3, 4)
        );

        var result = _board.Move(1, Direction.Left, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(true));
        Assert.That(output[2, 0], Is.EqualTo(32));
        Assert.That(output[2, 1], Is.EqualTo(4));
        Assert.That(output[2, 2], Is.EqualTo(4));
        Assert.That(output[2, 3], Is.EqualTo(0));
        Assert.That(score, Is.EqualTo(4));
    }

    [Test]
    public void Merge_1()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 2),
            new Tile(0, 1, 0),
            new Tile(0, 2, 2),
            new Tile(0, 3, 2)
        );

        var result = _board.Move(1, Direction.Right, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(true));
        Assert.That(output[0, 0], Is.EqualTo(0));
        Assert.That(output[0, 1], Is.EqualTo(0));
        Assert.That(output[0, 2], Is.EqualTo(2));
        Assert.That(output[0, 3], Is.EqualTo(4));
        Assert.That(score, Is.EqualTo(4));
    }

    [Test]
    public void Merge_2()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 256),
            new Tile(0, 1, 0),
            new Tile(0, 2, 2),
            new Tile(0, 3, 2)
        );

        var result = _board.Move(1, Direction.Right, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(true));
        Assert.That(output[0, 0], Is.EqualTo(0));
        Assert.That(output[0, 1], Is.EqualTo(0));
        Assert.That(output[0, 2], Is.EqualTo(256));
        Assert.That(output[0, 3], Is.EqualTo(4));
        Assert.That(score, Is.EqualTo(4));
    }

    [Test]
    public void NoMerge_0()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 2),
            new Tile(0, 1, 2),
            new Tile(0, 2, 2),
            new Tile(0, 3, 2)
        );

        var result = _board.Move(0, Direction.Right, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(false));
        Assert.That(output[0, 0], Is.EqualTo(2));
        Assert.That(output[0, 1], Is.EqualTo(2));
        Assert.That(output[0, 2], Is.EqualTo(2));
        Assert.That(output[0, 3], Is.EqualTo(2));
        Assert.That(score, Is.EqualTo(0));
    }

    [Test]
    public void NoMerge_1()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 2, 1),
            new Tile(0, 1, 2, 2),
            new Tile(0, 2, 2, 3),
            new Tile(0, 3, 2, 4)
        );

        var result = _board.Move(0, Direction.Right, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(false));
        Assert.That(output[0, 0], Is.EqualTo(2));
        Assert.That(output[0, 1], Is.EqualTo(2));
        Assert.That(output[0, 2], Is.EqualTo(2));
        Assert.That(output[0, 3], Is.EqualTo(2));
        Assert.That(score, Is.EqualTo(0));
    }

    [Test]
    public void NoMerge_2()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 2),
            new Tile(0, 1, 8),
            new Tile(0, 2, 2),
            new Tile(0, 3, 256)
        );

        var result = _board.Move(0, Direction.Right, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(false));
        Assert.That(output[0, 0], Is.EqualTo(2));
        Assert.That(output[0, 1], Is.EqualTo(8));
        Assert.That(output[0, 2], Is.EqualTo(2));
        Assert.That(output[0, 3], Is.EqualTo(256));
        Assert.That(score, Is.EqualTo(0));
    }

    [Test]
    public void Merge_100()
    {
        _board.Clear();
        _board.SetTiles(
            new Tile(0, 0, 2),
            new Tile(0, 1, 0),
            new Tile(0, 2, 0),
            new Tile(0, 3, 2),
            new Tile(1, 0, 0),
            new Tile(1, 1, 0),
            new Tile(1, 2, 4),
            new Tile(1, 3, 2),
            new Tile(2, 0, 0),
            new Tile(2, 1, 16),
            new Tile(2, 2, 2),
            new Tile(2, 3, 16),
            new Tile(3, 0, 0),
            new Tile(3, 1, 2),
            new Tile(3, 2, 8),
            new Tile(3, 3, 4)
        );

        var result = _board.Move(1, Direction.Up, out var score);
        var output = _board.GetGridOutput();

        Assert.That(result, Is.EqualTo(true));
        Assert.That(output[0, 0], Is.EqualTo(2));
        Assert.That(output[0, 1], Is.EqualTo(16));
        Assert.That(output[0, 2], Is.EqualTo(4));
        Assert.That(output[0, 3], Is.EqualTo(4));

        Assert.That(output[1, 0], Is.EqualTo(0));
        Assert.That(output[1, 1], Is.EqualTo(2));
        Assert.That(output[1, 2], Is.EqualTo(2));
        Assert.That(output[1, 3], Is.EqualTo(16));

        Assert.That(output[2, 0], Is.EqualTo(0));
        Assert.That(output[2, 1], Is.EqualTo(0));
        Assert.That(output[2, 2], Is.EqualTo(8));
        Assert.That(output[2, 3], Is.EqualTo(4));

        Assert.That(output[3, 0], Is.EqualTo(0));
        Assert.That(output[3, 1], Is.EqualTo(0));
        Assert.That(output[3, 2], Is.EqualTo(0));
        Assert.That(output[3, 3], Is.EqualTo(0));
        Assert.That(score, Is.EqualTo(4));
    }
}
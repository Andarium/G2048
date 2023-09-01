using G2048;

namespace G2048_Tests;

public sealed class ReadOnlyLineTests
{
    private int[,] _array = default!;

    [SetUp]
    public void Setup()
    {
        _array = new[,]
        {
            { 0, 1, 2, 3, 4 },
            { 5, 6, 7, 8, 9 },
            { 10, 11, 12, 13, 14 },
            { 15, 16, 17, 18, 19 },
            { 20, 21, 22, 23, 24 }
        };
    }

    [Test]
    public void RowView_Full_0()
    {
        var line = _array.GetRow(0);
        Assert.That(line[0], Is.EqualTo(0));
        Assert.That(line[1], Is.EqualTo(1));
        Assert.That(line[2], Is.EqualTo(2));
        Assert.That(line[3], Is.EqualTo(3));
        Assert.That(line[4], Is.EqualTo(4));
        Assert.That(line.Length, Is.EqualTo(5));
    }

    [Test]
    public void RowView_Full_1()
    {
        var line = _array.GetRow(2);
        Assert.That(line[0], Is.EqualTo(10));
        Assert.That(line[1], Is.EqualTo(11));
        Assert.That(line[2], Is.EqualTo(12));
        Assert.That(line[3], Is.EqualTo(13));
        Assert.That(line[4], Is.EqualTo(14));
        Assert.That(line.Length, Is.EqualTo(5));
    }

    [Test]
    public void RowView_Full_0_Reverse()
    {
        var line = _array.GetRow(0, reversed: true);
        Assert.That(line[0], Is.EqualTo(4));
        Assert.That(line[1], Is.EqualTo(3));
        Assert.That(line[2], Is.EqualTo(2));
        Assert.That(line[3], Is.EqualTo(1));
        Assert.That(line[4], Is.EqualTo(0));
        Assert.That(line.Length, Is.EqualTo(5));
    }

    [Test]
    public void RowView_Full_1_Reverse()
    {
        var line = _array.GetRow(3, reversed: true);
        Assert.That(line[0], Is.EqualTo(19));
        Assert.That(line[1], Is.EqualTo(18));
        Assert.That(line[2], Is.EqualTo(17));
        Assert.That(line[3], Is.EqualTo(16));
        Assert.That(line[4], Is.EqualTo(15));
        Assert.That(line.Length, Is.EqualTo(5));
    }

    [Test]
    public void RowView_Sub_0()
    {
        var line = _array.GetRow(0, xOffset: 2, length: 2);
        Assert.That(line[0], Is.EqualTo(2));
        Assert.That(line[1], Is.EqualTo(3));
        Assert.That(line.Length, Is.EqualTo(2));
    }

    [Test]
    public void RowView_Sub_1()
    {
        var line = _array.GetRow(4, xOffset: 1, length: 4);
        Assert.That(line[0], Is.EqualTo(21));
        Assert.That(line[1], Is.EqualTo(22));
        Assert.That(line[2], Is.EqualTo(23));
        Assert.That(line[3], Is.EqualTo(24));
        Assert.That(line.Length, Is.EqualTo(4));
    }

    [Test]
    public void RowView_Sub_0_Reverse()
    {
        var line = _array.GetRow(0, xOffset: 2, length: 2, reversed: true);
        Assert.That(line[0], Is.EqualTo(3));
        Assert.That(line[1], Is.EqualTo(2));
        Assert.That(line.Length, Is.EqualTo(2));
    }

    [Test]
    public void RowView_Sub_1_Reverse()
    {
        var line = _array.GetRow(4, xOffset: 1, length: 4, reversed: true);
        Assert.That(line[0], Is.EqualTo(24));
        Assert.That(line[1], Is.EqualTo(23));
        Assert.That(line[2], Is.EqualTo(22));
        Assert.That(line[3], Is.EqualTo(21));
        Assert.That(line.Length, Is.EqualTo(4));
    }

    [Test]
    public void ColView_Full_0()
    {
        var line = _array.GetColumn(0);
        Assert.That(line[0], Is.EqualTo(0));
        Assert.That(line[1], Is.EqualTo(5));
        Assert.That(line[2], Is.EqualTo(10));
        Assert.That(line[3], Is.EqualTo(15));
        Assert.That(line[4], Is.EqualTo(20));
        Assert.That(line.Length, Is.EqualTo(5));
    }

    [Test]
    public void ColView_Full_1()
    {
        var line = _array.GetColumn(2);
        Assert.That(line[0], Is.EqualTo(2));
        Assert.That(line[1], Is.EqualTo(7));
        Assert.That(line[2], Is.EqualTo(12));
        Assert.That(line[3], Is.EqualTo(17));
        Assert.That(line[4], Is.EqualTo(22));
        Assert.That(line.Length, Is.EqualTo(5));
    }

    [Test]
    public void ColView_Full_0_Reverse()
    {
        var line = _array.GetColumn(0, reversed: true);
        Assert.That(line[0], Is.EqualTo(20));
        Assert.That(line[1], Is.EqualTo(15));
        Assert.That(line[2], Is.EqualTo(10));
        Assert.That(line[3], Is.EqualTo(5));
        Assert.That(line[4], Is.EqualTo(0));
        Assert.That(line.Length, Is.EqualTo(5));
    }

    [Test]
    public void ColView_Full_1_Reverse()
    {
        var line = _array.GetColumn(3, reversed: true);
        Assert.That(line[0], Is.EqualTo(23));
        Assert.That(line[1], Is.EqualTo(18));
        Assert.That(line[2], Is.EqualTo(13));
        Assert.That(line[3], Is.EqualTo(8));
        Assert.That(line[4], Is.EqualTo(3));
        Assert.That(line.Length, Is.EqualTo(5));
    }

    [Test]
    public void ColView_Sub_0()
    {
        var line = _array.GetColumn(0, yOffset: 2, length: 2);
        Assert.That(line[0], Is.EqualTo(10));
        Assert.That(line[1], Is.EqualTo(15));
        Assert.That(line.Length, Is.EqualTo(2));
    }

    [Test]
    public void ColView_Sub_1()
    {
        var line = _array.GetColumn(4, yOffset: 1, length: 4);
        Assert.That(line[0], Is.EqualTo(9));
        Assert.That(line[1], Is.EqualTo(14));
        Assert.That(line[2], Is.EqualTo(19));
        Assert.That(line[3], Is.EqualTo(24));
        Assert.That(line.Length, Is.EqualTo(4));
    }

    [Test]
    public void ColView_Sub_0_Reverse()
    {
        var line = _array.GetColumn(0, yOffset: 2, length: 2, reversed: true);
        Assert.That(line[0], Is.EqualTo(15));
        Assert.That(line[1], Is.EqualTo(10));
        Assert.That(line.Length, Is.EqualTo(2));
    }

    [Test]
    public void ColView_Sub_1_Reverse()
    {
        var line = _array.GetColumn(4, yOffset: 1, length: 4, reversed: true);
        Assert.That(line[0], Is.EqualTo(24));
        Assert.That(line[1], Is.EqualTo(19));
        Assert.That(line[2], Is.EqualTo(14));
        Assert.That(line[3], Is.EqualTo(9));
        Assert.That(line.Length, Is.EqualTo(4));
    }

    [Test]
    public void RowView_GetSub_0()
    {
        var line = _array.GetRow(0);
        line = line.GetSubLine(3);
        Assert.That(line[0], Is.EqualTo(3));
        Assert.That(line[1], Is.EqualTo(4));
        Assert.That(line.Length, Is.EqualTo(2));
    }

    [Test]
    public void RowView_GetSub_0_Reverse()
    {
        var line = _array.GetRow(0, reversed: true);
        line = line.GetSubLine(3);
        Assert.That(line[0], Is.EqualTo(1));
        Assert.That(line[1], Is.EqualTo(0));
        Assert.That(line.Length, Is.EqualTo(2));
    }

    [Test]
    public void RowView_GetSub_1()
    {
        var line = _array.GetRow(0);
        line = line
            .GetSubLine(1);

        line = line
            .GetSubLine(0, 3);
        Assert.That(line[0], Is.EqualTo(1));
        Assert.That(line[1], Is.EqualTo(2));
        Assert.That(line[2], Is.EqualTo(3));
        Assert.That(line.Length, Is.EqualTo(3));
    }

    [Test]
    public void RowView_GetSub_1_Reverse()
    {
        var line = _array.GetRow(0, reversed: true);
        line = line
            .GetSubLine(1);

        line = line
            .GetSubLine(0, 3);
        Assert.That(line[0], Is.EqualTo(3));
        Assert.That(line[1], Is.EqualTo(2));
        Assert.That(line[2], Is.EqualTo(1));
        Assert.That(line.Length, Is.EqualTo(3));
    }
}
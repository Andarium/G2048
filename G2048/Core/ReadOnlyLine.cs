namespace G2048;

internal readonly ref struct ReadOnlyLine<T>
{
    private readonly T[,] _array;
    private readonly int _xStart;
    private readonly int _yStart;
    private readonly int _xEnd;
    private readonly int _yEnd;

    public readonly int Length;

    public T First => this[0];
    public T Last => this[Length - 1];

    public readonly bool Vertical;
    public readonly bool Reversed;

    public T this[int i]
    {
        get
        {
            if (Vertical)
            {
                return Reversed
                    ? _array[_yEnd - i, _xEnd]
                    : _array[_yStart + i, _xStart];
            }

            return Reversed
                ? _array[_yEnd, _xEnd - i]
                : _array[_yStart, _xStart + i];
        }
    }

    public ReadOnlyLine(T[,] array, int xStart, int yStart, int length, bool vertical, bool reversed)
    {
        if (xStart < 0 || xStart >= array.GetLength(1) || yStart < 0 || yStart >= array.GetLength(0))
        {
            throw new IndexOutOfRangeException();
        }

        var maxLength = array.GetLength(vertical ? 0 : 1);
        var offset = vertical ? yStart : xStart;
        if (length < 0 || length > maxLength || length + offset > maxLength)
        {
            throw new ArgumentOutOfRangeException();
        }

        _array = array;
        _yStart = yStart;
        _xStart = xStart;
        _yEnd = vertical ? yStart + length - 1 : yStart;
        _xEnd = vertical ? xStart : xStart + length - 1;
        Vertical = vertical;
        Reversed = reversed;
        Length = length;
    }

    public ReadOnlyLine<T> GetSubLine(int startIndex, int? length = null)
    {
        var xStart = _xStart;
        var xEnd = _xEnd;
        var yStart = _yStart;
        var yEnd = _yEnd;
        int lineLength;

        if (Vertical)
        {
            if (Reversed)
            {
                yEnd -= startIndex;
                if (length is { } l)
                {
                    yStart = yEnd - (l - 1);
                }
            }
            else
            {
                yStart += startIndex;
                if (length is { } l)
                {
                    yEnd = yStart + (l - 1);
                }
            }

            lineLength = yEnd - yStart + 1;
        }
        else
        {
            if (Reversed)
            {
                xEnd -= startIndex;
                if (length is { } l)
                {
                    xStart = xEnd - (l - 1);
                }
            }
            else
            {
                xStart += startIndex;
                if (length is { } l)
                {
                    xEnd = xStart + (l - 1);
                }
            }

            lineLength = xEnd - xStart + 1;
        }

        return new ReadOnlyLine<T>(_array, xStart, yStart, lineLength, Vertical, Reversed);
    }

    public override string ToString()
    {
        var s = "";
        for (var i = 0; i < Length; i++)
        {
            s += $"{this[i]};";
        }

        return s;
    }
}

internal static class ReadOnlySpanViewExtensions
{
    public static ReadOnlyLine<T> GetRow<T>(this T[,] input, int row, bool reversed = false, int xOffset = 0, int? length = null)
    {
        var rowLength = input.GetLength(0);
        length ??= rowLength - xOffset;
        return new ReadOnlyLine<T>(input, xOffset, row, length.Value, false, reversed);
    }

    public static ReadOnlyLine<T> GetColumn<T>(this T[,] input, int col, bool reversed = false, int yOffset = 0, int? length = null)
    {
        var colLength = input.GetLength(1);
        length ??= colLength - yOffset;
        return new ReadOnlyLine<T>(input, col, yOffset, length.Value, true, reversed);
    }

    public static ReadOnlyLine<T> GetLine<T>(this T[,] input, bool vertical, int index, bool reversed = false, int offset = 0, int? length = null)
    {
        return vertical
            ? GetColumn(input, index, reversed, offset, length)
            : GetRow(input, index, reversed, offset, length);
    }
}
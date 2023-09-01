namespace G2048;

public sealed class Board
{
    private const int DefaultBoardSize = 4;
    private readonly Tile[,] _grid;
    private readonly int[,] _tempOutput;
    private readonly IRandom _random;

    private int RowCount => _grid.GetLength(0);
    private int ColCount => _grid.GetLength(1);

    public Board(IRandom random, int rows = DefaultBoardSize, int cols = DefaultBoardSize)
    {
        _random = random;
        _tempOutput = new int[rows, cols];

        _grid = new Tile[rows, cols];

        for (var r = 0; r < RowCount; r++)
        {
            for (var c = 0; c < ColCount; c++)
            {
                _grid[r, c] = new Tile(r, c, 0);
            }
        }
    }

    public void AddTile()
    {
        var emptyTiles = _grid
            .Cast<Tile>()
            .Where(x => x.IsEmpty)
            .ToList();

        if (emptyTiles.Count == 0)
        {
            throw new InvalidOperationException("Can't add tile, board is full");
        }

        var index = _random.Next(emptyTiles.Count);
        var tile = emptyTiles[index];

        var value = GenerateNewValue();
        _grid[tile.Row, tile.Col] = new Tile(tile.Row, tile.Col, value);
    }

    public int[,] GetGridOutput()
    {
        for (var r = 0; r < RowCount; r++)
        {
            for (var c = 0; c < ColCount; c++)
            {
                _tempOutput[r, c] = _grid[r, c].Value;
            }
        }

        return _tempOutput;
    }

    private int GenerateNewValue()
    {
        return _random.Next(100) < 10
            ? 4
            : 2;
    }

    public bool Move(int turn, Direction dir, out int scoreDelta)
    {
        var reverse = dir is Direction.Left or Direction.Up;
        var vertical = dir is Direction.Up or Direction.Down;
        var limit = vertical ? RowCount : ColCount;

        var changed = false;
        scoreDelta = 0;
        for (var i = 0; i < limit; i++)
        {
            var line = _grid.GetLine(vertical, i, reverse);
            changed |= MoveTilesInLine(turn, line, out var subDelta);
            scoreDelta += subDelta;
        }

        return changed;
    }

    /// <summary>
    /// Move tiles within whole line
    /// </summary>
    private bool MoveTilesInLine(int turn, ReadOnlyLine<Tile> line, out int scoreDelta)
    {
        var changed = false;
        scoreDelta = 0;
        for (var i = line.Length - 1; i >= 0; i--)
        {
            var tile = line[i];
            if (tile.IsEmpty)
            {
                continue;
            }

            changed |= MoveFirstTile(turn, line.GetSubLine(i), out var subScoreDelta);
            scoreDelta += subScoreDelta;
        }

        return changed;
    }

    /// <summary>
    /// Move first tile to the end of line
    /// </summary>
    private bool MoveFirstTile(int turn, ReadOnlyLine<Tile> line, out int scoreDelta)
    {
        var sourceTile = line.First;

        var destination = 0;

        for (var i = 1; i < line.Length; i++)
        {
            var elem = line[i];
            if (elem.IsEmpty)
            {
                destination = i;
                continue;
            }

            if (sourceTile.CanMergeInto(elem, turn))
            {
                destination = i;
            }

            break;
        }

        if (destination == 0)
        {
            scoreDelta = 0;
            return false;
        }

        sourceTile.MergeOrMoveTo(line[destination], turn, out scoreDelta);
        return true;
    }

    public bool CanMove()
    {
        var rows = RowCount;
        var cols = ColCount;

        for (var r = 0; r < rows; r++)
        {
            if (CanMoveLine(_grid.GetRow(r)))
            {
                return true;
            }
        }

        for (var c = 0; c < cols; c++)
        {
            if (CanMoveLine(_grid.GetColumn(c)))
            {
                return true;
            }
        }

        return false;
    }

    private bool CanMoveLine(ReadOnlyLine<Tile> line)
    {
        var lastElem = line.First;
        for (var i = 1; i < line.Length; i++)
        {
            var elem = line[i];
            if (elem.IsEmpty || elem.Value == lastElem.Value)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasTile(int value)
    {
        return _grid
            .Cast<Tile>()
            .Any(x => x.Value == value);
    }

    internal void Clear()
    {
        foreach (var tile in _grid.Cast<Tile>())
        {
            tile.Clear();
        }
    }

    internal void SetTiles(params Tile[] tiles)
    {
        foreach (var tile in tiles)
        {
            _grid[tile.Row, tile.Col].CopyValuesFrom(tile);
        }
    }
}
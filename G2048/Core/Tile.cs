namespace G2048;

internal sealed class Tile
{
    public readonly int Row;
    public readonly int Col;
    public int Value { get; private set; }
    private int MergedTurn { get; set; }

    public bool IsEmpty => Value == 0;

    public void Clear()
    {
        Value = 0;
        MergedTurn = 0;
    }

    public Tile(int row, int col, int value, int mergedTurn = 0)
    {
        Row = row;
        Col = col;
        Value = value;
        MergedTurn = mergedTurn;
    }

    public bool CanMergeInto(Tile other, int turn)
    {
        if (Value != other.Value)
        {
            return false;
        }

        return turn > MergedTurn && turn > other.MergedTurn;
    }

    private bool MergeInto(Tile other, int turn)
    {
        if (!CanMergeInto(other, turn))
        {
            return false;
        }

        other.Value += Value;
        other.MergedTurn = turn;
        Clear();
        return true;
    }

    private void MoveTo(Tile other)
    {
        other.Value = Value;
        other.MergedTurn = MergedTurn;
        Clear();
    }

    public void MergeOrMoveTo(Tile other, int turn, out int scoreDelta)
    {
        if (other.IsEmpty)
        {
            MoveTo(other);
            scoreDelta = 0;
        }
        else
        {
            MergeInto(other, turn);
            scoreDelta = other.Value;
        }
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public void CopyValuesFrom(Tile tile)
    {
        MergedTurn = tile.MergedTurn;
        Value = tile.Value;
    }
}
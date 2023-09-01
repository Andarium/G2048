namespace G2048;

public interface IScore : IReadOnlyScore
{
    public void ResetCurrent();
    public void ResetHighest();

    public void Update(int scoreDelta);
}

public interface IReadOnlyScore
{
    public int GetCurrent();
    public int GetHighest();
}
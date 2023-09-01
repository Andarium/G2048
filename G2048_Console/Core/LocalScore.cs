using G2048;

namespace G2048_Console;

public sealed class LocalScore : IScore
{
    private int _current;
    private int _highest;

    public int GetCurrent() => _current;

    public int GetHighest() => _highest;

    public void ResetCurrent() => _current = 0;

    public void ResetHighest()
    {
        _highest = 0;
        SaveHighest(0);
    }

    public void Update(int scoreDelta)
    {
        if (scoreDelta <= 0)
        {
            return;
        }

        _current += scoreDelta;
        if (_current > _highest)
        {
            _highest = _current;
            SaveHighest(_highest);
        }
    }

    public LocalScore()
    {
        _highest = LoadHighest();
    }

    private int LoadHighest()
    {
        var path = GetHighScorePath();
        if (!File.Exists(path))
        {
            return 0;
        }

        var line = File.ReadLines(path).FirstOrDefault();

        if (string.IsNullOrWhiteSpace(line) || !int.TryParse(line, out var highScore))
        {
            return 0;
        }

        return highScore;
    }

    private void SaveHighest(int highScore)
    {
        var path = GetHighScorePath();
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        }

        File.WriteAllText(path, highScore.ToString());
    }

    private string GetHighScorePath()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
        path = Path.Combine(path, "G2048", "score.dat");
        return path;
    }
}
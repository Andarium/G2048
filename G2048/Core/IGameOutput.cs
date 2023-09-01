namespace G2048;

public interface IGameOutput
{
    void DisplayGame(GameState state, IReadOnlyScore score, int turn, int[,] board);
    void DisplayMessage(string message);
}
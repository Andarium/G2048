using System.Text;
using G2048;

namespace G2048_Console;

public sealed class ConsoleGameOutput : IGameOutput
{
    private readonly StringBuilder _buffer = new();
    private readonly string _cellFormat;
    private readonly string _gridPart;

    private readonly Dictionary<int, ConsoleColor> _colorMap = new()
    {
        { 2, ConsoleColor.DarkGray },
        { 4, ConsoleColor.DarkGreen },
        { 8, ConsoleColor.DarkYellow },
        { 16, ConsoleColor.Cyan },
        { 32, ConsoleColor.DarkCyan },
        { 64, ConsoleColor.Blue },
        { 128, ConsoleColor.Green },
        { 256, ConsoleColor.Yellow },
        { 512, ConsoleColor.Magenta },
        { 1024, ConsoleColor.DarkRed },
        { 2048, ConsoleColor.Red }
    };

    public ConsoleGameOutput(int cellWidth)
    {
        _cellFormat = $"{{0,{cellWidth}}}";
        _gridPart = "|" + new string(Enumerable.Repeat('-', cellWidth).ToArray());
    }

    public void DisplayGame(GameState state, IReadOnlyScore score, int turn, int[,] board)
    {
        Console.Clear();

        switch (state)
        {
            case GameState.NotStarted:
                DisplayNewGame();
                break;
            case GameState.Started:
                DisplayOngoing(score, turn, board);
                break;
            case GameState.Loss:
                DisplayLoss(score, turn, board);
                break;
            case GameState.Victory:
                DisplayVictory(score, turn, board);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void DisplayNewGame()
    {
        Console.WriteLine("Press R to start new game");
    }

    private void DisplayLoss(IReadOnlyScore score, int turn, int[,] board)
    {
        Console.WriteLine($"No more moves, final score: {score.GetCurrent()}, highest: {score.GetHighest()}, turns: {turn}");
        Console.WriteLine();
        DisplayField(board);
    }

    private void DisplayVictory(IReadOnlyScore score, int turn, int[,] board)
    {
        Console.WriteLine($"Victory, final score: {score}, highest: {score.GetHighest()}, turns: {turn}");
        Console.WriteLine();
        DisplayField(board);
    }

    private void DisplayOngoing(IReadOnlyScore score, int turn, int[,] board)
    {
        Console.WriteLine($"Turn: {turn}, score: {score.GetCurrent()}, highest: {score.GetHighest()}");
        Console.WriteLine();
        DisplayField(board);
    }

    private void DisplayField(int[,] board)
    {
        var rows = board.GetLength(0);
        var cols = board.GetLength(1);

        for (var r = 0; r < rows; r++)
        {
            DisplaySeparatorLine(cols);
            DisplayLine(board, r);
        }

        DisplaySeparatorLine(cols);

        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("WASD, arrows - move, Q - quit, R - restart");
    }

    private void DisplaySeparatorLine(int cols)
    {
        _buffer.Clear();
        for (var c = 0; c < cols; c++)
        {
            _buffer.Append(_gridPart);
        }

        _buffer.Append('|');
        Console.WriteLine(_buffer.ToString());
    }

    private void DisplayLine(int[,] board, int row)
    {
        var cols = board.GetLength(1);
        for (var c = 0; c < cols; c++)
        {
            Console.Write('|');
            var value = board[row, c];
            var s = string.Format(_cellFormat, value);
            using (GetColorBlock(value))
            {
                Console.Write(s);
            }
        }

        Console.WriteLine('|');
    }

    public void DisplayMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }

    private ColorBlock GetColorBlock(int value)
    {
        if (_colorMap.TryGetValue(value, out var color))
        {
            return new ColorBlock(color);
        }

        return new ColorBlock(ConsoleColor.White);
    }
}
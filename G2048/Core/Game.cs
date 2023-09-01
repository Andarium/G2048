namespace G2048;

public sealed class Game
{
    public const int BoardSize = 4;
    public const int VictoryTile = 2048;

    private readonly IGameInput _gameInput;
    private readonly IGameOutput _gameOutput;
    private readonly IRandom _random;
    private Board _board = default!;

    private readonly IScore _score;
    public int Turn { get; private set; }
    public GameState State { get; private set; }

    public Game(IGameInput input, IGameOutput output, IScore score, IRandom random)
    {
        _score = score;
        _random = random;
        _gameInput = input;
        _gameOutput = output;
    }

    private void StartNewGame(int rows = BoardSize, int cols = BoardSize)
    {
        _board = new Board(_random, rows, cols);
        _board.AddTile();
        _board.AddTile();
        _score.ResetCurrent();
        Turn = 1;
        State = GameState.Started;
    }

    private void CheckEndGame()
    {
        if (_board.HasTile(VictoryTile))
        {
            State = GameState.Victory;
        }
        else if (!_board.CanMove())
        {
            State = GameState.Loss;
        }
    }

    private void Move(Direction direction)
    {
        if (State is not GameState.Started)
        {
            throw new InvalidOperationException($"{GameState.Started} game state expected, got {State}");
        }

        if (_board.Move(Turn, direction, out var scoreDelta))
        {
            _score.Update(scoreDelta);
            Turn++;
            _board.AddTile();
        }
    }

    private bool ConfirmAction(string message)
    {
        GameCommandType command;
        do
        {
            _gameOutput.DisplayMessage(message);
            command = _gameInput.ReadCommand();
        } while (command is not (GameCommandType.Confirm or GameCommandType.Cancel));

        return command is GameCommandType.Confirm;
    }

    private void RestartWithConfirm()
    {
        if (ConfirmAction("Restart the game? (Y/N):"))
        {
            StartNewGame();
        }
    }

    private void QuitWithConfirm()
    {
        if (ConfirmAction("Quit the game? (Y/N):"))
        {
            Environment.Exit(0);
        }
    }

    public void Update()
    {
        if (State is GameState.NotStarted)
        {
            StartNewGame();
        }
        else
        {
            var command = _gameInput.ReadCommand();
            if (command is GameCommandType.Restart)
            {
                RestartWithConfirm();
            }
            else if (command is GameCommandType.Quit)
            {
                QuitWithConfirm();
            }

            if (State is GameState.Started)
            {
                switch (command)
                {
                    case GameCommandType.MoveDown:
                        Move(Direction.Down);
                        break;
                    case GameCommandType.MoveUp:
                        Move(Direction.Up);
                        break;
                    case GameCommandType.MoveLeft:
                        Move(Direction.Left);
                        break;
                    case GameCommandType.MoveRight:
                        Move(Direction.Right);
                        break;
                }
            }
        }

        CheckEndGame();
        _gameOutput.DisplayGame(State, _score, Turn, _board.GetGridOutput());
    }
}
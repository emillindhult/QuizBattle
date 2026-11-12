namespace QuizBattle;


public class Game
{
    private readonly GameSettings _settings;

    
    public Game(GameSettings settings)
    {
        _settings = settings;
    }

    
    public void Play()
    {
        for (int round = 1; round <= _settings.Rounds; round++)
        {
            PlayRound(round);
        }

        ShowFinalResults();
    }

    
    private void PlayRound(int roundNumber)
    {
        Console.WriteLine();
        Console.WriteLine($"=== Round {roundNumber} of {_settings.Rounds} ===");

        foreach (string playerName in _settings.PlayerNames)
        {
            PlayTurn(playerName);
        }
    }

    
    private void PlayTurn(string playerName)
    {
        Console.WriteLine($"{playerName}'s turn (placeholder)");
    }

    private void ShowFinalResults()
    {
  
        Console.WriteLine();
        Console.WriteLine("=== Final results ===");
    }
}
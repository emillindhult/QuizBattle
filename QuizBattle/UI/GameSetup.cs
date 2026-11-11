namespace QuizBattle;

// Asks the players for number of players, names and number of rounds.
public class GameSetup
{
    private const int MinPlayers = 2;
    private const int MaxPlayers = 4;
    private const int MinRounds = 1;
    private const int MaxRounds = 10;

    // Runs the whole setup and returns the chosen settings.
    public GameSettings CreateSettings()
    {
        var settings = new GameSettings();

        int playerCount = ConsoleInput.ReadInt(
            $"Number of players ({MinPlayers}-{MaxPlayers}): ", MinPlayers, MaxPlayers);

        // Each new name is checked against the names already entered
        for (int i = 1; i <= playerCount; i++)
        {
            string name = ConsoleInput.ReadName($"Name of player {i}: ", settings.PlayerNames);
            settings.PlayerNames.Add(name);
        }

        settings.Rounds = ConsoleInput.ReadInt(
            $"Number of rounds ({MinRounds}-{MaxRounds}): ", MinRounds, MaxRounds);

        return settings;
    }
}
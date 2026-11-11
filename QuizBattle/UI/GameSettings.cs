namespace QuizBattle;

// Holds the choices the players make before the game starts.
public class GameSettings
{
    public List<string> PlayerNames { get; set; } = new List<string>();
    public int Rounds { get; set; }
}
namespace QuizBattle.Models;

internal class LeaderboardEntry
{
    public string? Name { get; set; }
    public int Score { get; set; }
    public int CorrectAnswers { get; set; }
    public DateOnly Date { get; set; }
}

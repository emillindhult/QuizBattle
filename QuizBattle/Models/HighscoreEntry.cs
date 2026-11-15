namespace QuizBattle.Models;

internal sealed class HighscoreEntry : IComparable<HighscoreEntry>
{
    public string? Name { get; set; }
    public int Score { get; set; }
    public int CorrectAnswers { get; set; }
    public DateOnly Date { get; set; }

    public int CompareTo(HighscoreEntry? other)
    {
        if (other == null) return 1;

        return Score.CompareTo(other.Score);
    }
}

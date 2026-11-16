using QuizBattle.Models;

namespace QuizBattle.Data;

// Shows the ten best results of all time.
internal class Leaderboard
{
    private readonly JsonHighscoreRepository _repository = new JsonHighscoreRepository();

    public void Show()
    {
        List<HighscoreEntry> topTen = _repository.GetTopTen();

        Console.WriteLine("=== Highscores ===");

        if (topTen.Count == 0)
        {
            Console.WriteLine("No results yet.");
            return;
        }

        for (int i = 0; i < topTen.Count; i++)
        {
            HighscoreEntry entry = topTen[i];
            Console.WriteLine($"{i + 1}. {entry.Name}  {entry.Score} points  {entry.CorrectAnswers} correct  {entry.Date:yyyy-MM-dd}");
        }
    }
}
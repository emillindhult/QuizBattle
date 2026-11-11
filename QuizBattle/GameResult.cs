using QuizBattle.Models;

namespace QuizBattle.Services;

public class GameResult
{
    public List<Player> GetWinners(List<Player> players)
    {
        if (players.Count == 0)
        {
            return new List<Player>();
        }

        int highestScore = players.Max(p => p.Score);

        int mostCorrectAnswers = players
            .Where(p => p.Score == highestScore)
            .Max(p => p.CorrectAnswers);

        return players
            .Where(p =>
                p.Score == highestScore &&
                p.CorrectAnswers == mostCorrectAnswers)
            .ToList();
    }
}
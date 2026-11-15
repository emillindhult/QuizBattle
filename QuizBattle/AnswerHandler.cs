using QuizBattle.Models;
using QuizBattle.Services;

namespace QuizBattle;

public class AnswerHandler
{
    private readonly ScoreManager _scoreManager;

    public AnswerHandler(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }

    public void Handle(Player player, Question question, bool isCorrect)
    {
        if (isCorrect)
        {
            HandleCorrectAnswer(player, question);
        }
        else
        {
            HandleWrongAnswer(player, question);
        }
    }

    private void HandleCorrectAnswer(Player player, Question question)
    {
        int newStreak = player.Streak + 1;
        int totalPoints = _scoreManager.CalculatePoints(question.Difficulty.ToString(), newStreak);
        int bonus = _scoreManager.GetStreakBonus(newStreak);
        int basePoints = totalPoints - bonus;

        player.RegisterCorrectAnswer(totalPoints);

        if (bonus > 0)
        {
            Console.WriteLine($"Correct! {player.Name} gets {basePoints} points + {bonus} streak bonus ({newStreak} correct in a row). Total: {player.Score} points.");
        }
        else
        {
            Console.WriteLine($"Correct! {player.Name} gets {basePoints} points. Total: {player.Score} points.");
        }
    }

    private void HandleWrongAnswer(Player player, Question question)
    {
        player.RegisterWrongAnswer();

        Console.WriteLine($"Wrong. The correct answer was: {GetCorrectAnswerText(question)}. {player.Name} gets 0 points. Total: {player.Score} points.");
    }

    private string GetCorrectAnswerText(Question question)
    {
        return question switch
        {
            MultipleChoiceQuestion multipleChoice => multipleChoice.Options[multipleChoice.CorrectIndex],
            TrueFalseQuestion trueFalse => trueFalse.Correct ? "True" : "False",
            _ => "unknown"
        };
    }
}
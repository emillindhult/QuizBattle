using QuizBattle.Models;
using QuizBattle.Services;

namespace QuizBattle;

public class QuestionAsker
{
    private readonly ScoreManager _scoreManager;

    public QuestionAsker(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }

    public bool TryAsk(string categoryName, Question question, out bool isCorrect)
    {
       
        isCorrect = false;

        if (question is not IQuestion askable)
        {
            Console.WriteLine("This question could not be shown.");
            return false;
        }

        int basePoints = _scoreManager.GetBasePoints(question.Difficulty.ToString());

        Console.WriteLine();
        Console.WriteLine($"{categoryName}, {basePoints} points");
        askable.Display();

        int optionCount = GetOptionCount(question);
        int choice = ConsoleInput.ReadInt($"Your answer (1-{optionCount}): ", 1, optionCount);

        isCorrect = askable.CheckAnswer(choice.ToString());
        return true;
    }

    private int GetOptionCount(Question question)
    {
        return question switch
        {
            MultipleChoiceQuestion multipleChoice => multipleChoice.Options.Count,
            TrueFalseQuestion => 2,
            _ => 0
        };
    }
}
using QuizBattle.Data;
using QuizBattle.Models;

namespace QuizBattle;

public class Game
{
    private readonly GameSettings _settings;
    private readonly CategorySelector _categorySelector = new CategorySelector();
    private List<Category> _categories = new List<Category>();

    public Game(GameSettings settings)
    {
        _settings = settings;
    }

    public void Play()
    {
        LoadCategories();

        if (_categories.Count == 0)
        {
            Console.WriteLine("No questions could be loaded. The game cannot start.");
            return;
        }

        for (int round = 1; round <= _settings.Rounds; round++)
        {
            bool canContinue = PlayRound(round);

            if (!canContinue)
            {
                break;
            }
        }

        ShowFinalResults();
    }

    
    private void LoadCategories()
    {
        var loader = new QuestionLoader();
        _categories = loader.Load();
    }

    private bool PlayRound(int roundNumber)
    {
        Console.WriteLine();
        Console.WriteLine($"=== Round {roundNumber} of {_settings.Rounds} ===");

        foreach (string playerName in _settings.PlayerNames)
        {
            bool turnPlayed = PlayTurn(playerName);

            if (!turnPlayed)
            {
                return false;
            }
        }

        return true;
    }

    private bool PlayTurn(string playerName)
    {
        RemoveEmptyCategories();

        if (_categories.Count == 0)
        {
            Console.WriteLine("All questions have been used. The game ends early.");
            return false;
        }

        Category category = _categorySelector.ChooseCategory(playerName, _categories);

        Console.WriteLine($"{playerName} chose {category.Name} (placeholder)");
        return true;
    }

    private void RemoveEmptyCategories()
    {
        _categories.RemoveAll(category => !category.HasQuestions());
    }

    private void ShowFinalResults()
    {
        Console.WriteLine();
        Console.WriteLine("=== Final results ===");
    }
}
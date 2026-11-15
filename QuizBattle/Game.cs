using QuizBattle.Data;
using QuizBattle.Models;
using QuizBattle.Services;

namespace QuizBattle;

public class Game
{
    private readonly GameSettings _settings;
    private readonly CategorySelector _categorySelector = new CategorySelector();
    private readonly QuestionAsker _questionAsker;
    private readonly AnswerHandler _answerHandler;
    private readonly List<Player> _players = new List<Player>();
    private List<Category> _categories = new List<Category>();

    public Game(GameSettings settings)
    {
        _settings = settings;

        var scoreManager = new ScoreManager();
        _questionAsker = new QuestionAsker(scoreManager);
        _answerHandler = new AnswerHandler(scoreManager);

        foreach (string name in settings.PlayerNames)
        {
            _players.Add(new Player(name));
        }
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

        foreach (Player player in _players)
        {
            bool turnPlayed = PlayTurn(player);

            if (!turnPlayed)
            {
                return false;
            }
        }

        return true;
    }

    private bool PlayTurn(Player player)
    {
        RemoveEmptyCategories();

        if (_categories.Count == 0)
        {
            Console.WriteLine("All questions have been used. The game ends early.");
            return false;
        }

        Category category = _categorySelector.ChooseCategory(player.Name, _categories);
        Question question = DrawQuestion(category);

        if (_questionAsker.TryAsk(category.Name, question, out bool isCorrect))
        {
            _answerHandler.Handle(player, question, isCorrect);
        }

        return true;
    }

    private void RemoveEmptyCategories()
    {
        _categories.RemoveAll(category => !category.HasQuestions());
    }

    private Question DrawQuestion(Category category)
    {
        int index = Random.Shared.Next(category.Questions.Count);
        Question question = category.Questions[index];
        category.Questions.RemoveAt(index);
        return question;
    }

    private void ShowFinalResults()
    {
        Console.WriteLine();
        Console.WriteLine("=== Final results ===");

        foreach (Player player in _players)
        {
            Console.WriteLine($"{player.Name}: {player.Score} points ({player.CorrectAnswers} correct)");
        }
    }
}
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
    private readonly GameResult _gameResult = new GameResult();
    private readonly List<Player> _players = new List<Player>();
    private readonly Dictionary<Player, int> _questionsAnswered = new Dictionary<Player, int>();
    private List<Category> _categories = new List<Category>();

    public Game(GameSettings settings)
    {
        _settings = settings;

        var scoreManager = new ScoreManager();
        _questionAsker = new QuestionAsker(scoreManager);
        _answerHandler = new AnswerHandler(scoreManager);

        foreach (string name in settings.PlayerNames)
        {
            var player = new Player(name);
            _players.Add(player);
            _questionsAnswered[player] = 0;
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
            Console.WriteLine();
            Console.WriteLine("All questions have been used. The game ends early.");
            return false;
        }

        Category category = _categorySelector.ChooseCategory(player.Name, _categories);
        Question question = DrawQuestion(category);

        if (_questionAsker.TryAsk(category.Name, question, out bool isCorrect))
        {
            _answerHandler.Handle(player, question, isCorrect);
            _questionsAnswered[player]++;
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

        List<Player> ranking = _players
            .OrderByDescending(player => player.Score)
            .ThenByDescending(player => player.CorrectAnswers)
            .ToList();

        for (int i = 0; i < ranking.Count; i++)
        {
            Player player = ranking[i];
            Console.WriteLine($"{i + 1}. {player.Name}  {player.Score} points  ({player.CorrectAnswers} of {_questionsAnswered[player]} correct)");
        }

        ShowWinners();
        SaveResults();
    }

    private void ShowWinners()
    {
        List<Player> winners = _gameResult.GetWinners(_players);

        if (winners.Count == 1)
        {
            Console.WriteLine($"{winners[0].Name} wins!");
        }
        else if (winners.Count > 1)
        {
            string names = string.Join(" and ", winners.Select(winner => winner.Name));
            Console.WriteLine($"Shared victory: {names}!");
        }
    }

    private List<LeaderboardEntry> CreateLeaderboardEntries()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        return _players
            .Select(player => new LeaderboardEntry
            {
                Name = player.Name,
                Score = player.Score,
                CorrectAnswers = player.CorrectAnswers,
                Date = today
            })
            .ToList();
    }

    private void SaveResults()
    {
        List<LeaderboardEntry> entries = CreateLeaderboardEntries();
    }
}
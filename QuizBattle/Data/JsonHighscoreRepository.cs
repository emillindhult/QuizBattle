using QuizBattle.Models;
using System.Text.Json;

namespace QuizBattle.Data;

internal sealed class JsonHighscoreRepository : IRepository<HighscoreEntry>, IJsonLoadable<HighscoreEntry>
{
    private readonly string _path = Path.Combine(AppContext.BaseDirectory, "Data", "highscores.json");
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public void Add(List<HighscoreEntry> values)
    {
        List<HighscoreEntry> highscores = Load();
        highscores.AddRange(values);
        Save(highscores);
    }

    public void Add(HighscoreEntry value)
    {
        Add(new List<HighscoreEntry> { value });
    }

    public List<HighscoreEntry> Load()
    {
        try
        {
            if (!File.Exists(_path))
            {
                return [];
            }

            string jsonString = File.ReadAllText(_path);

            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return [];
            }

            List<HighscoreEntry>? highscores = JsonSerializer.Deserialize<List<HighscoreEntry>>(jsonString, _options);

            return highscores ?? [];
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Could not parse highscores.json: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"Could not read highscores.json: {ex.Message}");
        }

        return [];
    }

    public List<HighscoreEntry> GetTopTen()
    {
        return Load()
            .OrderByDescending(entry => entry.Score)
            .ThenByDescending(entry => entry.CorrectAnswers)
            .Take(10)
            .ToList();
    }

    private void Save(List<HighscoreEntry> highscores)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path)!);

            string jsonString = JsonSerializer.Serialize(highscores, _options);
            File.WriteAllText(_path, jsonString);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"Could not save highscores: {ex.Message}");
        }
    }
}
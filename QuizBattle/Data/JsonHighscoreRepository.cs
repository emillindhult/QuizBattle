using System;
using QuizBattle.Models;
using System.Text.Json;

namespace QuizBattle.Data;

internal sealed class JsonHighscoreRepository : IRepository<HighscoreEntry>, IJsonLoadable<HighscoreEntry>
{
    private readonly string _path = Path.Combine(AppContext.BaseDirectory, "Data", "highscore.json");
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public void Add(List<HighscoreEntry> values)
    {
        List<HighscoreEntry> tempHighscore = Load();
        tempHighscore.AddRange(values);

        var jsonString = JsonSerializer.Serialize(tempHighscore, _options);

        try
        {
            File.WriteAllText(_path, jsonString);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"Could not save highscores: {ex.Message}");
        }
    }

    public void Add(HighscoreEntry value)
    {
        List<HighscoreEntry> tempHighscore = Load();
        tempHighscore.Add(value);

        var jsonString = JsonSerializer.Serialize(tempHighscore, _options);

        try
        {
            File.WriteAllText(_path, jsonString);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"Could not save highscores: {ex.Message}");
        }
    }

    public List<HighscoreEntry> Load()
    {
        try
        {
            if (!File.Exists(_path))
            {
                File.Create(_path);
            }

            string jsonString = File.ReadAllText(_path);

            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return [];
            }

            List<HighscoreEntry>? highscore = JsonSerializer.Deserialize<List<HighscoreEntry>>(jsonString);

            return highscore ?? [];
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Could not parse highscore.json: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"Could not read highscore.json: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Something unexpected happened: {ex.Message}");
        }

        return [];
    }

    public List<HighscoreEntry> GetTopTen()
    {
        string jsonString = File.ReadAllText(_path);

        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return [];
        }

        try
        {
            List<HighscoreEntry>? highscore = JsonSerializer.Deserialize<List<HighscoreEntry>>(jsonString);

            if (highscore == null)
            {
                return [];
            }

            highscore.Sort();
            highscore.Reverse();

            return highscore.GetRange(0, 10);
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Could not parse highscore.json: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"Could not read highscore.json: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Something unexpected happened: {ex.Message}");
        }

        return [];
    }
}

using QuizBattle.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuizBattle.Data;

public class QuestionLoader : IJsonLoadable<Category>
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public List<Category> Load()
    {
        string filePath = GetFilePath();

        try
        {
            string jsonString = File.ReadAllText(filePath);

            QuestionsDocument? document = JsonSerializer.Deserialize<QuestionsDocument>(jsonString, options);

            return document?.Categories ?? [];
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Questions file was not found: {filePath}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Could not parse questions.json: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Could not read questions.json: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something unexpected happened: {ex.Message}");
        }

        return [];
    }

    private static string GetFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "Data", "questions.json");
    }

    private sealed class QuestionsDocument
    {
        public List<Category> Categories { get; set; } = [];
    }
}
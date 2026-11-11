using System.Text.Json;

namespace QuizBattle.Data;

public class QuestionLoader : IJsonLoadable<Category>
{
    public List<Category> Load()
    {
        string filePath = GetFilePath();

        try
        {
            string jsonString = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

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

public class Category
{
    public string? Name { get; set; }

    public List<Question> Questions { get; set; } = [];
}

public class Question
{
    public string? Type { get; set; }

    public string? Text { get; set; }

    public List<string> Options { get; set; } = [];

    public int? CorrectIndex { get; set; }

    public bool? Correct { get; set; }

    public string? Difficulty { get; set; }
}
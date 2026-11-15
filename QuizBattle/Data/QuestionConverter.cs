using System.Text.Json;
using System.Text.Json.Serialization;
using QuizBattle.Models;

namespace QuizBattle.Data;

// Reads the "type" field in the JSON and creates the correct question class.
public class QuestionConverter : JsonConverter<Question>
{
    public override Question? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        JsonElement root = document.RootElement;
        string json = root.GetRawText();

        string type = string.Empty;

        // "type" can be text ("multipleChoice") or a number (enum value)
        if (root.TryGetProperty("type", out JsonElement typeElement))
        {
            type = typeElement.ValueKind == JsonValueKind.String
                ? typeElement.GetString() ?? string.Empty
                : typeElement.ToString();
        }

        // Fallback: decide from the fields the question has
        if (type == string.Empty)
        {
            type = root.TryGetProperty("options", out _) ? "multipleChoice" : "trueFalse";
        }

        return type.ToLower() switch
        {
            "multiplechoice" or "1" => JsonSerializer.Deserialize<MultipleChoiceQuestion>(json, options),
            "truefalse" or "0" => JsonSerializer.Deserialize<TrueFalseQuestion>(json, options),
            _ => throw new JsonException($"Unknown question type: {type}")
        };
    }

    public override void Write(Utf8JsonWriter writer, Question value, JsonSerializerOptions options)
    {
        throw new NotSupportedException("Writing questions to JSON is not needed.");
    }
}
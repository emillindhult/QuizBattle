using System.Text.Json;

namespace QuizBattle.Data;

public class QuestionLoader : IJsonLoadable<Category>
{
    public QuestionLoader()
    {
        if (!File.Exists(GetFilePath()))
        {
            CreateAndSeedFile();
        }
    }

    public List<Category> Load()
    {
        string filePath = GetFilePath();
        string jsonString = File.ReadAllText(filePath);

        List<Category> result = [];

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {
            result = JsonSerializer.Deserialize<List<Category>>(jsonString, options) ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not load questions. {ex.Message}");
        }

        return result;
    }

    private static string GetFilePath()
    {
        string dataDirectory = Directory.GetCurrentDirectory();

        return Path.Combine(dataDirectory, "questions.json");
    }

    private static void CreateAndSeedFile()
    {
        string filePath = GetFilePath();
        string jsonString = "{\r\n  \"categories\": [\r\n    {\r\n      \"name\": \"Programmering\",\r\n      \"questions\": [\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilket tecken används i C# för att en klass ska ärva från en annan klass?\",\r\n          \"options\": [\r\n            \"->\",\r\n            \"=>\",\r\n            \":\",\r\n            \"::\"\r\n          ],\r\n          \"correctIndex\": 2,\r\n          \"difficulty\": \"easy\"\r\n        },\r\n        {\r\n          \"type\": \"trueFalse\",\r\n          \"text\": \"I C# används typen int för heltal.\",\r\n          \"correct\": true,\r\n          \"difficulty\": \"easy\"\r\n        },\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilket Git-kommando skapar en ny gren och byter till den?\",\r\n          \"options\": [\r\n            \"git branch -d\",\r\n            \"git checkout -b\",\r\n            \"git merge\",\r\n            \"git clone\"\r\n          ],\r\n          \"correctIndex\": 1,\r\n          \"difficulty\": \"medium\"\r\n        },\r\n        {\r\n          \"type\": \"trueFalse\",\r\n          \"text\": \"En abstrakt klass i C# kan instansieras med new.\",\r\n          \"correct\": false,\r\n          \"difficulty\": \"medium\"\r\n        },\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilken samling i C# passar bäst för en lista som kan växa och krympa?\",\r\n          \"options\": [\r\n            \"List<T>\",\r\n            \"int[]\",\r\n            \"string\",\r\n            \"char\"\r\n          ],\r\n          \"correctIndex\": 0,\r\n          \"difficulty\": \"easy\"\r\n        },\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vad händer om du anropar First() med LINQ på en tom lista?\",\r\n          \"options\": [\r\n            \"Det returneras null\",\r\n            \"Det returneras 0\",\r\n            \"Ett undantag kastas\",\r\n            \"Det returneras en tom lista\"\r\n          ],\r\n          \"correctIndex\": 2,\r\n          \"difficulty\": \"hard\"\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"name\": \"Geografi\",\r\n      \"questions\": [\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilken är Sveriges huvudstad?\",\r\n          \"options\": [\r\n            \"Göteborg\",\r\n            \"Stockholm\",\r\n            \"Malmö\",\r\n            \"Uppsala\"\r\n          ],\r\n          \"correctIndex\": 1,\r\n          \"difficulty\": \"easy\"\r\n        },\r\n        {\r\n          \"type\": \"trueFalse\",\r\n          \"text\": \"Göteborg ligger vid Västerhavet.\",\r\n          \"correct\": true,\r\n          \"difficulty\": \"easy\"\r\n        },\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilken är Europas längsta flod?\",\r\n          \"options\": [\r\n            \"Donau\",\r\n            \"Rhen\",\r\n            \"Loire\",\r\n            \"Volga\"\r\n          ],\r\n          \"correctIndex\": 3,\r\n          \"difficulty\": \"medium\"\r\n        },\r\n        {\r\n          \"type\": \"trueFalse\",\r\n          \"text\": \"Mount Everest ligger i Anderna.\",\r\n          \"correct\": false,\r\n          \"difficulty\": \"medium\"\r\n        },\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilken är Sveriges största sjö?\",\r\n          \"options\": [\r\n            \"Vänern\",\r\n            \"Vättern\",\r\n            \"Mälaren\",\r\n            \"Hjälmaren\"\r\n          ],\r\n          \"correctIndex\": 0,\r\n          \"difficulty\": \"medium\"\r\n        },\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilket land har flest invånare i Afrika?\",\r\n          \"options\": [\r\n            \"Egypten\",\r\n            \"Etiopien\",\r\n            \"Nigeria\",\r\n            \"Sydafrika\"\r\n          ],\r\n          \"correctIndex\": 2,\r\n          \"difficulty\": \"hard\"\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"name\": \"Allmänbildning\",\r\n      \"questions\": [\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Hur många sidor har en hexagon?\",\r\n          \"options\": [\r\n            \"Fem\",\r\n            \"Sex\",\r\n            \"Sju\",\r\n            \"Åtta\"\r\n          ],\r\n          \"correctIndex\": 1,\r\n          \"difficulty\": \"easy\"\r\n        },\r\n        {\r\n          \"type\": \"trueFalse\",\r\n          \"text\": \"Vatten kokar vid 100 grader Celsius vid normalt lufttryck.\",\r\n          \"correct\": true,\r\n          \"difficulty\": \"easy\"\r\n        },\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilken kemisk symbol har guld?\",\r\n          \"options\": [\r\n            \"Ag\",\r\n            \"Gd\",\r\n            \"Au\",\r\n            \"Go\"\r\n          ],\r\n          \"correctIndex\": 2,\r\n          \"difficulty\": \"medium\"\r\n        },\r\n        {\r\n          \"type\": \"trueFalse\",\r\n          \"text\": \"Blåvalen är det största djur som någonsin har levt.\",\r\n          \"correct\": true,\r\n          \"difficulty\": \"medium\"\r\n        },\r\n        {\r\n          \"type\": \"multipleChoice\",\r\n          \"text\": \"Vilket är det vanligaste grundämnet i universum?\",\r\n          \"options\": [\r\n            \"Helium\",\r\n            \"Väte\",\r\n            \"Syre\",\r\n            \"Kol\"\r\n          ],\r\n          \"correctIndex\": 1,\r\n          \"difficulty\": \"hard\"\r\n        },\r\n        {\r\n          \"type\": \"trueFalse\",\r\n          \"text\": \"Botaniskt sett är en gurka en frukt.\",\r\n          \"correct\": true,\r\n          \"difficulty\": \"hard\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}\r\n";

        File.WriteAllText(filePath, jsonString);
    }
}

public class Category
{

}
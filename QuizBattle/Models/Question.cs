namespace QuizBattle.Models;

public class Question
{
    public QuestionType Type { get; set; }
    public string? Text { get; set; }
    public Difficulty Difficulty { get; set; }

    public int GetPoints()
    {
        return Difficulty switch
        {
            Models.Difficulty.Easy => 10,
            Models.Difficulty.Medium => 20,
            Models.Difficulty.Hard => 30,
            _ => 0,
        };
    }
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public enum QuestionType
{
    TrueFalse,
    MultipleChoice
}

namespace QuizBattle.Models;

public class Question
{
    public QuestionType Type { get; set; }
    public string? Text { get; set; }
    public Difficulty Difficulty { get; set; }
    //public string Text { get; set; }
    //public string Category { get; set; }
    //public string Difficulty { get; set; }

    //public Question(string text, string difficulty)
    //{
    //    Text = text;
    //    //Category = category;
    //    Difficulty = difficulty;
    //}

    public int GetPoints()
    {
        switch (Difficulty)
        {
            case Models.Difficulty.Easy:
                return 10;

            case Models.Difficulty.Medium:
                return 20;

            case Models.Difficulty.Hard:
                return 30;

            default:
                return 0;
        }
    }

    //public abstract void Display();

    //public abstract bool CheckAnswer(string answer);
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

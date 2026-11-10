
namespace QuizBattle.Models
{
    public abstract class Question : IQuestion
    {
        public string Text { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }

        public Question(string text, string category, string difficulty)
        {
            Text = text;
            Category = category;
            Difficulty = difficulty;
        }

        public int GetPoints()
        {
            switch (Difficulty.ToLower())
            {
                case "easy":
                    return 10;

                case "medium":
                    return 20;

                case "hard":
                    return 30;

                default:
                    return 0;
            }
        }

        public abstract void Display();

        public abstract bool CheckAnswer(string answer);
    }
}

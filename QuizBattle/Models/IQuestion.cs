namespace QuizBattle.Models
{
    public interface IQuestion
    {
        void Display();

        bool CheckAnswer(string answer);
    }
}
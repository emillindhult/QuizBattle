namespace QuizBattle.Models;

public sealed class TrueFalseQuestion : Question, IQuestion
{
    public bool Correct { get; set; }

    public void Display()
    {
        Console.WriteLine(Text);
        Console.WriteLine("1. True");
        Console.WriteLine("2. False");
    }

    public bool CheckAnswer(string answer)
    {
        if (answer == "1" || answer.ToLower() == "true")
        {
            return Correct == true;
        }

        if (answer == "2" || answer.ToLower() == "false")
        {
            return Correct == false;
        }

        return false;
    }
}

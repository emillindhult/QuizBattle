namespace QuizBattle.Models;

public class Player
{
    public string Name { get; }

    public int Score { get; private set; }

    public int CorrectAnswers { get; private set; }

    public int Streak { get; private set; }

    public Player(string name)
    {
        Name = name;
        Score = 0;
        CorrectAnswers = 0;
        Streak = 0;
    }

    public void RegisterCorrectAnswer(int points)
    {
        Score += points;
        CorrectAnswers++;
        Streak++;
    }

    public void RegisterWrongAnswer()
    {
        Streak = 0;
    }
}
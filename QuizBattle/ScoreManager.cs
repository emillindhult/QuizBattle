namespace QuizBattle.Services;

public class ScoreManager
{
    // Returns base points based on difficulty
    public int GetBasePoints(string difficulty)
    {
        return difficulty.ToLower() switch
        {
            "easy" => 10,
            "medium" => 20,
            "hard" => 30,
            _ => throw new ArgumentException("Invalid difficulty")
        };
    }

    // Calculates the streak bonus
    public int GetStreakBonus(int streak)
    {
        if (streak <= 1)
        {
            return 0;
        }

        return Math.Min((streak - 1) * 5, 20);
    }

    // Calculates total points for a correct answer
    public int CalculatePoints(string difficulty, int streak)
    {
        int basePoints = GetBasePoints(difficulty);
        int bonus = GetStreakBonus(streak);

        return basePoints + bonus;
    }
}
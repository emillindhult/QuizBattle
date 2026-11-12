using QuizBattle.Models;

namespace QuizBattle;

public class CategorySelector
{
    public Category ChooseCategory(string playerName, List<Category> categories)
    {
        Console.WriteLine($"{playerName}, choose a category:");

        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i].Name} ({categories[i].QuestionCount()} questions left)");
        }

        int randomOption = categories.Count + 1;
        Console.WriteLine($"{randomOption}. Random");

        int choice = ConsoleInput.ReadInt("Choice: ", 1, randomOption);

        if (choice == randomOption)
        {
            return categories[Random.Shared.Next(categories.Count)];
        }

        return categories[choice - 1];
    }
}
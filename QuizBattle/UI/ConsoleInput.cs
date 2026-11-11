namespace QuizBattle;

public static class ConsoleInput
{
    
    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Please enter a number between {min} and {max}.");
        }
    }

    public static string ReadName(string prompt, List<string> takenNames)
    {
        while (true)
        {
            Console.Write(prompt);

            string? name = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("The name cannot be empty.");
            }
          
            else if (takenNames.Contains(name, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("That name is already taken. Please choose another one.");
            }
            else
            {
                return name;
            }
        }
    }
}
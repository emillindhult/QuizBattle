namespace QuizBattle;


public class MainMenu
{
    
    public void Run()
    {
        bool running = true;

        while (running)
        {
            ShowMenu();
            int choice = ConsoleInput.ReadInt("Choice: ", 1, 3);

            switch (choice)
            {
                case 1:
                    StartGame();
                    break;
                case 2:
                    
                    Console.WriteLine("Showing the highscore list...");
                    break;
                case 3:
                    Console.WriteLine("Goodbye!");
                    running = false;
                    break;
            }

            Console.WriteLine();
        }
    }

    
    private void ShowMenu()
    {
        Console.WriteLine("=== Quiz Battle ===");
        Console.WriteLine("1. Play  2. Highscores  3. Exit");
    }

    
    private void StartGame()
    {
        var setup = new GameSetup();
        GameSettings settings = setup.CreateSettings();

        var game = new Game(settings);
        game.Play();
    }
}
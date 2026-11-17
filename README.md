# QuizBattle

QuizBattle is a local multiplayer quiz game for the console, written in C# (.NET 10). Two to four players take turns choosing a category and answering questions. Harder questions give more points, and a streak of correct answers gives bonus points. At the end of the game the results are saved to a highscore list.

## Features

- 2–4 players, 1–10 rounds
- Choose a category each turn, or let the game pick a random one
- Multiple choice and true/false questions
- Points based on difficulty, plus a bonus for answering correctly several times in a row
- Questions are never repeated within a game
- Final ranking with winner (or shared victory)
- Top 10 highscore list saved between games

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

Check that it is installed:

```bash
dotnet --version
```

## Getting started

### 1. Clone the repository

```bash
git clone https://github.com/emillindhult/QuizBattle.git
cd QuizBattle
```

### 2. Build

```bash
dotnet build
```

### 3. Start the game

```bash
dotnet run --project QuizBattle
```

### Visual Studio

Open `QuizBattle.slnx` and press **F5** (or **Ctrl+F5** to run without debugging).

### Standalone build (optional)

```bash
dotnet publish QuizBattle -c Release -o publish
```

Then start the game with `publish\QuizBattle.exe` on Windows or `./publish/QuizBattle` on macOS/Linux.

## How to play

1. Choose an option in the main menu:
   ```
   === Quiz Battle ===
   1. Play  2. Highscores  3. Exit
   ```
2. **Play** – enter the number of players (2–4), each player's name (names must be unique) and the number of rounds (1–10).
3. Every round, each player takes one turn:
   - Choose a category from the list, or **Random**. The list shows how many questions are left in each category.
   - Answer the question by typing the number of your answer.
   - The game tells you if you were right, how many points you got and your total score.
4. After the last round the final ranking is shown and the results are saved to the highscore list. If all questions run out, the game ends early.
5. **Highscores** – shows the ten best results of all time.

## Scoring

| Difficulty | Points |
|------------|--------|
| Easy       | 10     |
| Medium     | 20     |
| Hard       | 30     |

**Streak bonus:** each correct answer in a row after the first gives +5 extra points, up to a maximum of +20.

| Correct in a row | Bonus |
|------------------|-------|
| 1                | +0    |
| 2                | +5    |
| 3                | +10   |
| 4                | +15   |
| 5 or more        | +20   |

A wrong answer gives 0 points and resets the streak.

The final ranking is sorted by score, then by number of correct answers. Players with the same top score share the victory.

## Highscores

Results are stored in `highscores.json` in the `Data` folder next to the built program (for example `QuizBattle/bin/Debug/net10.0/Data/highscores.json`). The file is created the first time a game is finished. The highscore list shows name, points, correct answers and date for the ten best results.

> Cleaning or deleting the `bin` folder also removes the saved highscores.

## Adding your own questions

All questions are stored in `QuizBattle/Data/questions.json`, grouped by category:

```json
{
  "categories": [
    {
      "name": "Programmering",
      "questions": [
        {
          "type": "multipleChoice",
          "text": "Vilket Git-kommando skapar en ny gren och byter till den?",
          "options": ["git branch -d", "git checkout -b", "git merge", "git clone"],
          "correctIndex": 1,
          "difficulty": "Medium"
        },
        {
          "type": "trueFalse",
          "text": "I C# används typen int för heltal.",
          "correct": true,
          "difficulty": "Easy"
        }
      ]
    }
  ]
}
```

| Field          | Description |
|----------------|-------------|
| `type`         | `"multipleChoice"` or `"trueFalse"` (or `1` / `0`). If left out, it is decided by whether `options` exists. |
| `text`         | The question. |
| `options`      | Multiple choice only: the answer options. |
| `correctIndex` | Multiple choice only: index of the correct option, starting at 0. |
| `correct`      | True/false only: `true` or `false`. |
| `difficulty`   | `"Easy"`, `"Medium"` or `"Hard"` (or `0` / `1` / `2`). |

The file is copied to the output folder when you build, so rebuild after editing it. New categories show up in the game automatically.

## Project structure

```
QuizBattle/
├── QuizBattle.slnx            Solution file
├── UML-diagram/               Class diagram
└── QuizBattle/
    ├── Program.cs             Entry point
    ├── Game.cs                Runs rounds, turns and final results
    ├── Player.cs              Name, score, streak and correct answers
    ├── ScoreManager.cs        Points and streak bonus
    ├── AnswerHandler.cs       Gives feedback and updates the player's score
    ├── GameResult.cs          Finds the winner(s)
    ├── Models/                Question types, categories and highscore entries
    ├── Data/                  questions.json, question loading and highscore storage
    └── UI/                    Main menu, game setup, category selection and console input
```

using QuizBattle.Data;

Console.WriteLine("Hello, World!");
var questionLoader = new QuestionLoader();

var questions = questionLoader.Load();

foreach (var question in questions)
{
    Console.WriteLine(question.ToString());
}
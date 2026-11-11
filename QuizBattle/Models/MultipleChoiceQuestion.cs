
using System;
using System.Collections.Generic;

namespace QuizBattle.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; } = [];
        public int CorrectIndex { get; set; }

        //public MultipleChoiceQuestion(
        //    string text,
        //    string difficulty,
        //    List<string> options,
        //    int correctIndex)
        //    : base(text, difficulty)
        //{
        //    Options = options;
        //    CorrectIndex = correctIndex;
        //}

        public void Display()
        {
            Console.WriteLine(Text);

            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }
        }

        public bool CheckAnswer(string answer)
        {
            if (int.TryParse(answer, out int selectedOption))
            {
                return selectedOption - 1 == CorrectIndex;
            }

            return false;
        }
    }
}

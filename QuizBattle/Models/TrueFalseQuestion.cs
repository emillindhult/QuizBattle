
using System;

namespace QuizBattle.Models
{
    public class TrueFalseQuestion : Question
    {
        public bool Correct { get; set; }

        public TrueFalseQuestion(
            string text,
            string category,
            string difficulty,
            bool correct)
            : base(text, category, difficulty)
        {
            Correct = correct;
        }

        public override void Display()
        {
            Console.WriteLine(Text);
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");
        }

        public override bool CheckAnswer(string answer)
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
}

using System.Collections.Generic;

namespace QuizBattle.Models
{
    public class Category
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }

        public Category(string name)
        {
            Name = name;
            Questions = new List<Question>();
        }

        public int QuestionCount()
        {
            return Questions.Count;
        }

        public bool HasQuestions()
        {
            return Questions.Count > 0;
        }
    }
}
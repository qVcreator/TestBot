using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Questions;

namespace TestBot.BLL
{
    public class UserTestData
    {
        public List<AbstractQuestion> Questions { get; private set; }

        public int QuestionNumber { get; private set; }

        public Dictionary<AbstractQuestion, List<string>> UserAnswers { get; private set; }

        public bool IsTest { get; private set; }

        public string Name { get; private set; }

        public UserTestData(List<AbstractQuestion> questions, bool type, string name)
        {
            Questions = questions;

            IsTest = type;

            QuestionNumber = 0;

            Name = name;

            UserAnswers = new Dictionary<AbstractQuestion, List<string>>();

            foreach (var question in questions)
            {
                UserAnswers.Add(question,new List<string>());
            }
        }

        public void QuestionNumberIncrement()
        {
            QuestionNumber++;
        }
    }
}

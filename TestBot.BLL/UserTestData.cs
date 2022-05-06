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

        public bool IsTest { get; private set; }

        public string Name { get; private set; }

        public UserTestData(List<AbstractQuestion> questions, bool type, string name)
        {
            Questions = questions;

            IsTest = type;

            QuestionNumber = 0;

            Name = name;
        }

        public void QuestionNumberIncrement()
        {
            QuestionNumber++;
        }
    }
}

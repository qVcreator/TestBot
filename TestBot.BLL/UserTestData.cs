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

        public UserTestData(List<AbstractQuestion> questions, bool type)
        {
            Questions = questions;

            IsTest = type;

            QuestionNumber = 0;
        }

        public void QuestionNumberIncrement()
        {
            QuestionNumber++;
        }
    }
}

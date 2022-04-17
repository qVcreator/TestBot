using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    abstract public class Question
    {
        public string Text { get; set; }

        public List<Answer> Answers { get; set; }

        public void ChangeText(string newText)
        {
            if(newText == null)
            {
                throw new Exception("newText must not be null");
            }
            Text = newText;
        }
    }

    public class Answer
    {
        public string Text { get; set; }
    }
}

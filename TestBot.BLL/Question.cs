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

        public List<string> Answers { get; set; }

        public int CountOfAnswer = 0;

        public void ChangeText(string newText)
        {
            if (newText == null)
            {
                throw new Exception("newText must not be null");
            }
            Text = newText;
        }

        public void AddAnswer(string textNewAnswer)
        {
            if (textNewAnswer == null)
            {
                throw new Exception("textNewAnswer must not be null");
            }

            bool isThisAnswerInListAnswer = false;

            for(int i = 0; i < Answers.Count; i++)
            {
                if (Answers[i] == textNewAnswer)
                {
                    isThisAnswerInListAnswer = true;
                }
            }

            if (isThisAnswerInListAnswer == true)
            {
                throw new Exception("textNewAnswer must be different from the text of the previous answers");
            }

            Answers.Add(textNewAnswer);
            CountOfAnswer++;
        }    
    }
}



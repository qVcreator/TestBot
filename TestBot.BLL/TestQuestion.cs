using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    abstract public class TestQuestion : Question
    {
        public string CorrectAnswer { get; set; }

        public bool IsCorrectAnswer = false;

        public void CheckAnswer(string answer)
        {
            if (answer == CorrectAnswer)
            {
                IsCorrectAnswer = true;
            }
        }

        public void ChangeCorrectAnswer(string newCorrectAnswer)
        {
            if (newCorrectAnswer == null)
            {
                throw new Exception("newCorrectAnswer must not be null");
            }

            CorrectAnswer = newCorrectAnswer;
        }
    }
}

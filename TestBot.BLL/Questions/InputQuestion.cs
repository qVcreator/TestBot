using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL.Questions
{
    public class InputQuestion:AbstractQuestion
    {
        public InputQuestion(string description)
        {
            if(string.IsNullOrEmpty(description))
            {
                throw new Exception("Invalid description");
            }
            else
            {
                Description = description;

                UserAnswers = new List<string> { };
            }
        }

        public InputQuestion(string description, List<string> correctAnswers)
        {
            if(string.IsNullOrEmpty(description))
            {
                throw new Exception("Invalid description");
            }
            else if (correctAnswers.Count == 1 || correctAnswers.Count > 4)
            {
                throw new Exception("Invalid amount of correct answers");
            }
            else
            {
                Description = description;

                CorrectAnswers = correctAnswers;

                UserAnswers = new List<string> { };
            }
        }
    }
}

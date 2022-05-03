using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Interfaces;

namespace TestBot.BLL.Questions
{
    public class OptionQuestion : AbstractQuestion
    {
        public OptionQuestion(string description, List<string> options, ITester tester)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Invalid description");
            }
            if (options.Count < 2 || options.Count > 4)
            {
                throw new ArgumentException("Invalid amount of options");
            }

            Description = description!;

            Options = options;

            UserAnswers = new List<string> { };

            _test = tester;
        }

        public OptionQuestion(string description, List<string> options, List<string> correctAnswers, ITester tester)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Invalid description");
            }
            if (options.Count < 2 || options.Count > 10)
            {
                throw new ArgumentException("Invalid amount of options");
            }
            if (correctAnswers.Count == 0 || correctAnswers.Count > options.Count)
            {
                throw new ArgumentException("Invalid amount of correct answers");
            }
            
            Description = description!;

            Options = options;

            CorrectAnswers = correctAnswers;

            UserAnswers = new List<string> { };

            _test = tester; 
        }

    }
}

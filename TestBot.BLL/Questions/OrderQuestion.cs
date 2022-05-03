using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Interfaces;

namespace TestBot.BLL.Questions
{
    public class OrderQuestion : AbstractQuestion
    {
        public OrderQuestion(string description, List<string> options, ITester tester, IKeyboardMaker keyboard)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Invalid description");
            }
            if (options.Count < 2 || options.Count > 4)
            {
                throw new ArgumentException("Invalid amount of options");
            }

            Description = description;

            Options = options;

            UserAnswers = new List<string> { };

            _test = tester;

            _keyboardMaker = keyboard;
        }

        public OrderQuestion(string description, List<string> options, List<string> correctAnswers, ITester tester, IKeyboardMaker keyboard)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new Exception("Invalid description");
            }
            if (options.Count < 2 || options.Count > 4)
            {
                throw new Exception("Invalid amount of options");
            }
            if (correctAnswers.Count == 0 || correctAnswers.Count > options.Count)
            {
                throw new Exception("Invalid amount of correct answers");
            }

            Description = description;

            Options = options;

            CorrectAnswers = correctAnswers;

            UserAnswers = new List<string> { };

            _test = tester;

            _keyboardMaker = keyboard;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Interfaces;
using TestBot.BLL.Interfaces.Implementations;

namespace TestBot.BLL.Questions
{
    public class InputQuestion : AbstractQuestion
    {
        public InputQuestion(string description, ITester tester, IKeyboardMaker keyboard)
        {
            if(string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Invalid description");
            }

            Description = description;

            UserAnswers = new List<string> { };

            _test = tester;

            _keyboardMaker = keyboard;

            Options = new List<string>();


        }

        public InputQuestion(string description, List<string> correctAnswers, ITester tester, IKeyboardMaker keyboard)
        {
            if(string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Invalid description");
            }
            if (!(correctAnswers.Count == 1))
            {
                throw new ArgumentException("Invalid amount of correct answers");
            }

            Description = description;

            CorrectAnswers = correctAnswers;

            UserAnswers = new List<string> { };

            _test = tester;

            _keyboardMaker = keyboard;

            Options = new List<string>();

        }
    }
}

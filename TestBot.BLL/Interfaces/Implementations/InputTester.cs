using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL.Interfaces.Implementations
{
    public class InputTester : ITester
    {
        public bool CheckAnswer(string input, List<string> correctAnswers)
        {
            if (correctAnswers.Contains(input.ToLower().Trim()))
            {
                return true;
            }

            return false;
        }

        public bool CheckInput(string input)
        {
            if (input == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(input.Trim()))
            {
                return true;
            }

            return false;
        }
    }
}


namespace TestBot.BLL.Interfaces.Implementations
{
    public class OptionTester : ITester
    {
        //Issue: ITester interface method signature using single [string: answer] for CheckAnswer() method
        //       by that we cant check question with few correct asnwers [OptionQuestion] because we'll receive [List <string> useranswers] instead of single [string: answer]

        //public bool CheckAnswer(List <string> userAnswers, List<string> correctAnswers)
        //{
        //    foreach (string item in userAnswers)
        //    {
        //        if (!(correctAnswers.Contains(item)))
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        public bool CheckAnswer(string input, List<string> correctAnswers) //Use by cycle in Bot part of code
        {
            if (correctAnswers.Contains(input.ToLower().Trim()))
            {
                return true;
            }

            return false;
        }

        public bool CheckAnswer(List<string> inputs, List<string> correctAnswers)
        {
            if (inputs.Count != correctAnswers.Count)
            {
                return false;
            }
            List<bool> isCorrect = new List<bool>();
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] == correctAnswers[i])
                {
                    isCorrect.Add(true);
                }
            }
            if(isCorrect.Count != correctAnswers.Count)
            {
                return false;
            }
            return true;
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

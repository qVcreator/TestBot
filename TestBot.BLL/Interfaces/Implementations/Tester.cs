
namespace TestBot.BLL.Interfaces.Implementations
{
    public class Tester : ITester
    {
        public bool CheckAnswer(string input, List<string> correctAnswers)
        {
            if (correctAnswers.Contains(input))
            {
                return true;
            }
            else
            {
                return false; 
            }
        }

        public bool CheckInput(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return true;
            }
            else
            {
                throw new Exception("Invalid input");
            }
        }
    }
}

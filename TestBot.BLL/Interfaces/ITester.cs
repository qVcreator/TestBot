
namespace TestBot.BLL.Interfaces
{
    public interface ITester
    {
        public bool CheckInput(string input);

        public bool CheckAnswer(List<string> input, List<string> correctAnswers);
    }
}

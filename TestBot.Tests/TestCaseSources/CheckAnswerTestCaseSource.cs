using System.Collections;
using System.Collections.Generic;

namespace TestBot.Tests.TestCaseSources
{
    public class CheckAnswerTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> correctAnswersFirst = new List<string>()
            {
                "рис", 
                "гречка", 
                "булгур", 
                "перловка"
            };

            List<string> correctAnswersSecond = new List<string>() {};

            bool expectedFirst = true;
            string inputFirst = "Булгур                       ";

            bool expectedSecond = true;
            string inputSecond = " гречка";

            bool expectedThird = false;
            string inputThird = "хречка";

            bool expectedFourth = false;
            string inputFourth = "гречка  228";

            bool expectedFifth = false;
            string inputFifth = "";



            yield return new object[] { expectedFirst, inputFirst, correctAnswersFirst };
            yield return new object[] { expectedSecond, inputSecond, correctAnswersFirst };
            yield return new object[] { expectedThird, inputThird, correctAnswersFirst };
            yield return new object[] { expectedFourth, inputFourth, correctAnswersFirst };
            yield return new object[] { expectedFifth, inputFifth, correctAnswersSecond };
        }
    }
}

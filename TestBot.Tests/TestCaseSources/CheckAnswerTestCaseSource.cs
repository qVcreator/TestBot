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
            string inputFirst = "булгур";

            bool expectedSecond = false;
            string inputSecond = "хречка";

            bool expectedThird = false;
            string inputThird = null;

            bool expectedFourth = false;
            string inputFourth = "";

            bool expectedFifth = false;
            string inputFifth = null;

            bool expectedSixth = false;
            string inputSixth = "";

            yield return new object[] { expectedFirst, inputFirst, correctAnswersFirst };
            yield return new object[] { expectedSecond, inputSecond, correctAnswersFirst };
            yield return new object[] { expectedThird, inputThird, correctAnswersFirst };
            yield return new object[] { expectedFourth, inputFourth, correctAnswersFirst };
            yield return new object[] { expectedFifth, inputFifth, correctAnswersSecond };
            yield return new object[] { expectedSixth, inputSixth, correctAnswersSecond };
        }
    }
}

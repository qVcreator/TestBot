using System.Collections;
using System.Collections.Generic;

namespace TestBot.Tests.TestCaseSources
{
    public class CheckInputTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<string> givenInputs = new()
            {
                "Барбоскины",
                "",
                " ",
                "    ",
                "    Барбоскины    "
            };

            List<bool> expectedBools = new()
            {
                true,
                false,
                false,
                false,
                true
            };

            for (int i = 0; i < givenInputs.Count; i++)
            {
                yield return new object[] {expectedBools[i], givenInputs[i]};
            }
        }
    }
}

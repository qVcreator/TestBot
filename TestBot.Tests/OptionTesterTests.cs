using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestBot.BLL.Interfaces.Implementations;
using TestBot.Tests.TestCaseSources;

namespace TestBot.Tests
{
    public class OptionTesterTests
    {
        [TestCaseSource(typeof(CheckAnswerTestCaseSource))]
        public void CheckAnswerTest(bool expected, string input, List<string> correctAnswers)
        {
            OptionTester tester = new OptionTester();

            bool actual = tester.CheckAnswer(input, correctAnswers);

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(CheckInputTestCaseSource))]
        public void CheckInputTest(bool expected, string input)
        {
            OptionTester tester = new OptionTester();

            bool actual = tester.CheckInput(input);

            Assert.AreEqual(expected, actual);
        }
    }
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestBot.BLL.Interfaces.Implementations;
using TestBot.Tests.TestCaseSources;

namespace TestBot.Tests
{
    public class OrderTesterTests
    {
        [TestCaseSource(typeof(CheckAnswerTestCaseSource))]
        public void CheckAnswerTest(bool expected, string input, List<string> correctAnswers)
        {
            OrderTester tester = new();

            bool actual = tester.CheckAnswer(input, correctAnswers);

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(CheckInputTestCaseSource))]
        public void CheckInputTest(bool expected, string input)
        {
            OrderTester tester = new();

            bool actual = tester.CheckInput(input);

            Assert.AreEqual(expected, actual);
        }
    }
}

﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestBot.BLL.Interfaces.Implementations;
using TestBot.Tests.TestCaseSources;

namespace TestBot.Tests
{
    public class TesterTests
    {
        [TestCaseSource(typeof(CheckAnswerTestCaseSource))]
        public void CheckAnswerTest(bool expected, string input, List<string> correctAnswers)
        {
            OptionTester tester = new OptionTester();

            bool actual = tester.CheckAnswer(input, correctAnswers);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(true, "Маргарин")]
        [TestCase(true, "Иваново")]
        public void CheckInputTest(bool expected, string input)
        {
            OptionTester tester = new OptionTester();

            bool actual = tester.CheckInput(input);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CheckInputTestWhenInvalidInputShouldThrowException(string input)
        {
            OptionTester tester = new OptionTester();

            Assert.Throws<Exception>(() => tester.CheckInput(input));

        }
    }
}

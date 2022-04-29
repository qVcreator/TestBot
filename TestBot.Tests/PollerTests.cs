using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Interfaces.Implementations;

namespace TestBot.Tests
{
    public class PollerTests
    {
        [TestCase(true, "Цыпленок")]
        public void CheckInputTest(bool expected, string input)
        {
            Poller poller = new Poller();

            bool actual = poller.CheckInput(input);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CheckInputTestWhenInvalidInputShouldThrowException(string input)
        {
            Poller poller = new Poller();

            Assert.Throws<Exception>(() => poller.CheckInput(input));

        }
    }
}

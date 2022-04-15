using NUnit.Framework;
using System;
using TestBot.BLL;
using TestBot.BLL.Mocks;

namespace TestBot.Tests
{
    public class UserTests
    {
        [TestCase(UserEnums.user3, "ОЛЕГ", "ОЛЕГ")]
        [TestCase(UserEnums.user1, "Кирилл", "Кирилл")]
        [TestCase(UserEnums.user2, "Никитка", "Никитка")]
        public void ChangeNameTest(UserEnums type, string newName, string expected)
        {
            User actual = UserMock.GetMock(type);
            actual.ChangeName(newName);
            Assert.AreEqual(expected, actual.Name);
        }

        [TestCase(UserEnums.user3, null)]
        [TestCase(UserEnums.user2, null)]
        [TestCase(UserEnums.user1, null)]
        public void ChangeNameTest_WhenNewNameIsNull_ShouldThrowArgumentNullException(UserEnums type, string newName)
        {
            User testUser = UserMock.GetMock(type);
            Assert.Throws<ArgumentNullException>(() => testUser.ChangeName(newName));
        }
    }
}
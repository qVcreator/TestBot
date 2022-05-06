using NUnit.Framework;
using System;
using TestBot.BLL;
using TestBot.BLL.Mocks;
using TestBot.BLL.Questions;

namespace TestBot.Tests
{
    public class TestTests
    {
        [TestCase("TessViolet", TestEnums.Test1, TestEnums.TestChangedName1)]
        [TestCase("TessViolet", TestEnums.Test2, TestEnums.TestChangedName2)]
        [TestCase("TessViolet", TestEnums.Test3, TestEnums.TestChangedName3)]
        public void ChangeNameTest(string newName, TestEnums actualType, TestEnums expectedType)
        {
            var actual = TestMock.GetMock(actualType);
            var expected = TestMock.GetMock(expectedType);

            actual.ChangeName(newName);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void ChangeNameTest_WnenNameIsNullOrEmpty_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => TestMock.GetMock(TestEnums.Test1).ChangeName(null));
            Assert.Throws<ArgumentException>(() => TestMock.GetMock(TestEnums.Test1).ChangeName(""));
        }

        [TestCase(QuestionEnums.OptionQuestion4, TestEnums.Test1, TestEnums.TestQuestionAdded1)]
        [TestCase(QuestionEnums.OptionQuestion4, TestEnums.Test2, TestEnums.TestQuestionAdded2)]
        [TestCase(QuestionEnums.OptionQuestion4, TestEnums.Test3, TestEnums.TestQuestionAdded3)]
        public void AddQuestionTest(QuestionEnums newQuestionType, TestEnums actualType, TestEnums expectedType)
        {
            var actual = TestMock.GetMock(actualType);
            var expected = TestMock.GetMock(expectedType);
            var newQuestion = QuestionMock.GetMock(newQuestionType);

            actual.AddQuestion(newQuestion);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddQuestionTest_WhenQuesstionIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentException>(() => TestMock.GetMock(TestEnums.Test1).AddQuestion(null));
        }

        [TestCase(QuestionEnums.OptionQuestion4, TestEnums.TestQuestionAdded1, TestEnums.Test1)]
        [TestCase(QuestionEnums.OptionQuestion4, TestEnums.TestQuestionAdded2, TestEnums.Test2)]
        [TestCase(QuestionEnums.OptionQuestion4, TestEnums.TestQuestionAdded3, TestEnums.Test3)]
        public void DeleteQuestionTest(QuestionEnums questionType, TestEnums actualType, TestEnums expectedType)
        {
            var actual = TestMock.GetMock(actualType);
            var expected = TestMock.GetMock(expectedType);
            var question = QuestionMock.GetMock(questionType);

            actual.DeleteQuestion(question);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteQuestionTest_WhenQuesstionIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentException>(() => TestMock.GetMock(TestEnums.Test1).DeleteQuestion((string)null));
            Assert.Throws<ArgumentException>(() => TestMock.GetMock(TestEnums.Test1).DeleteQuestion((AbstractQuestion)null));
        }

        [TestCase(GroupEnums.group3, TestEnums.Test1, TestEnums.TestGroupAdded1)]
        [TestCase(GroupEnums.group2, TestEnums.Test2, TestEnums.TestGroupAdded2)]
        [TestCase(GroupEnums.group1, TestEnums.Test3, TestEnums.TestGroupAdded3)]
        public void AddGroupTest(GroupEnums newGroupType, TestEnums actualType, TestEnums expectedType)
        {
            var actual = TestMock.GetMock(actualType);
            var expected = TestMock.GetMock(expectedType);
            var newGroup = GroupMock.GetMock(newGroupType);

            actual.AddGroup(newGroup);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddGroupTest_WhenGroupIsNull_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => TestMock.GetMock(TestEnums.Test1).AddGroup(null));
        }

        [TestCase(GroupEnums.group3, TestEnums.TestGroupAdded1, TestEnums.Test1)]
        [TestCase(GroupEnums.group2, TestEnums.TestGroupAdded2, TestEnums.Test2)]
        [TestCase(GroupEnums.group1, TestEnums.TestGroupAdded3, TestEnums.Test3)]
        public void DeleteGroupTest(GroupEnums groupType, TestEnums actualType, TestEnums expectedType)
        {
            var actual = TestMock.GetMock(actualType);
            var expected = TestMock.GetMock(expectedType);
            var group = GroupMock.GetMock(groupType);

            actual.DeleteGroup(group);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteGroupTest_WhenGroupIsNull_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => TestMock.GetMock(TestEnums.Test1).DeleteGroup(null));
        }
    }
}
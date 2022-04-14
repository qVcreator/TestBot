using NUnit.Framework;
using System;
using TestBot.BLL;
using TestBot.BLL.Mocks;


namespace TestBot.Tests
{
    public class GroupTests
    {
        [TestCase(GroupEnums.group1, "Паша", GroupEnums.group1DeleteFirst)]
        [TestCase(GroupEnums.group1, "СoolGuy2008", GroupEnums.group1DeleteMid)]
        [TestCase(GroupEnums.group1, "PinkyPie", GroupEnums.group1DeleteLast)]
        [TestCase(GroupEnums.EmptyGroup, "PinkyPie", GroupEnums.EmptyGroup)]
        public void DeleteUserTest_WhenParamIsString(GroupEnums type, string name, Group expected)
        {
            Group actual = GroupMock.GetMock(type);
            actual.DeleteUser(name);
            Assert.AreEqual(expected.Users, actual.Users);
        }

        [TestCase(GroupEnums.group1, null)]       
        [TestCase(GroupEnums.EmptyGroup, null)]       
        public void DeleteUserTest_WhenNameIsNull_ShouldThrowArgumentNullException(GroupEnums type, string name)
        {
            Group testGroup = GroupMock.GetMock(type);
            Assert.Throws<ArgumentNullException>(() => testGroup.DeleteUser(name));
        }

        [TestCase(GroupEnums.group1, 0, GroupEnums.group1DeleteFirst)]
        [TestCase(GroupEnums.group1, 1, GroupEnums.group1DeleteMid)]
        [TestCase(GroupEnums.group1, 2, GroupEnums.group1DeleteLast)]
        [TestCase(GroupEnums.EmptyGroup, 0, GroupEnums.EmptyGroup)]
        public void DeleteUserTest_WhenParamIsId(GroupEnums type, int id, Group expected)
        {
            Group actual = GroupMock.GetMock(type);
            actual.DeleteUser(id);
            Assert.AreEqual(expected.Users, actual.Users);
        }

        [TestCase(GroupEnums.group1, null)]
        [TestCase(GroupEnums.EmptyGroup, null)]
        public void DeleteUserTest_WhenNameIsNull_ShouldThrowArgumentNullException(GroupEnums type, int id)
        {
            Group testGroup = GroupMock.GetMock(type);
            Assert.Throws<ArgumentNullException>(() => testGroup.DeleteUser(id));
        }

        [TestCase(GroupEnums.group1, -1)]
        [TestCase(GroupEnums.group1, 6)]
        [TestCase(GroupEnums.group1, 3)]
        [TestCase(GroupEnums.EmptyGroup, 0)]
        public void DeleteUserTest_WhenIdIsOutOfRange_ShouldThrowArgumentOutOfRangeException(GroupEnums type, int id)
        {
            Group testGroup = GroupMock.GetMock(type);
            Assert.Throws<ArgumentNullException>(() => testGroup.DeleteUser(id));
        }

        [TestCase(GroupEnums.group1, 123456789, GroupEnums.group1DeleteFirst)]
        [TestCase(GroupEnums.group1, 4563217890, GroupEnums.group1DeleteMid)]
        [TestCase(GroupEnums.group1, 4057869231, GroupEnums.group1DeleteLast)]
        [TestCase(GroupEnums.EmptyGroup, 348953498, GroupEnums.EmptyGroup)]
        public void DeleteUserTest_WhenParamIsChatId(GroupEnums type, long chatId, Group expected)
        {
            Group actual = GroupMock.GetMock(type);
            actual.DeleteUser(chatId);
            Assert.AreEqual(expected.Users, actual.Users);
        }

        [TestCase(GroupEnums.group1, "Джамперы", GroupEnums.change1)]
        [TestCase(GroupEnums.EmptyGroup, "Бегуны", GroupEnums.EmptyGroup)]
        public void ChangeNameTest(GroupEnums type, string newName, GroupEnums expectedtype)
        {
            Group actual = GroupMock.GetMock(type);
            Group expected = GroupMock.GetMock(expectedtype);
            actual.ChangeName(newName);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(GroupEnums.group1, null)]
        [TestCase(GroupEnums.EmptyGroup, null)]
        public void ChangeNameTest_WhenNameIsNull_ShouldThrowArgumentNullException(GroupEnums type, string name)
        {
            Group testGroup = GroupMock.GetMock(type);
            Assert.Throws<ArgumentNullException>(() => testGroup.DeleteUser(name));
        }

        [TestCase(GroupEnums.add1, UserEnums.user3Change, GroupEnums.added1)]
        [TestCase(GroupEnums.add2, UserEnums.user3Change, GroupEnums.added2)]
        [TestCase(GroupEnums.add3, UserEnums.user3Change, GroupEnums.added3)]
        public void AddUserTest(GroupEnums type, UserEnums usertype, GroupEnums expectedtype)
        {
            Group actual = GroupMock.GetMock(type);
            Group expected = GroupMock.GetMock(expectedtype);
            User testUser = UserMock.GetMock(usertype);
            actual.AddUser(testUser);
            Assert.AreEqual(expected.Users, actual.Users);
        }

        [TestCase(GroupEnums.group1)]
        [TestCase(GroupEnums.EmptyGroup)]
        public void AddUserTest_WhenNameIsNull_ShouldThrowArgumentNullException(GroupEnums type)
        {
            Group testGroup = GroupMock.GetMock(type);
            User testUser = null;
            Assert.Throws<ArgumentNullException>(() => testGroup.AddUser(testUser));
        }
    }
}

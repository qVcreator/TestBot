using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Questions;

namespace TestBot.BLL.Mocks
{
    public static class TestMock
    {
        public static Test GetMock(TestEnums type)
        {
            switch (type)
            {
                case TestEnums.Test1:
                    return new Test("Test1",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group1) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion1)},
                        DateTime.Now, 2.0, null);
                    break;
                case TestEnums.Test2:
                    return new Test("Test2",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group2) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion2) },
                        DateTime.Now, 2.0, null);
                    break;
                case TestEnums.Test3:
                    return new Test("Test3",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group3) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.OptionQuestion3) },
                        DateTime.Now, 2.0, null);
                    break;
                default:
                    throw new Exception();
                    break;
            }
        }
    }
}

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
                        DateTime.Now, 2.0, null,false);
                    break;
                case TestEnums.Test2:
                    return new Test("Test2",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group2) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion2) },
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.Test3:
                    return new Test("Test3",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group3) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.OptionQuestion3) },
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.TestChangedName1:
                    return new Test("TessViolet",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group1) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion1) },
                        DateTime.Now, 2.0, null, false);
                case TestEnums.TestChangedName2:
                    return new Test("TessViolet",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group2) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion2) },
                        DateTime.Now, 2.0, null, false);
                    break;
                    break;
                case TestEnums.TestChangedName3:
                    return new Test("TessViolet",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group3) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.OptionQuestion3) },
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.TestQuestionAdded1:
                    return new Test("Test1",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group1) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion1),
                        QuestionMock.GetMock(QuestionEnums.OptionQuestion4)},
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.TestQuestionAdded2:
                    return new Test("Test2",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group2) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion2),
                        QuestionMock.GetMock(QuestionEnums.OptionQuestion4)},
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.TestQuestionAdded3:
                    return new Test("Test3",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group3) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.OptionQuestion3),
                        QuestionMock.GetMock(QuestionEnums.OptionQuestion4)},
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.TestGroupAdded1:
                    return new Test("Test1",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group1), GroupMock.GetMock(GroupEnums.group3) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion1) },
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.TestGroupAdded2:
                    return new Test("Test2",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group2), GroupMock.GetMock(GroupEnums.group2) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion2) },
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.TestGroupAdded3:
                    return new Test("Test3",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group3), GroupMock.GetMock(GroupEnums.group1) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.OptionQuestion3) },
                        DateTime.Now, 2.0, null, false);
                    break;
                case TestEnums.TestTelega:
                    return new Test("Test111",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group1) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion1),
                        QuestionMock.GetMock(QuestionEnums.OptionQuestion3),
                        QuestionMock.GetMock(QuestionEnums.InputQuestion2)},
                        DateTime.Now, 2.0, null, true);
                case TestEnums.RealTest:
                    return new Test("RealTest",
                        new List<Group> { GroupMock.GetMock(GroupEnums.group1) },
                        new List<AbstractQuestion>() { QuestionMock.GetMock(QuestionEnums.InputQuestion1),
                        QuestionMock.GetMock(QuestionEnums.OptionQuestion3),
                        QuestionMock.GetMock(QuestionEnums.InputQuestion2)},
                        DateTime.Now, 2.0, null, false);
                default:
                    throw new Exception();
                    break;
            }
        }
    }
}

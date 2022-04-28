using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Interfaces.Implementations;
using TestBot.BLL.Questions;
namespace TestBot.BLL.Mocks
{
    public static class QuestionMock
    {
        public static AbstractQuestion GetMock(QuestionEnums type)
        {
            switch (type)
            {
                case QuestionEnums.InputQuestion1:
                    return new InputQuestion("Как тебе наш бот?");
                    break;
                case QuestionEnums.InputQuestion2:
                    return new InputQuestion("2+2*2", new List<string>() { "6" });
                    break;
                case QuestionEnums.OptionQuestion3:
                    return new OrderQuestion("Расположите числа в порядке возрастания: 6,4,5,2 ",
                        new List<string>() { "2", "4", "6", "5" }, new Tester());
                    break;
                case QuestionEnums.OptionQuestion4:
                    return new OptionQuestion("Кто написал Евегений Онегин?",
                        new List<string>() { "Евгений", "Онегин", "А.С. Пушкин", "Бородино" },
                        new List<string>() { "A.C. Пушкин" });
            }
        }
    }
}
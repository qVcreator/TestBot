using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL.Mocks
{
    public static class UserMock
    {
        public static User GetMock(UserEnums type)
        {
            switch (type)
            {
                case UserEnums.user1:
                    return new User("Паша", 123456789);
                    break;
                case UserEnums.user2:
                    return new User("СoolGuy2008", 4563217890);
                    break;
                case UserEnums.user3:
                    return new User("PinkyPie", 4057869231);
                    break;
                case UserEnums.user4:
                    return new User("YEEET", 3847562910);
                    break;
                case UserEnums.user5:
                    return new User("ElderComb", 5374897102);
                    break;
                case UserEnums.user6:
                    return new User("PieEater", 9238572394);
                    break;
                case UserEnums.user7:
                    return new User("PieMaker42", 3457092118);
                    break;
                case UserEnums.user1Change:
                    return new User("Петя", 3457092118);
                    break;
                case UserEnums.user2Change:
                    return new User("Илья", 3457092118);
                    break;
                case UserEnums.user3Change:
                    return new User("Максим", 3457092118);
                    break;
                default: throw new Exception();
            }
        } 
            
    }
}

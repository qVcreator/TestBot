using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL.Mocks
{
    public static class GroupMock
    {
        public static Group GetMock(GroupEnums type)
        {
            List<User> users = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user1));
            users.Add(UserMock.GetMock(UserEnums.user2));
            users.Add(UserMock.GetMock(UserEnums.user3));

            List<User> usersDeleteFirst = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user2));
            users.Add(UserMock.GetMock(UserEnums.user3));

            List<User> usersDeleteLast = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user1));
            users.Add(UserMock.GetMock(UserEnums.user2));

            List<User> usersDeleteMid = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user1));
            users.Add(UserMock.GetMock(UserEnums.user3));

            List<User> EmptyUser = new List<User>();

            List<User> addOn2 = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user1));
            users.Add(UserMock.GetMock(UserEnums.user2));

            List<User> addedOn2 = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user1));
            users.Add(UserMock.GetMock(UserEnums.user2));
            users.Add(UserMock.GetMock(UserEnums.user3Change));


            List<User> addOn3 = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user1));
            users.Add(UserMock.GetMock(UserEnums.user2));
            users.Add(UserMock.GetMock(UserEnums.user3));

            List<User> addedOn3 = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user1));
            users.Add(UserMock.GetMock(UserEnums.user2));
            users.Add(UserMock.GetMock(UserEnums.user3));
            users.Add(UserMock.GetMock(UserEnums.user3Change));

            List<User> addedOnEmpty = new List<User>();
            users.Add(UserMock.GetMock(UserEnums.user3));

            List<User> users2 = new List<User>();
            users2.Add(UserMock.GetMock(UserEnums.user4));
            users2.Add(UserMock.GetMock(UserEnums.user5));
            users2.Add(UserMock.GetMock(UserEnums.user6));
            users2.Add(UserMock.GetMock(UserEnums.user7));

            switch (type)
            {
                case GroupEnums.group1:
                    return new Group("Цыплята", users);
                    break;
                case GroupEnums.group2:
                    return new Group("Ковбои");
                    break;
                case GroupEnums.group3:
                    return new Group("Динозавры", users2);
                    break;
                case GroupEnums.group1DeleteFirst:
                    return new Group("Цыплята", usersDeleteFirst);
                    break;
                case GroupEnums.group1DeleteLast:
                    return new Group("Цыплята", usersDeleteLast);
                    break;
                case GroupEnums.group1DeleteMid:
                    return new Group("Цыплята", usersDeleteMid);
                    break;
                case GroupEnums.EmptyGroup:
                    return new Group("Цыплята", EmptyUser);
                    break;
                case GroupEnums.change1:
                    return new Group("Джамперы", users);
                    break;
                case GroupEnums.add1:
                    return new Group("Цыплята", addOn3);
                    break;
                case GroupEnums.added1:
                    return new Group("Цыплята", addedOn3);
                    break;
                case GroupEnums.add2:
                    return new Group("Цыплята", addOn2);
                    break;
                case GroupEnums.added2:
                    return new Group("Цыплята", addedOn2);
                    break;
                case GroupEnums.add3:
                    return new Group("Цыплята", EmptyUser);
                    break;
                case GroupEnums.added3:
                    return new Group("Цыплята", addedOnEmpty);
                    break;

                default: throw new Exception();
            }
        }
    }
}

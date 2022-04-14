﻿using System;
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
                default: throw new Exception();
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL.GroupModels
{
    public class GroupModel
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public GroupModel()
        {

        }

        public GroupModel(Group group)
        {
            Name = group.Name;
            Users = group.Users;
        }
    }
}

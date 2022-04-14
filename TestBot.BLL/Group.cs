using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    public class Group
    {
        public string Name { get; private set; }

        public List<User> Users { get; private set; }

        public Group()
        {
            Users = new List<User>();
            Name = "Новая группа";
        }

        public Group(string name)
        {
            Name = name;
            Users = new List<User>();
        }

        public Group(string name, List<User> users)
        {
            Name = name;
            Users = users;
        }

        public void DeleteUser(string name)
        {
            foreach (User item in Users)
            {
                if(item.Name == name)
                {
                    Users.Remove(item);
                }
            }
        }

        public void DeleteUser(int id)
        {
            Users.RemoveAt(id);
        }

        public void DeleteUser(long chatId)
        {
            foreach (User item in Users)
            {
                if (item.ChatId == chatId)
                {
                    Users.Remove(item);
                }
            }
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void AddUser(User newUser)
        {
            Users.Add(newUser);
        }
    }
}

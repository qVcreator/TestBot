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
            if (name == null)
            {
                Name = "Новый пользователь";
            }
            else 
            {
                Name = name;
            }
        }

        public Group(string name, List<User> users)
        {
            if (name == null)
            {
                Name = "Новый пользователь";
            }
            else
            {
                Name= name;
            }

            if (users == null)
            {
                Users = new List<User>();
            }
            else
            {
                Users = users;   
            }
        }

        public void DeleteUser(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

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
            if(id < 0 || id > Users.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
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
            if (newName == null)
            {
                throw new ArgumentNullException(nameof(newName));
            }
            Name = newName;
        }

        public void AddUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }
            Users.Add(newUser);
        }
    }
}
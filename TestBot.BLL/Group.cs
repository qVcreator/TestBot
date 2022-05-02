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
                Name = name;
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
                throw new ArgumentNullException("name");
            }

            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Name == name)
                {
                    Users.RemoveAt(i);
                }
            }
        }

        public void DeleteUser(int id)
        {
            if (id < 0 || id > Users.Count || Users.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            Users.RemoveAt(id);
        }

        public void DeleteUser(long chatId)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].ChatId == chatId)
                {
                    Users.RemoveAt(i);
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

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Group))
            {
                return false;
            }

            Group group = (Group)obj;

            if (group.Name != Name)
            {
                return false;
            }

            if (group.Users.Count != Users.Count)
            {
                return false;
            }

            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Name != group.Users[i].Name)
                {
                    return false;
                }

                if (Users[i].ChatId != group.Users[i].ChatId)
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string str = $"[{Name}: ";
            
            for (int i = 0; i < Users.Count; i++)
            {
                str += $"[{Users[i].Name}; {Users[i].ChatId}]";
            }

            str += "]";

            return str;
        }
    }   
}
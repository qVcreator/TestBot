using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    public class Test
    {
        public string Name { get; set; }

        //public List<Question> Questions { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public double TestDuration { get; set; }

        public List <Group> Groups { get; set; }


        public void ChangeName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Name entered incorrectly");
            }

            Name = name;
        }

        //public void AddQuestion(string question)

        //public void DeleteQuestion(int id)

        //public void ChangeQuestion(int id)

        public void DeleteGroup(int id, List <Group> Groups)
        {
            if(id < 0 || Groups.Count == 0 || id > Groups.Count)
            {
                throw new ArgumentException("Id entered incorrectly");
            }

            Groups.RemoveAt(id);
        }



    }
}

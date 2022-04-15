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
            Name = name;
        }

        //public void AddQuestion(string question)

        //public void DeleteQuestion(int id)

        //public void ChangeQuestion(int id)

        public void DeleteGroup(int id, List <Group> Groups)
        {
            Groups.Remove(Groups[id]);
        }



    }
}

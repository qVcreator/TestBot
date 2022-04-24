using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Questions;

namespace TestBot.BLL
{
    public class Test
    {
        public string Name { get; set; }

        public List<Group> Groups { get; set; }

        public List<AbstractQuestion> Questions { get; set; }

        public DateTime StartTime { get; set; }
        
        public double TestDuration { get; set; }

        public DateTime FinishTime { get; set; }

        public void ChangeName() { }

        public void AddQuestion() { }

        public void DeleteQuestion(int id) { }

        public void ChangeQuestion(int id) { }

        public void DeleteGroup(int id) { }


    }
}

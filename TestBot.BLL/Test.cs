using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    public class AbstractQuestion
    {
        public string Text { get; set; }

        public List <string> Answers { get; set; }

        public AbstractQuestion(string text, List<string> answers)
        {
            Text = text;
            Answers = answers;
        }
    }

    public class Test
    {
        public string Name { get; set; }
        
        public List <AbstractQuestion> Questions { get; set; }

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

        public void AddQuestion(AbstractQuestion newQuestion)
        {
            if (newQuestion == null)
            {
                throw new ArgumentNullException(nameof(newQuestion));
            }
            Questions.Add(newQuestion);
        }

        public void DeleteQuestion(int id)
        {
            Questions.RemoveAt(id);
        }

        public void ChangeQuestion(string text, int id)
        {
            Questions[id].Text = text;  
        }

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

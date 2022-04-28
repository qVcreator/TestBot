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
        public List<Group> Groups { get; set; }
        public List<AbstractQuestion> Questions { get; set; }
        public DateTime StartTime { get; set; }
        public double? TestDuration { get; set; }
        public DateTime FinishTime { get; set; }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void AddQuestion(AbstractQuestion newQuestion)
        {
            Questions.Add(newQuestion);
        }

        public void DeleteQuestion(AbstractQuestion question)
        {
            for (int i = 0; i < Questions.Count; i++)
            {
                if (Questions[i].Description == question.Description)
                {
                    Questions.RemoveAt(i);
                }
            }
        }

        public void ChangeQuestion(AbstractQuestion question, string newDescription)
        {
            for (int i = 0; i < Questions.Count; i++)
            {
                if (Questions[i].Description == question.Description)
                {
                    Questions[i].Description = newDescription;
                }
            }
        }

        public void AddGroup(Group newGroup)
        {
            Groups.Add(newGroup);
        }

        public void DeleteGroup(Group group)
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Name == group.Name)
                {
                    Groups.RemoveAt(i);
                }
            }
        }
    }
}

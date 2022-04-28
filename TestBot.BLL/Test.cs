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
        public double? TestDuration { get; set; }
        public DateTime FinishTime { get; set; }

        public void ChangeName(string newName)
        {
            if(newName is null || newName == "")
            {
                throw new ArgumentException("newName");
            }

            Name = newName;
        }

        public void AddQuestion(AbstractQuestion newQuestion)
        {
            if(newQuestion is null)
            {
                throw new ArgumentException("newQuestion");
            }

            Questions.Add(newQuestion);
        }

        public void DeleteQuestion(AbstractQuestion question)
        {
            if (question is null)
            {
                throw new ArgumentException("question");
            }

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
            if (question is null)
            {
                throw new ArgumentException("question");
            }
            if(newDescription is null || newDescription == "")
            {
                throw new ArgumentException("newDescription");
            }

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
            if (newGroup is null)
            {
                throw new ArgumentException("newGroup");
            }

            Groups.Add(newGroup);
        }

        public void DeleteGroup(Group group)
        {
            if (group is null)
            {
                throw new ArgumentException("group");
            }

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

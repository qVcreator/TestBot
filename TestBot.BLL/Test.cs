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
        public string Name { get; private set; }
        public List<Group> Groups { get; private set; }
        public List<AbstractQuestion> Questions { get; private set; }
        public DateTime StartTime { get; private set; }
        public double TestDuration { get; private set; }
        public DateTime? FinishTime { get; private set; }

        public bool IsTest { get; private set; }

        public Test()
        {

        }

        public Test(string name, List<Group> groups, List<AbstractQuestion> questions,
            DateTime startTime, double testDuration, DateTime? finishTime, bool type)
        {
            Name = name;
            Groups = groups;
            Questions = questions;
            StartTime = startTime;
            IsTest = type;
            if(testDuration == 0)
            {
                FinishTime = finishTime;
            }
            else
            {
                FinishTime = StartTime.AddHours(TestDuration);
            }
        }

        public Test(string name, List<Group> groups, double testDuration, bool type)
        {
            Name = name;
            Groups = groups;
            TestDuration = testDuration;
            StartTime = DateTime.Now;
            FinishTime = StartTime.AddHours(TestDuration);
            Questions = new List<AbstractQuestion>();
            IsTest = type;
        }

        public Test(string name, List<Group> groups, DateTime finishTime, bool type)
        {
            Name = name;
            Groups = groups;
            StartTime = DateTime.Now;
            FinishTime = finishTime;
            Questions = new List<AbstractQuestion>();
            IsTest = type;
        }

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

        public void DeleteQuestion(string description)
        {
            if (description is null)
            {
                throw new ArgumentException("question");
            }

            for (int i = 0; i < Questions.Count; i++)
            {
                if (Questions[i].Description == description)
                {
                    Questions.RemoveAt(i);
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

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Test))
            {
                return false;
            }

            Test test = (Test)obj;

            if (test.Name != Name)
            {
                return false;
            }

            if (test.Groups.Count != Groups.Count)
            {
                return false;
            }

            if(test.Questions.Count != Questions.Count)
            {
                return false;
            }

            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Name != test.Groups[i].Name)
                {
                    return false;
                }
            }

            for (int i = 0; i < Questions.Count; i++)
            {
                if(Questions[i].Description != test.Questions[i].Description)
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string str = $"[{Name}: ";

            str += "Группы: {";
            for (int i = 0; i < Groups.Count; i++)
            {
                str += $"{Groups[i].Name}, ";
            }
            str += "}\n";

            str += "Вопросы: {";
            for (int i = 0; i < Questions.Count; i++)
            {
                str += $"{Questions[i].Description}, ";
            }
            str += "}";

            str += "]";

            return str;
        }
    }
}

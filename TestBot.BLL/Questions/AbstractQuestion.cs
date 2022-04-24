using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBot.BLL.Interfaces;

namespace TestBot.BLL.Questions
{
    public abstract class AbstractQuestion
    {
        public string Description { get; set; }

        public List<string> Options { get; set; }

        public List<string> CorrectAnswers { get; set; }

        public List<string> UserAnswers { get; set; }

        public bool IsTest { get; set; }

        private ITester _test;

        private IPoller _poll;

        public void ChangeText(string questionText)
        {
            questionText = questionText.Trim();

            if(string.IsNullOrEmpty(questionText))
            {
                throw new Exception("Invalid argument");
            }
            else
            {
                Description = questionText;
            }
        }

        public void AddOption(string optionText)
        {
            optionText = optionText.Trim();

            if(string.IsNullOrEmpty(optionText))
            {
                throw new Exception("Invalid argument");
            }
            else if (Options.Contains(optionText))
            {
                throw new Exception("Current option already exists in options list");
            }
            else
            {
                Options.Add(optionText);
            }
        }

        public void DeleteOption(string optionText)
        {
            if (Options.Contains(optionText))
            {
                Options.Remove(optionText);
            }
            else
            {
                throw new Exception("Current option does not contains in options list");
            }
        }
    }
}

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

        protected ITester _test;

        public void ChangeText(string questionText)
        {
            questionText = questionText.Trim();

            if(string.IsNullOrEmpty(questionText))
            {
                throw new Exception("Invalid argument");
            }

            Description = questionText;
          
        }

        public void AddOption(string optionText)
        {
            optionText = optionText.Trim();

            if(string.IsNullOrEmpty(optionText))
            {
                throw new Exception("Invalid argument");
            }
            if (Options.Contains(optionText))
            {
                throw new Exception("Current option already exists in options list");
            }

            Options.Add(optionText);

        }

        public void DeleteOption(string optionText)
        {
            if (!Options.Contains(optionText))
            {
                throw new Exception("Current option does not contains in options list");
            }
            
            Options.Remove(optionText);
        }
    }
}

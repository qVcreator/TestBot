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

            if(questionText != null || questionText != "")
            {
                Description = questionText!;
            }
        }

        public void AddOption(string optionText)
        {
            optionText = optionText.Trim();

            if(optionText != null || optionText != "")
            {
                Options.Add(optionText!);
            }
        }

        public void DeleteOption() { }
    }
}

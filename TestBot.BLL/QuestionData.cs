using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    public class QuestionData
    {
        public string Description { get; set; }

        public string Type { get; set; }

        public QuestionData(string type, string description)
        {

            Type = type;

            Description = description;
        }
    }
}

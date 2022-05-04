using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    public class QuestionData
    {
        public int Number { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public QuestionData(string type, string description, int number)
        {
            Type = type;

            Description = description;

            Number = number;
        }
    }
}

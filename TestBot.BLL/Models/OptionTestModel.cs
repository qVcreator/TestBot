using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL.Models
{
    public class OptionTestModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Answer { get; set; }

        public OptionTestModel(int id, string name, string answer)
        {
            Id = id;
            Answer = answer;
            Name = name;
        }
    }
}

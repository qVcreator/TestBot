using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    public class Report
    {
        public User User { get; private set; }

        public List<AbstractQuestion> Questions { get; private set; }

        public List<string> UserAnswers { get; private set; }

        public void GetReport()
        {

        }
    }
}

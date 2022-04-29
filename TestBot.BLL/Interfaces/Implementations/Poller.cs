using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL.Interfaces.Implementations
{
    public class Poller : IPoller
    {
        public bool CheckInput(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return true;
            }
            else
            {
                throw new Exception("Invalid input");
            }
        }
    }
}

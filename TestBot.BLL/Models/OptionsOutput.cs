using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL.Models
{
    public class OptionsOutput
    {
        private static OptionsOutput instance;

        public List<OptionTestModel> Options { get; private set; }
        private OptionsOutput()
        { }

        public static OptionsOutput GetOptionsOutput()
        {
            if (instance == null)
            {
                instance = new OptionsOutput();
                instance.Options = new List<OptionTestModel>();
            }

            return instance;
        }

        public void AddOptions(OptionTestModel option)
        {
            instance.Options.Add(option);
        }
    }
}

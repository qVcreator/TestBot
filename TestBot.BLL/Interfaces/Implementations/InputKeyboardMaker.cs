using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestBot.BLL.Interfaces.Implementations
{
    public class InputKeyboardMaker : IKeyboardMaker
    {
        public IReplyMarkup? GetKeyboard(List<string> options)
        {
            return null;
        }
    }
}

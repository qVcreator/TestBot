using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestBot.BLL.Interfaces.Implementations
{
    public class OrderKeyboardMaker : IKeyboardMaker
    {
        private List<InlineKeyboardButton> GetCopy(List<InlineKeyboardButton> list)
        {
            List<InlineKeyboardButton> tmp = new List<InlineKeyboardButton>();
            
            foreach (var button in list)
            {
                tmp.Add(button);
            }

            return tmp;
        }

        public IReplyMarkup? GetKeyboard(List<string> options)
        {
            int buttonsQuantity = options.Count();
            InlineKeyboardMarkup inlineKeyboardMarkup;
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();

            if (buttonsQuantity % 2 == 0)
            {
                List<InlineKeyboardButton> stringButtons = new List<InlineKeyboardButton>();

                for (int i = 0; i < buttonsQuantity; i++)
                {
                    if (stringButtons.Count() == 2)
                    {

                        buttons.Add(GetCopy(stringButtons));
                        stringButtons.Clear();
                    }

                    stringButtons.Add(InlineKeyboardButton.WithCallbackData(options[i], options[i]));
                }

                if (stringButtons.Count() == 2)
                {
                    buttons.Add(GetCopy(stringButtons));

                    stringButtons.Clear();
                }

                stringButtons.Add(InlineKeyboardButton.WithCallbackData("Подтвердить", "Подтвердить"));

                buttons.Add(GetCopy(stringButtons));
                
                inlineKeyboardMarkup = new InlineKeyboardMarkup(buttons);
            }
            else
            {
                List<InlineKeyboardButton> stringButtons = new List<InlineKeyboardButton>();

                for (int i = 0; i < buttonsQuantity; i++)
                {
                    if (stringButtons.Count() == 3)
                    {
                        buttons.Add(GetCopy(stringButtons));

                        stringButtons.Clear();
                    }

                    stringButtons.Add(InlineKeyboardButton.WithCallbackData(options[i], options[i]));

                }

                buttons.Add(GetCopy(stringButtons));

                stringButtons.Clear();

                stringButtons.Add(InlineKeyboardButton.WithCallbackData("Подтвердить", "Подтвердить"));
                buttons.Add(GetCopy(stringButtons));


                inlineKeyboardMarkup = new InlineKeyboardMarkup(buttons);
            }

            return inlineKeyboardMarkup;
        }
    }
}

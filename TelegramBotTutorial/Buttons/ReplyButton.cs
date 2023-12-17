using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotTutorial.ReplyButtons
{
    public class Replybutton
    {
        public static ReplyKeyboardMarkup MainButtons()
        {
            // We wanna have 3 rows of buttons in our bot
            KeyboardButton[][] list =
            {
                     new KeyboardButton[] { new KeyboardButton("Button1"), new KeyboardButton("Button2"), new KeyboardButton("Button3") },
                     new KeyboardButton[] { new KeyboardButton("Button4") },
                     new KeyboardButton[] { new KeyboardButton("Button5"), new KeyboardButton("Button6") }
                    };

            var keyboard = new ReplyKeyboardMarkup(list);

            return keyboard;
        }
        public static ReplyKeyboardMarkup Button1()
        {
            KeyboardButton[][] list =
                      {
                     new KeyboardButton[] { new KeyboardButton("Button7") },
                     new KeyboardButton[] {  new KeyboardButton("Button8"), new KeyboardButton("Button9") },
                     new KeyboardButton[] { new KeyboardButton("Back") }
                    };

            var keyboard = new ReplyKeyboardMarkup(list);

            return keyboard;
        }
    }
}

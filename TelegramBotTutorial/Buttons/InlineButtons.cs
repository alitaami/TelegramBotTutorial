using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotTutorial.Buttons
{
    public class InlineButtons
    {
        public static InlineKeyboardMarkup Button2()
        {
            InlineKeyboardButton[][] list =
                     {
                     new InlineKeyboardButton[] { new InlineKeyboardButton("Yes") /* we should have this */ { CallbackData = "button9" },new InlineKeyboardButton("No") /* we should have this */ { CallbackData = "button10" } },
                     new InlineKeyboardButton[] { new InlineKeyboardButton("Back") { CallbackData = "back"} }
                    };

            var keyboard = new InlineKeyboardMarkup(list);

            return keyboard;
        }
        public static InlineKeyboardMarkup YesButton()
        {
            InlineKeyboardButton[][] list =
                     {
                     new InlineKeyboardButton[] { new InlineKeyboardButton("Yes1") /* we should have this */ { CallbackData = "button11" },new InlineKeyboardButton("No1") /* we should have this */ { CallbackData = "button12" } },
                     new InlineKeyboardButton[] { new InlineKeyboardButton("Back") { CallbackData = "back1"} }
                    };

            var keyboard = new InlineKeyboardMarkup(list);

            return keyboard;
        }
    }
}

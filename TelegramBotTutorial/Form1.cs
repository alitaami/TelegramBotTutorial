using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTutorial.ReplyButtons;
using System.Collections.Generic;
using TelegramBotTutorial.Buttons;

namespace TelegramBotTutorial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var bot = new Telegram.Bot.TelegramBotClient(txtToken.Text.Trim());

            toolStripStatusLabel2.Text = "Online";
            toolStripStatusLabel2.ForeColor = System.Drawing.Color.Green;

            // configs for receiving options 
            var receivingOptions = new ReceiverOptions()
            {
                AllowedUpdates = new UpdateType[]
                {
                 UpdateType.Message,
                 UpdateType.CallbackQuery
                }
            };

            bot.StartReceiving(updateHandler: updateHandler, errorHandler, receivingOptions);
        }

        private async Task updateHandler(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            if (update.CallbackQuery != null)
            {
                await ManageCallbackQueryAsync(bot, update, cancellationToken);
            }
            else
            {
                var text = update.Message.Text.ToLower();
                var chatId = update.Message.Chat.Id;
                var sendDateTime = update.Message.Date;
                var firstName = update.Message.From.FirstName.ToString();

                switch (text)
                {
                    case "/start":
                        await bot.SendTextMessageAsync(chatId, $"Hi {firstName}", /* user will see the buttons when it wanna use the bot */  replyMarkup: Replybutton.MainButtons());
                        break;

                    case "button1":
                        await bot.SendTextMessageAsync(chatId, $"You clicked on Button1", /* user will see the buttons when it wanna use the bot */  replyMarkup: Replybutton.Button1());
                        break;

                    case "button2":
                        await bot.SendTextMessageAsync(chatId, $"We are testing inline buttons", /* user will see the buttons when it wanna use the bot */  replyMarkup: InlineButtons.Button2());
                        break;

                    case "back":
                        await bot.SendTextMessageAsync(chatId, "Hi", /* user will see the buttons when it wanna use the bot */  replyMarkup: Replybutton.MainButtons());
                        break;

                    default:
                        await bot.SendTextMessageAsync(chatId, "I do not understand what you said!");
                        break;
                }
            }
        }

        private async Task ManageCallbackQueryAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            var text = update.CallbackQuery.Data.ToLower();
            var chatId = update.CallbackQuery.Message.Chat.Id;

            switch (text)
            {
                case "back1":
                    await bot.SendTextMessageAsync(chatId, "test for inline keyboard", replyMarkup: InlineButtons.Button2());
                    break;

                case "back":
                    await bot.SendTextMessageAsync(chatId, "test for inline keyboard", replyMarkup: Replybutton.MainButtons());
                    break;

                case "button9":
                    await bot.SendTextMessageAsync(chatId, "thanks for your vote", replyMarkup: InlineButtons.YesButton());
                    break;
                
                case "button11":
                    await bot.SendTextMessageAsync(chatId, "thanks for your vote");
                    break;
            }
        }

        private async Task errorHandler(ITelegramBotClient bot, Exception ex, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void txtToken_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
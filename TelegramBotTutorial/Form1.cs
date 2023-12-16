using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

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
            var text = update.Message.Text.ToLower();
            var chatId = update.Message.Chat.Id;
            var sendDateTime = update.Message.Date;
            var firstName = update.Message.From.FirstName.ToString();

            switch (text)
            {
                case "/start":

                    // We wanna have 3 rows of buttons in our bot
                    KeyboardButton[][] list =
                    {
                     new KeyboardButton[] { new KeyboardButton("Button1"), new KeyboardButton("Button2"), new KeyboardButton("Button3") },
                     new KeyboardButton[] { new KeyboardButton("Button4") },
                     new KeyboardButton[] { new KeyboardButton("Button5"), new KeyboardButton("Button6") }
                    };

                    var keyboard = new ReplyKeyboardMarkup(list);

                    await bot.SendTextMessageAsync(chatId, $"Hi {firstName}", /* user will see the buttons when it wanna use the bot */  replyMarkup: keyboard);
                    break;

                default:
                    await bot.SendTextMessageAsync(chatId, "I do not understand what you said! ");
                    break;

            }
        }
        private async Task errorHandler(ITelegramBotClient bot, Exception ex, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
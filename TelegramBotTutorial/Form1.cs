using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

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
                    await bot.SendTextMessageAsync(chatId, $"Hi {firstName}");
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
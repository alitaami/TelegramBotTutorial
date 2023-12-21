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
                 UpdateType.CallbackQuery,
                 UpdateType.Poll,
                 UpdateType.PollAnswer
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
            else if (update.Poll != null)
            {
                // We can have access to all information of the poll
            }
            else if (update.PollAnswer != null)
            {
                // We can have access to data of quiz polls ( polls those have answers)
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
                        
                    case "button4":
                        
                        //var image1 = InputFile.FromUri("https://picsum.photos/200/300");
                        //var message = await bot.SendPhotoAsync(chatId, image1, caption:"file sent by url");

                        ////we can get file id from the message
                        //// if we have file in the past,  telegram recomend this to use  to donot directly upload a file
                        //var image2 = InputFile.FromFileId(message.Photo[0].FileId);
                        //await bot.SendPhotoAsync(chatId, image2, caption: "file sent by fileId");

                        //var stream = new StreamReader("1.jpg").BaseStream;
                        //await bot.SendPhotoAsync(chatId, InputFile.FromStream(stream), caption: "file sent by stream");

                        //await bot.SendVoiceAsync(chatId, InputFile.FromUri(""), caption: "Voice sent by url");

                        //var stream1 = new StreamReader("2.mp3").BaseStream;
                        //await bot.SendVoiceAsync(chatId, InputFile.FromStream(stream1), caption: "Voice sent by stream");
                        //await bot.SendAudioAsync(chatId, InputFile.FromStream(stream1), caption: "audio sent by url");

                        //var stream2 = new StreamReader("3.mp4").BaseStream;
                        //await bot.SendVideoAsync(chatId, InputFile.FromStream(stream2), caption: "Video sent by stream");

                        await bot.SendContactAsync(chatId, "+989301327634", "Ali", lastName: "Taami",replyToMessageId:update.Message.MessageId);

                        await bot.SendLocationAsync(chatId, 70.030, 56.5454); 
                            break;

                    case "button3":

                        // it returns data of that poll, we can have access to them and store them in DB in future
                        var res = await bot.SendPollAsync(chatId,
                            question: "آیا از چنل ما راضی هستید؟",
                            new string[] { "بلی", "خیر", "دیدن نتایج" },
                            correctOptionId: 0,
                            isAnonymous: false,
                            closeDate: DateTime.Now.AddMinutes(1),
                            type: PollType.Quiz, cancellationToken: cancellationToken);

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
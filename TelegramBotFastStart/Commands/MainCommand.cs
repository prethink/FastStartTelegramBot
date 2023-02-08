using TelegramBotFastStart.Attributes;
using TelegramBotFastStart.Commands.Common;
using TelegramBotFastStart.Helpers;
using TelegramBotFastStart.Helpers.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace TelegramBotFastStart.Commands
{
    internal class MainCommand
    {
        [MessageMenuHandler(true, nameof(Router.BT_MENU), nameof(Router.BT_MAIN_MENU))]
        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task MainMenu(ITelegramBotClient botClient, Update update)
        {
            try
            {
                await MainMenu(botClient,  update.GetChatId(), MessagesPattern.GetMessage(nameof(MessagesPattern.MSG_MAIN_MENU)));
            }
            catch(Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        public static async Task MainMenu(ITelegramBotClient botClient, long telegramId)
        {
            try
            {
                await MainMenu(botClient, telegramId, MessagesPattern.GetMessage(nameof(MessagesPattern.MSG_MAIN_MENU)));
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task MainMenu(ITelegramBotClient botClient, long telegramId, string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    message = "Главное меню";
                }

                var option = new OptionMessage();
                option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, Router.GetValueButton(nameof(Router.BT_MAIN_MENU)));
                await Common.Message.Send(botClient, telegramId, message, option);
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

    }
}

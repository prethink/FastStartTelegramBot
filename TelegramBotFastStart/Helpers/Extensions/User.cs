using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBotFastStart.Helpers.Extensions
{
    public static class User
    {
        public static long GetChatId(this Update update)
        {
            if (update.Message != null)
                return update.Message.Chat.Id;

            if (update.CallbackQuery != null)
                return update.CallbackQuery.Message.Chat.Id;


            throw new Exception("Не удалось получить чат ID");
        }

        public static int GetMessageId(this Update update)
        {
            var data = update.GetCacheData();

            if (update.CallbackQuery != null)
                return update.CallbackQuery.Message.MessageId;

            if (data?.LastMessage?.MessageId > 0)
            {
                var messageId = data.LastMessage.MessageId;
                return messageId;
            }

            throw new Exception("Не удалось получить ID чата");
        }

        public static bool IsAdmin(this Update update)
        {
            try
            {
                var telegramId = update.GetChatId();
                var settings = ConfigApp.GetSettings<SettingsConfig>();
                return settings.Admins.Contains(telegramId);
            }
            catch(Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return false;
            }
        }


        public static string GetInfoUser(this Update update)
        {
            string result = "";

            result += update?.Message?.Chat?.Id + " ";
            result += update?.Message?.Chat?.FirstName + " " ?? "";
            result += update?.Message?.Chat?.LastName + " " ?? "";
            result += update?.Message?.Chat?.Username + " " ?? "";

            result += update?.CallbackQuery?.Message?.Chat?.Id + " ";
            result += update?.CallbackQuery?.Message?.Chat?.FirstName + " " ?? "";
            result += update?.CallbackQuery?.Message?.Chat?.LastName + " " ?? "";
            result += update?.CallbackQuery?.Message?.Chat?.Username + " " ?? "";

            return result;
        }
    }
}

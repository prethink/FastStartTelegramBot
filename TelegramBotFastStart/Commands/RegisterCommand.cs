using TelegramBotFastStart.Attributes;
using TelegramBotFastStart.Helpers;
using TelegramBotFastStart.Helpers.Extensions;
using TelegramBotFastStart.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotFastStart.Commands
{
    public class RegisterCommand
    {
        [MessageMenuHandler(true, nameof(Router.BT_START))]
        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task Start(ITelegramBotClient botClient, Update update)
        {
            await CheckRegister(botClient, update, true);
        }

        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task StartWithArguments(ITelegramBotClient botClient, Update update, string arg)
        {
            try
            {
                if (!string.IsNullOrEmpty(arg))
                {
                    await CheckRegister(botClient, update, true, arg);
                }
                else
                {
                    await CheckRegister(botClient, update, true);
                } 
            }
            catch(Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }

        }

        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task CheckRegister(ITelegramBotClient botClient, Update update, bool showMsg, string refferId = null)
        {
            try
            {
                if(update.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
                {
                    await UserHandler(botClient, update, showMsg, refferId);
                }
                else
                {
                    string msgUser = $"Регистрация группы или другого объекта {update.GetCacheData()}";
                    TelegramService.GetInstance().InvokeCommonLog(msgUser, TelegramService.TelegramEvents.GroupAction, ConsoleColor.White);
                }
            
                
            }
            catch(Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task UserHandler(ITelegramBotClient botClient, Update update, bool showMsg, string refferId = null)
        {
            try
            {

            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task RegisterNewUser(ITelegramBotClient botClient, UserBot user, long? existRefUser, Models.DataBase.LinkStatistic link)
        {
            try
            {
                var settings = ConfigApp.GetSettings<SettingsConfig>();
            }
            catch(Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }
    }
}

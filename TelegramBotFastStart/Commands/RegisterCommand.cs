using TelegramBotFastStart.Attributes;
using TelegramBotFastStart.Helpers.Extensions;
using TelegramBotFastStart.Models.DataBase;
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
            //try
            //{
            //    bool addBonusParent = false;
            //    long? parentUserId = null;
            //    LinkStatistic link = null;
            //    using (var db = new AppDbContext())
            //    {
            //        var user = await db.Users.FirstOrDefaultAsync(x => x.TelegramId == update.GetChatId());
            //        if (user != null)
            //        {
            //            await ReactivateUser(botClient, user);
            //            await MainCommand.MainMenu(botClient, update);
            //            return;
            //        }

            //        var newUser = new UserBot();

            //        if (!string.IsNullOrEmpty(refferId))
            //        {
            //            if (long.TryParse(refferId, out var id))
            //            {
            //                var parentUser = db.Users.FirstOrDefault(x => x.TelegramId == id);
            //                if (parentUser != null)
            //                {
            //                    newUser.ParentUserId = parentUser.TelegramId;
            //                    parentUserId = parentUser.TelegramId;
            //                    addBonusParent = true;
            //                }
            //            }
            //            else
            //            {
            //                var parentUser = db.Users.FirstOrDefault(x => x.Link == refferId);
            //                if (parentUser != null)
            //                {
            //                    newUser.ParentUserId = parentUser.TelegramId;
            //                    parentUserId = parentUser.TelegramId;
            //                    addBonusParent = true;
            //                }
            //                link = await db.Links.FirstOrDefaultAsync(x => x.Link == refferId);
            //                if (link != null)
            //                {
            //                    link.RegCount++;
            //                }
            //            }


            //            var settings = ConfigApp.GetSettings<SettingsConfig>();
            //            newUser.TelegramId = update.GetChatId();
            //            newUser.RegisteredDate = DateTime.Now;
            //            newUser.LastActivity = DateTime.Now;
            //            newUser.Login = update.Message.Chat.Username;
            //            newUser.FirstName = update.Message.Chat.FirstName;
            //            newUser.LastName = update.Message.Chat.LastName;
            //            newUser.Link = MessageGenerator.PasswordGenerate(MessageGenerator.PasswordChars.Digits | MessageGenerator.PasswordChars.Alphabet, ConfigApp.GetSettings<SettingsConfig>().WordLength, "u");
            //            db.Users.Add(newUser);
            //            await db.SaveChangesAsync();
            //            await RegisterNewUser(botClient, newUser, parentUserId, link);
            //            await MainCommand.MainMenu(botClient, update);
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    TelegramService.GetInstance().InvokeErrorLog(ex);
            //}
        }

        //public static async Task<bool> ReactivateUser(ITelegramBotClient botClient, UserBot user)
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        if (user != null)
        //        {
        //            bool reactivate = !user.IsActivate;
        //            user.IsActivate = true;
        //            db.Entry(user).State = EntityState.Modified;
        //            await db.SaveChangesAsync();
        //            if (reactivate)
        //            {
        //                await Common.Message.Send(botClient, user.TelegramId, "Ваш пользователь снова активен");
        //            }

        //            return true;
        //        }
        //    }
        //    return false;
        //}

        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task RegisterNewUser(ITelegramBotClient botClient, UserBot user, long? parentUserId, Models.DataBase.LinkStatistic link)
        {
            //try
            //{
            //    var settings = ConfigApp.GetSettings<SettingsConfig>();
            //    using (var db = new AppDbContext())
            //    {
            //        if (parentUserId != null)
            //        {
            //            var parentUser = await db.Users.Include(x => x.PhotoBattleRequest).FirstOrDefaultAsync(x => x.TelegramId == parentUserId);
            //            if (parentUser != null)
            //            {
            //                string msg = "✨ По вашей ссылке зарегистрирован новый пользователь\n🎁 Вам начислено:\n" +
            //                    $"├ + {settings.RefferalCoins} рублей {Router.S_COINS}\n" +
            //                    $"├ + {countRating} очков рейтинга {Router.S_RATING}\n" +
            //                    $"└ + {countActivity} очков активности {Router.S_ACTIVITY}\n";

            //                await Common.Message.Send(botClient, parentUserId.Value, msg);
            //                parentUser.AddActivity(countActivity, false);
            //                await db.SaveChangesAsync();
            //            }
            //        }


            //        if (settings.ShowNotifyRegisterUserForAdmin)
            //        {
            //            var photos = await botClient.GetUserProfilePhotosAsync(user.TelegramId);
            //            long allCountUser = db.Users.Count();

            //            foreach (var telegramId in settings.Admins)
            //            {
            //                string msg = MessagesPattern.GetMessage(nameof(MessagesPattern.MSG_NEW_USER));
            //                msg += "\n🆔 " + user.TelegramId;
            //                msg += "\n🙆‍♂️ " + user.GetName();
            //                msg += "\n\n\nВсего пользователей: " + allCountUser;
            //                if (link != null)
            //                {
            //                    msg += $"\nРегистрация с {link.Description} - количество регистраций {link.RegCount}";
            //                }
            //                if (parentUserId != null)
            //                {
            //                    msg += $"\nПривел человека {parentUserId.Value}";
            //                }
            //                if (photos.TotalCount > 0)
            //                {
            //                    string photoId = photos.Photos[0][1].FileId;
            //                    await Common.Message.SendPhotoWithUrl(botClient, telegramId, msg, photoId);
            //                }
            //                else
            //                {
            //                    await Common.Message.Send(botClient, telegramId, msg);
            //                }
            //            }
            //        }
            //        TelegramService.GetInstance().InvokeCommonLog($"В боте зарегистрирован новый пользователь! Id:{user.TelegramId} Имя:{user.GetName()}", TelegramService.TelegramEvents.Register, ConsoleColor.Green);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    TelegramService.GetInstance().InvokeErrorLog(ex);
            //}
        }
    }
}

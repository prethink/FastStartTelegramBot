using TelegramBotFastStart.Commands;
using TelegramBotFastStart.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFastStart.Helpers
{
    public static class TextGenerator
    {
        const string Digits = "0123456789";
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        const string Symbols = " ~`@#$%^&*()_+-=[]{};'\\:\"|,./<>?";

        [Flags]
        public enum PasswordChars
        {
            Digits = 0b0001,
            Alphabet = 0b0010,
            Symbols = 0b0100
        }

        public static string PasswordGenerate(PasswordChars passwordChars, int length, string prefix = "")
        {
            var random = new Random();
            var resultPassword = new StringBuilder(length);
            var passwordCharSet = String.Empty;
            resultPassword.Append(prefix);
            if (passwordChars.HasFlag(PasswordChars.Alphabet))
            {
                passwordCharSet += Alphabet + Alphabet.ToUpper();
            }
            if (passwordChars.HasFlag(PasswordChars.Digits))
            {
                passwordCharSet += Digits;
            }
            if (passwordChars.HasFlag(PasswordChars.Symbols))
            {
                passwordCharSet += Symbols;
            }
            for (var i = 0; i < length; i++)
            {
                resultPassword.Append(passwordCharSet[random.Next(0, passwordCharSet.Length)]);
            }
            return resultPassword.ToString();
        }

        public static string CouponGenerate(DateTime date, int segmentLength = 6, int countSplit = 1)
        {
            var random = new Random((int)date.Ticks);
            var couponCharSet = Alphabet.ToUpper() + Digits;
            var result = new StringBuilder();

            for (int i = 0; i < countSplit + 1; i++)
            {
                for (int j = 0; j < segmentLength; j++)
                {
                    result.Append(couponCharSet[random.Next(0, couponCharSet.Length)]);
                }
                if (i < countSplit)
                    result.Append('-');
            }

            return result.ToString();
        }

        public static string GetRefLink(long telegramId, bool copy = false)
        {
            if(copy)
            {
                return $"<code>https://t.me/{TelegramService.GetInstance().BotName}?start={telegramId}</code>";
            }
            else
            {
                return $"https://t.me/{TelegramService.GetInstance().BotName}?start={telegramId}";
            }

        }
    }
}

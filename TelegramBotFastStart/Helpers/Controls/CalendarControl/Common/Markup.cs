using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotFastStart.Helpers;
using TelegramBotFastStart.Models;
using TelegramBotFastStart.Models.CallbackCommands;

namespace CalendarPicker.CalendarControl
{
    public static class Markup
    {
        public static InlineKeyboardMarkup Calendar(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>();

            keyboardRows.Add(Row.Date(date, dtfi));
            keyboardRows.Add(Row.DayOfWeek(dtfi));
            keyboardRows.AddRange(Row.Month(date, dtfi));
            keyboardRows.Add(Row.Controls(date));

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickMonthYear(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>(date.ToString("MMMM", dtfi), InlineCallback.PickMonth, new CallendarCommand(date))),
                    MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>(date.ToString("yyyy", dtfi), InlineCallback.PickYear, new CallendarCommand(date)))
                },
                new InlineKeyboardButton[]
                {
                    MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>("<<", InlineCallback.ChangeTo, new CallendarCommand(date))),
                    " "
                }
            };

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickMonth(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[5][];

            for (int month = 0, row = 0; month < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, month++)
                {
                    var day = new DateTime(date.Year, month + 1, 1);

                    keyboardRow[j] = MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>(dtfi.MonthNames[month], InlineCallback.YearMonthPicker, new CallendarCommand(day)));
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date);

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickYear(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[6][];

            var startYear = date.AddYears(-7);

            for (int i = 0, row = 0; i < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, i++)
                {
                    var day = startYear.AddYears(i);
                    keyboardRow[j] = MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>(day.ToString("yyyy", dtfi), InlineCallback.YearMonthPicker, new CallendarCommand(day)));
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date);
            keyboardRows[5] = Row.ChangeYear(date);

            return new InlineKeyboardMarkup(keyboardRows);
        }
    }
}

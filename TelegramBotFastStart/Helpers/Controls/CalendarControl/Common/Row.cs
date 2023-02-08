using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotFastStart.Helpers;
using TelegramBotFastStart.Models;
using TelegramBotFastStart.Models.CallbackCommands;

namespace CalendarPicker.CalendarControl
{
    public static class Row
    {
        public static IEnumerable<InlineKeyboardButton> Date(in DateTime date, DateTimeFormatInfo dtfi) =>
        new InlineKeyboardButton[]
        {
                MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>($"» {date.ToString("Y", dtfi)} «", InlineCallback.YearMonthPicker, new CallendarCommand(date)))
            };

        public static IEnumerable<InlineKeyboardButton> DayOfWeek(DateTimeFormatInfo dtfi)
        {
            var dayNames = new InlineKeyboardButton[7];

            var firstDayOfWeek = (int)dtfi.FirstDayOfWeek;
            for (int i = 0; i < 7; i++)
            {
                yield return dtfi.AbbreviatedDayNames[(firstDayOfWeek + i) % 7];
            }
        }

        public static IEnumerable<IEnumerable<InlineKeyboardButton>> Month(DateTime date, DateTimeFormatInfo dtfi)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).Day;

            for (int dayOfMonth = 1, weekNum = 0; dayOfMonth <= lastDayOfMonth; weekNum++)
            {
                yield return NewWeek(weekNum, ref dayOfMonth);
            }

            IEnumerable<InlineKeyboardButton> NewWeek(int weekNum, ref int dayOfMonth)
            {
                var week = new InlineKeyboardButton[7];

                for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                {
                    if ((weekNum == 0 && dayOfWeek < FirstDayOfWeek())
                       ||
                       dayOfMonth > lastDayOfMonth
                    )
                    {
                        week[dayOfWeek] = " ";
                        continue;
                    }

                    week[dayOfWeek] = MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>(dayOfMonth.ToString(), InlineCallback.PickDate, new CallendarCommand(new DateTime(date.Year, date.Month, dayOfMonth)))); 
                    dayOfMonth++;
                }
                return week;

                int FirstDayOfWeek() =>
                    (7 + (int)firstDayOfMonth.DayOfWeek - (int)dtfi.FirstDayOfWeek) % 7;
            }
        }

        public static IEnumerable<InlineKeyboardButton> Controls(in DateTime date) =>
            new InlineKeyboardButton[]
            {
                MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>("<", InlineCallback.ChangeTo, new CallendarCommand(date.AddMonths(-1)))),
                " ",
                MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>(">", InlineCallback.ChangeTo, new CallendarCommand(date.AddMonths(1)))),
            };

        public static InlineKeyboardButton[] BackToMonthYearPicker(in DateTime date) =>
            new InlineKeyboardButton[3]
            {
                MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>("<<", InlineCallback.YearMonthPicker, new CallendarCommand(date))),
                " ",
                " "
            };
            public static InlineKeyboardButton[] ChangeYear(in DateTime date) =>
            new InlineKeyboardButton[3]
            {
                MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>("<", InlineCallback.PickYear, new CallendarCommand(date.AddYears(-12)))),
                " ",
                MenuGenerator.GetInlineButton(new InlineCallbackCommand<CallendarCommand>(">", InlineCallback.PickYear, new CallendarCommand(date.AddYears(12))))
            };
    }
}

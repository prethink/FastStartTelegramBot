using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFastStart.Models
{
    public enum InlineCallbackCommands : ushort
    {
        [Description("None")]
        None = 594,
        [Description("PickMonth")]
        PickMonth,
        [Description("PickYear")]
        PickYear,
        [Description("ChangeTo")]
        ChangeTo,
        [Description("YearMonthPicker")]
        YearMonthPicker,
        [Description("PickDate")]
        PickDate,
    }
}

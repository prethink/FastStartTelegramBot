using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFastStart.Models
{
    public enum InlineCallback : ushort
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
        [Description("NextPage")]
        NextPage,
        [Description("CurrentPage")]
        CurrentPage,
        [Description("PreviousPage")]
        PreviousPage,
        [Description("GetFreeVIP")]
        GetFreeVIP,
        [Description("GetVipOneDay")]
        GetVipOneDay,
        [Description("GetVipOneWeek")]
        GetVipOneWeek,
        [Description("GetVipOneMonth")]
        GetVipOneMonth,
        [Description("GetVipOneForever")]
        GetVipOneForever,
    }
}

using TelegramBotFastStart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFastStart.Attributes
{
    internal class InlineCallbackHandlerAttribute : Attribute
    {
        public List<InlineCallback> Commands { get; set; }

        public InlineCallbackHandlerAttribute(params InlineCallback[] commands)
        {
            Commands = commands.ToList();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFastStart.Models.CallbackCommands
{
    public class CallbackBaseCommand
    {
        [JsonProperty("0")]
        public InlineCallback LastCommand { get; set; }
        public CallbackBaseCommand(InlineCallback data = InlineCallback.None)
        {
            LastCommand = data;
        }
    }
}

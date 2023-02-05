using TelegramBotFastStart.Commands;
using TelegramBotFastStart.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TelegramBotFastStart.TelegramService;

namespace TelegramBotFastStart
{
    public class Tasker
    {
        public int TimeOut { get; set; }

        public Tasker(int timeOut)
        {
            TimeOut = timeOut;
        }

        public async Task Start()
        {
            while(true)
            {
                await Task.Delay(TimeOut * 1000);
           }
        }
    }
}

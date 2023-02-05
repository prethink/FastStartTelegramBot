using TelegramBotFastStart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotFastStart.Attributes
{
    internal class AccessAttribute : Attribute
    {
        public UserPrivilege? RequiredPrivilege { get; set; }
        public AccessAttribute()
        {
        }

        public AccessAttribute(UserPrivilege privilages)
        {
            RequiredPrivilege = privilages;
        }
    }
}

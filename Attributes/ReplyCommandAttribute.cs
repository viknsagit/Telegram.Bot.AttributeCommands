using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.AttributeCommands.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ReplyCommandAttribute : Attribute
    {
        public string ReplyCommand { get; private set; }

        public ReplyCommandAttribute(string ReplyCommand)
        {
            this.ReplyCommand = ReplyCommand;
        }
    }
}
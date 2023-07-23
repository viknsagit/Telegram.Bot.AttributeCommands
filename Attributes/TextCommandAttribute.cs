namespace Telegram.Bot.AttributeCommands.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextCommandAttribute : Attribute
    {
        public string Command { get; private set; }

        public TextCommandAttribute(string Command)
        {
            this.Command = Command;
        }
    }
}
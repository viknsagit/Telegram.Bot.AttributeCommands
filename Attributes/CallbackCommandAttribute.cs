namespace Telegram.Bot.AttributeCommands.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CallbackCommandAttribute : Attribute
    {
        public string CallbackCommand { get; private set; }

        public CallbackCommandAttribute(string CallbackCommand)
        {
            this.CallbackCommand = CallbackCommand;
        }
    }
}
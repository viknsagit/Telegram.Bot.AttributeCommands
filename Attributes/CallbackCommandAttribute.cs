namespace Telegram.Bot.AttributeCommands.Attributes
{
    /// <summary>
    /// Attribute used to mark a method as a callback command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CallbackCommandAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the callback command.
        /// </summary>
        public string CallbackCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallbackCommandAttribute"/> class.
        /// </summary>
        /// <param name="CallbackCommand">The callback command.</param>
        /// <returns>
        /// A new instance of the <see cref="CallbackCommandAttribute"/> class.
        /// </returns>
        public CallbackCommandAttribute(string CallbackCommand)
        {
            this.CallbackCommand = CallbackCommand;
        }
    }
}
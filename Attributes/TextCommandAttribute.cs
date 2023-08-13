namespace Telegram.Bot.AttributeCommands.Attributes
{
    /// <summary>
    /// Attribute used to mark a method as a text command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextCommandAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Initializes a new instance of the TextCommandAttribute class with the specified command.
        /// </summary>
        /// <param name="Command">The command.</param>
        /// <returns>A new instance of the TextCommandAttribute class.</returns>
        public TextCommandAttribute(string Command)
        {
            this.Command = Command;
        }
    }
}
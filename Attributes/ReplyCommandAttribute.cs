namespace Telegram.Bot.AttributeCommands.Attributes
{
    /// <summary>
    /// Attribute used to mark a method as a reply command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ReplyCommandAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the ReplyCommand property.
        /// </summary>
        public string ReplyCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ReplyCommandAttribute class with the specified reply command.
        /// </summary>
        /// <param name="ReplyCommand">The reply command.</param>
        /// <returns>A new instance of the ReplyCommandAttribute class.</returns>
        public ReplyCommandAttribute(string ReplyCommand)
        {
            this.ReplyCommand = ReplyCommand;
        }
    }
}
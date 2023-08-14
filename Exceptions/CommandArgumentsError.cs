namespace Telegram.Bot.AttributeCommands.Exceptions
{

    /// <summary>
    /// Represents an exception that is thrown when the number of command arguments is incorrect.
    /// </summary>
    public class CommandArgumentsCountError : Exception
    {
        /// <summary>
        /// Constructor for CommandArgumentsCountError class.
        /// </summary>
        /// <param name="findedArgsCount">Number of arguments in a command.</param>
        /// <param name="invokeArgsCount">Number of arguments to call.</param>
        /// <returns>
        /// An instance of CommandArgumentsCountError class.
        /// </returns>
        public CommandArgumentsCountError(int findedArgsCount, int invokeArgsCount) : base($"The number of arguments to invoke the command method is incorrect. Number of arguments in a command: {findedArgsCount}, number of arguments to call: {invokeArgsCount}.") { }

    }
}
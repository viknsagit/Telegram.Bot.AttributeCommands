namespace Telegram.Bot.AttributeCommands.Exceptions
{
    /// <summary>
    /// Exception thrown when a command is passed an argument of an incorrect type.
    /// </summary>
    public class CommandBadArgumentType : Exception
    {
        /// <summary>
        /// Constructor for CommandBadArgumentType class.
        /// </summary>
        /// <param name="methodArg">The argument in the command method.</param>
        /// <param name="invokeArg">The argument to call.</param>
        /// <returns>
        /// A CommandBadArgumentType object.
        /// </returns>
        public CommandBadArgumentType(Type methodArg, Type invokeArg) : base($"There is a mismatch in the arguments to invoke the command. The argument in the command method is {methodArg}. The argument to call is {invokeArg}.")
        { }
    }
}
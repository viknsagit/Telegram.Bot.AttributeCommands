using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.AttributeCommands.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a command is not found.
    /// </summary>
    public class CommandNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of CommandNotFoundException with the specified command name.
        /// </summary>
        /// <param name="commandName">The name of the command.</param>
        /// <returns>A new instance of CommandNotFoundException.</returns>
        public CommandNotFoundException(string commandName) : base($"Command with name: {commandName}, is not registered.") { }
    }
}
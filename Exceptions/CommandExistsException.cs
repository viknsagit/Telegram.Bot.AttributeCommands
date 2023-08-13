using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.AttributeCommands.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a command with the same name is already registered.
    /// </summary>
    public class CommandExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExistsException"/> class with a specified error message.
        /// </summary>
        /// <param name="commandName">The name of the command.</param>
        /// <returns>A new instance of the <see cref="CommandExistsException"/> class.</returns>
        public CommandExistsException(string commandName) : base($"Command with name: {commandName} is already registered.") { }
    }
}
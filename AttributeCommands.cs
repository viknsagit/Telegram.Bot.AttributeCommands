using System.Diagnostics;
using System.Reflection;

using Telegram.Bot.AttributeCommands.Attributes;
using Telegram.Bot.AttributeCommands.Exceptions;
using Telegram.Bot.Types;

namespace Telegram.Bot.AttributeCommands
{
    /// <summary>
    /// This class provides methods for manipulating attributes.
    /// </summary>
    public class AttributeCommands
    {
        private readonly Dictionary<string, MethodInfo> _textCommandsMethods = new();
        private readonly Dictionary<string, MethodInfo> _callbackCommandsMethods = new();
        private readonly Dictionary<string, MethodInfo> _replyCommandsMethods = new();

        /// <summary>
        /// Registers commands from the given class.
        /// </summary>
        /// <param name="CommandsClass">The class containing the commands.</param>
        public void RegisterCommands(Type CommandsClass)
        {
            MethodInfo[] methods = CommandsClass.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.GetCustomAttribute<TextCommandAttribute>() != null)
                {
                    var commandName = method.GetCustomAttribute<TextCommandAttribute>()!.Command;
                    if (!_textCommandsMethods.ContainsKey(commandName))
                        _textCommandsMethods.Add(commandName, method);
                    else
                        throw new CommandExistsException(commandName);
                }
                if (method.GetCustomAttribute<CallbackCommandAttribute>() != null)
                {
                    var commandName = method.GetCustomAttribute<CallbackCommandAttribute>()!.CallbackCommand;
                    if (!_callbackCommandsMethods.ContainsKey(commandName))
                        _callbackCommandsMethods.Add(commandName, method);
                    else
                        throw new CommandExistsException(commandName);
                }
                if (method.GetCustomAttribute<ReplyCommandAttribute>() != null)
                {
                    var commandName = method.GetCustomAttribute<ReplyCommandAttribute>()!.ReplyCommand;
                    if (!_replyCommandsMethods.ContainsKey(commandName))
                        _replyCommandsMethods[commandName] = method;
                    else
                        throw new CommandExistsException(commandName);
                }
            }
        }

        /// <summary>
        /// Gets the command method info by name.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <returns>The command method info.</returns>
        /// <exception cref="CommandNotFoundException">Thrown when the command is not found.</exception>
        public async Task<MethodInfo> GetCommandByNameAsync(string commandName)
        {
            if (_textCommandsMethods.TryGetValue(commandName, out var textCommand))
                return await Task.FromResult(textCommand);
            else if (_callbackCommandsMethods.TryGetValue(commandName, out var callbackCommand))
                return await Task.FromResult(callbackCommand);
            else if (_replyCommandsMethods.TryGetValue(commandName, out var replyCommand))
                return await Task.FromResult(replyCommand);

            throw new CommandNotFoundException(commandName);
        }

        /// <summary>
        /// Processes the command with the specified name and arguments.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ProcessCommand(string commandName, object[] arguments)
        {
            MethodInfo? findedMethod = await GetCommandByNameAsync(commandName);

            if (findedMethod is null)
                throw new CommandNotFoundException(commandName);
            else
            {
                var findedArgs = findedMethod.GetParameters();
                if (findedArgs.Length != arguments.Length)
                    throw new CommandArgumentsCountError(findedArgs.Length, arguments.Length);

                for (int i = 0; i < findedArgs.Length; i++)
                {
                    if (findedArgs[i].ParameterType != arguments[i].GetType())
                    {
                        throw new CommandBadArgumentType(findedArgs[i].ParameterType, arguments[i].GetType());
                    }
                }

                findedMethod.Invoke(this, arguments);
            }

            await Task.CompletedTask;
        }
    }
}
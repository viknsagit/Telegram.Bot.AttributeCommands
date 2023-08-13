﻿using System.Reflection;

using Telegram.Bot.AttributeCommands.Attributes;
using Telegram.Bot.AttributeCommands.Exceptions;
using Telegram.Bot.Types;

namespace Telegram.Bot.AttributeCommands
{
    public class AttributeCommands
    {
        private readonly Dictionary<string, MethodInfo> _textCommandsMethods;
        private readonly Dictionary<string, MethodInfo> _callbackCommandsMethods;
        private readonly Dictionary<string, MethodInfo> _replyCommandsMethods;

        public AttributeCommands()
        {
            _textCommandsMethods = new();
            _callbackCommandsMethods = new();
            _replyCommandsMethods = new();
        }

        /// <summary>
        /// Registers text commands from the given class.
        /// </summary>
        /// <param name="commandsClass">The class containing the text commands.</param>
        public void RegisterTextCommands(Type commandsClass)
        {
            MethodInfo[] methods = commandsClass.GetMethods();
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
            }
        }

        /// <summary>
        /// Registers callback commands from the given class.
        /// </summary>
        /// <param name="commandsClass">The class containing the callback commands.</param>
        public void RegisterCallbackCommands(Type commandsClass)
        {
            MethodInfo[] methods = commandsClass.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.GetCustomAttribute<CallbackCommandAttribute>() != null)
                {
                    var commandName = method.GetCustomAttribute<CallbackCommandAttribute>()!.CallbackCommand;
                    if (!_callbackCommandsMethods.ContainsKey(commandName))
                        _callbackCommandsMethods.Add(commandName, method);
                    else
                        throw new CommandExistsException(commandName);
                }
            }
        }

        /// <summary>
        /// Registers the reply commands from the specified commands class.
        /// </summary>
        /// <param name="commandsClass">The commands class.</param>
        public void RegisterReplyCommands(Type commandsClass)
        {
            MethodInfo[] methods = commandsClass.GetMethods();
            foreach (MethodInfo method in methods)
            {
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
        /// Processes the command by searching for the appropriate method.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="client">TelegramBotClient instance.</param>
        /// <param name="update">Update instance.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation.
        /// </returns>
        public async Task ProcessCommand(string commandName, TelegramBotClient client, Update update)
        {
            MethodInfo? findedMethod = await GetCommandByNameAsync(commandName);
            if (findedMethod is null)
                throw new CommandNotFoundException(commandName);
            else
                findedMethod.Invoke(this, new object[] { client, update });

            await Task.CompletedTask;
        }

        /// <summary>
        /// Processes the command by searching for the corresponding method.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="arguments">Arguments for the command.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public async Task ProcessCommand(string commandName, object[] arguments)
        {
            MethodInfo? findedMethod = await GetCommandByNameAsync(commandName);
            if (findedMethod is null)
                throw new CommandNotFoundException(commandName);
            else
                findedMethod.Invoke(this, arguments);

            await Task.CompletedTask;
        }
    }
}
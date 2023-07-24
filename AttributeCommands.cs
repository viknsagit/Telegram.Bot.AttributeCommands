using System.Reflection;
using Telegram.Bot.AttributeCommands.Attributes;

namespace Telegram.Bot.AttributeCommands
{
    public class AttributeCommands
    {
        private readonly Dictionary<string, MethodInfo> _textCommandsMethods;
        private readonly Dictionary<string, MethodInfo> _callbackCommandsMethods;

        public AttributeCommands()
        {
            _textCommandsMethods = new();
            _callbackCommandsMethods = new();
        }

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
                        throw new Exception($"Text command with name {commandName} is already registered.");
                }
            }
        }

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
                        throw new Exception($"Callback command with name {commandName} is already registered.");
                }
            }
        }

        public MethodInfo GetTextCommand(string commandName)
        {
            if (_textCommandsMethods.ContainsKey(commandName))
                return _textCommandsMethods[commandName];
            else
                throw new Exception($"Text command with name {commandName} is not registered.");
        }

        public MethodInfo GetCallbackCommand(string commandName)
        {
            if (_callbackCommandsMethods.ContainsKey(commandName))
                return _callbackCommandsMethods[commandName];
            else
                throw new Exception($"Callback command with name {commandName} is not registered.");
        }

        public async Task ProcessTextCommand(string commandName, object[] invokeArguments)
        {
            GetTextCommand(commandName)?.Invoke(this, invokeArguments);
            await Task.CompletedTask;
        }

        public async Task ProcessCallbackCommand(string commandName, object[] invokeArguments)
        {
            GetCallbackCommand(commandName)?.Invoke(this, invokeArguments);
            await Task.CompletedTask;
        }
    }
}
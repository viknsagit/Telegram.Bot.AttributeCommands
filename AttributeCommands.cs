using System.Reflection;
using Telegram.Bot.AttributeCommands.Attributes;

namespace Telegram.Bot.AttributeCommands
{
    public class AttributeCommands
    {
        private readonly List<MethodInfo> _textCommandsMethods;
        private readonly List<MethodInfo> _callbackCommandsMethods;

        public AttributeCommands()
        {
            _textCommandsMethods = new List<MethodInfo>();
            _callbackCommandsMethods = new List<MethodInfo>();
        }

        public void RegisterTextCommands(Type commandsClass)
        {
            MethodInfo[] methods = commandsClass.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.GetCustomAttribute<TextCommandAttribute>() is not null)
                    _textCommandsMethods.Add(method);
            }
        }

        public MethodInfo GetTextCommand(string text)
        {
            return _textCommandsMethods.FirstOrDefault(m => m.GetCustomAttribute<TextCommandAttribute>()?.Command == text)!;
        }

        public MethodInfo GetCallbackCommand(string text)
        {
            return _callbackCommandsMethods.FirstOrDefault(m => m.GetCustomAttribute<CallbackCommandAttribute>()?.CallbackCommand == text)!;
        }

        public void RegisterCallbackCommands(Type commandsClass)
        {
            MethodInfo[] methods = commandsClass.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.GetCustomAttribute<CallbackCommandAttribute>() is not null)
                    _callbackCommandsMethods.Add(method);
            }
        }
    }
}
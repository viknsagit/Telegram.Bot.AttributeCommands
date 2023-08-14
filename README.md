# Telegram.Bot.AttributeCommands Library Documentation

The `Telegram.Bot.AttributeCommands` library offers a streamlined approach to managing and executing text, callback, and reply commands for a Telegram bot. By utilizing custom attributes to mark and categorize methods, this library simplifies the registration and processing of commands.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
  - [Creating Command Classes](#creating-command-classes)
  - [Registering Commands](#registering-commands)
  - [Handling Updates](#handling-updates)
- [Usage](#usage)
  - [Attributes](#attributes)
    - [`TextCommandAttribute`](#textcommandattribute)
    - [`CallbackCommandAttribute`](#callbackcommandattribute)
    - [`ReplyCommandAttribute`](#replycommandattribute)
  - [Exceptions](#exceptions)
    - [`CommandNotFoundException`](#commandnotfoundexception)
    - [`CommandExistsException`](#commandexistsexception)
- [Example](#example)
- [Processing Updates](#processing-updates)
- [Exception Handling](#exception-handling)

## Introduction

The **Telegram.Bot.AttributeCommands** library provides an elegant solution for managing and processing different command types within a Telegram bot. By using custom attributes, the library organizes text, callback, and reply commands, leading to a more organized and efficient command handling process.

## Getting Started

### Creating Command Classes

To start using the **Telegram.Bot.AttributeCommands** library, create a class to contain your command methods. These methods should be static and marked with the appropriate command attributes.

### Registering Commands

Before utilizing the registered commands, instantiate the `AttributeCommands` class. Depending on your command types, use the `RegisterTextCommands`, `RegisterCallbackCommands`, and `RegisterReplyCommands` methods to register commands from your command class.

Here's an example of registering commands:

```csharp
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.AttributeCommands;
using Telegram.Bot.AttributeCommands.Exceptions;

public class YourBotClass
{
    private readonly TelegramBotClient _botClient;
    private readonly AttributeCommands _commands;

    public YourBotClass(string botToken)
    {
        _botClient = new TelegramBotClient(botToken);
        _commands = new AttributeCommands();

        // Register your command classes
        _commands.RegisterTextCommands(typeof(TestCommands));
        _commands.RegisterCallbackCommands(typeof(TestCommands));
        _commands.RegisterReplyCommands(typeof(TestCommands));
    }

    // Other bot handling methods here...
}
```

### Handling Updates

When handling incoming updates in your bot's code, ensure that you invoke the appropriate command processing methods based on the command type received. This guarantees that registered methods with corresponding attributes are invoked correctly.

```csharp
private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    switch (update)
    {
        case { Message: { } message }:
            {
                if (message.ReplyToMessage != null)
                    await BotOnReplyMessage(message, cts);
                else if (message.Text != null)
                    await BotOnMessageReceived(message, cts);
            }

            break;

        case { CallbackQuery: { } callbackQuery }:
            await BotOnCallbackQueryReceived(callbackQuery, cts);
            break;

        default:
            await UnknownUpdateHandlerAsync(update, cts);
            break;
    }
}

private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationTokenSource cts)
{
    try
    {
        await _commands.ProcessCommand(callbackQuery.Data!, new object[] { botClient, callbackQuery });
    }
    catch (CommandNotFoundException ex)
    {
        await botClient.SendTextMessageAsync(callbackQuery.Message!.Chat.Id, ex.Message);
    }
}

private async Task BotOnMessageReceived(Message message, CancellationTokenSource cts)
{
    try
    {
        await _commands.ProcessCommand(message.Text!, new object[] { botClient, message });
    }
    catch (CommandNotFoundException ex)
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, ex.Message);
    }
}

private async Task BotOnReplyMessage(Message message, CancellationTokenSource cts)
{
    try
    {
        await _commands.ProcessCommand(message.ReplyToMessage!.Text!, new object[] { botClient, message });
    }
    catch (CommandNotFoundException ex)
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, ex.Message);
    }
}
```

## Usage

### Attributes

The **Telegram.Bot.AttributeCommands** library includes three custom attributes to mark methods as different command types.

#### `TextCommandAttribute`

Use the `TextCommandAttribute` to identify methods as text commands for your Telegram bot.

```csharp
[TextCommand("your_text_command")]
public static void YourTextCommandMethod(TelegramBotClient client, Update update)
{
    // Your text command logic here
}
```

#### `CallbackCommandAttribute`

Employ the `CallbackCommandAttribute` to mark methods as callback commands.

```csharp
[CallbackCommand("your_callback_command")]
public static void YourCallbackCommandMethod(TelegramBotClient client, Update update)
{
    // Your callback command logic here
}
```

#### `ReplyCommandAttribute`

Utilize the `ReplyCommandAttribute` to indicate methods as reply commands.

```csharp
[ReplyCommand("your_reply_command")]
public static void YourReplyCommandMethod(TelegramBotClient client, Update update)
{
    // Your reply command logic here
}
```

### Exceptions

The **Telegram.Bot.AttributeCommands** library provides custom exceptions for error handling.

#### `CommandNotFoundException`

Thrown when attempting to process a non-existent command.

#### `CommandExistsException`

Thrown when trying to register a command with a duplicate name.

## Example

### [Example Project](https://github.com/viknsagit/Telegram.Bot.AttributeCommandsExamples)

```csharp
using Telegram.Bot;
using Telegram.Bot.Types;

public class TestCommands
{
    [TextCommand("start")]
    public static void StartCommand(TelegramBotClient client, Update update)
    {
        // Logic for the start text command
    }

    [CallbackCommand("button_click")]
    public static void ButtonClickCallback(TelegramBotClient client, Update update)
    {
        // Logic for the button click callback command
    }

    [ReplyCommand("thanks")]
    public static void ThankYouReply(TelegramBotClient client, Update update)
    {
        // Logic for the thank you reply command
    }
}
```

## Exception Handling

When using the **Telegram.Bot.AttributeCommands** library, handle exceptions to provide a smooth user experience. Catch `CommandNotFoundException` and `CommandExistsException` exceptions as needed.

---

This comprehensive documentation covers the **Telegram.Bot.AttributeCommands** library, including its custom attributes and exceptions. For more detailed information and usage scenarios, refer to the library's source code and comments.

Please note that this documentation is provided for informational purposes.
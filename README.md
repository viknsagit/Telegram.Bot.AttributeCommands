# Telegram.Bot.AttributeCommands Library Documentation

The `Telegram.Bot.AttributeCommands` library provides a streamlined approach to managing and executing text, callback, and reply commands for a Telegram bot. The library utilizes custom attributes to mark and categorize methods, simplifying the registration and processing of commands.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
- [Usage](#usage)
  - [Attributes](#attributes)
    - [`TextCommandAttribute`](#textcommandattribute)
    - [`CallbackCommandAttribute`](#callbackcommandattribute)
    - [`ReplyCommandAttribute`](#replycommandattribute)
  - [Exceptions](#exceptions)
    - [`CommandNotFoundException`](#commandnotfoundexception)
    - [`CommandExistsException`](#commandexistsexception)
- [Example](#example)
- [Exception Handling](#exception-handling)

## Introduction

The `Telegram.Bot.AttributeCommands` library offers a convenient solution for managing and processing various types of commands within a Telegram bot. By leveraging custom attributes, the library organizes text, callback, and reply commands, resulting in a more structured and efficient command handling process.

## Getting Started

1. Install the `Telegram.Bot.AttributeCommands` library via your preferred package manager.
2. Import necessary namespaces:

```csharp
using System;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.AttributeCommands;
using Telegram.Bot.AttributeCommands.Exceptions;
```

3. Create an instance of the `Telegram.Bot.AttributeCommands` class to begin managing your commands.

## Usage

### Attributes

The `Telegram.Bot.AttributeCommands` library includes three custom attributes to mark methods as different types of commands.

#### `TextCommandAttribute`

The `TextCommandAttribute` is used to identify methods as text commands for your Telegram bot.

```csharp
[TextCommand("your_text_command")]
public static void YourTextCommandMethod(TelegramBotClient client, Update update)
{
    // Your text command logic here
}
```

#### `CallbackCommandAttribute`

The `CallbackCommandAttribute` is employed for marking methods as callback commands.

```csharp
[CallbackCommand("your_callback_command")]
public static void YourCallbackCommandMethod(TelegramBotClient client, Update update)
{
    // Your callback command logic here
}
```

#### `ReplyCommandAttribute`

The `ReplyCommandAttribute` is used to indicate methods as reply commands.

```csharp
[ReplyCommand("your_reply_command")]
public static void YourReplyCommandMethod(TelegramBotClient client, Update update)
{
    // Your reply command logic here
}
```

### Exceptions

The `Telegram.Bot.AttributeCommands` library provides custom exceptions for error handling.

#### `CommandNotFoundException`

Thrown when attempting to process a non-existent command.

#### `CommandExistsException`

Thrown when trying to register a command with a duplicate name.

## Example

```csharp
using Telegram.Bot;
using Telegram.Bot.Types;

public class YourCommandsClass
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

When using the `Telegram.Bot.AttributeCommands` library, handle exceptions to ensure a smooth user experience. Catch `CommandNotFoundException` and `CommandExistsException` exceptions as needed.

---

This comprehensive documentation covers the `Telegram.Bot.AttributeCommands` library, including its custom attributes and exceptions. For more detailed information and usage scenarios, refer to the library's source code and comments.

Please note that this documentation is provided for informational purposes and may require adjustments based on the specific implementation of your project.
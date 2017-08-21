using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MyerSplash.Core.Handlers;
using MyerSplash.Shared.Logger;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyerSplash.Core.Services
{
    public class MessageService : IMessageService
    {
        private string TAG => nameof(MessageService);

        private IBotService _botService;
        private ICommandService _commandService;

        public MessageService(IBotService service, ICommandService commandService)
        {
            _botService = service;
            _commandService = commandService;
        }

        public string Echo(Message message)
        {
            if (message == null || string.IsNullOrEmpty(message.Text))
            {
                Logger.Warning(TAG, "message or its text is null");
                return null;
            }

            Logger.Info(TAG, $"message received: {message.Chat.Id} {message.Text}");

            var type = message.Type;

            ICommand command = _commandService.GetCommandFromMessageText(message.Text);
            if (command != null)
            {
                command.HandleCommand(message);
            }
            else
            {
                // no command matches, send the default text
                _botService.Client.SendTextMessageAsync(message.Chat.Id, "Hi there~");
            }

            return null;
        }

        public string Echo(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}
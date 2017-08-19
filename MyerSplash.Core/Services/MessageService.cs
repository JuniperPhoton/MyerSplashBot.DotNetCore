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
        private IServiceCollection _serviceProvider;
        private ICommandService _commandService;

        public MessageService(IBotService service, IServiceCollection servicesProvider, ICommandService commandService)
        {
            _botService = service;
            _serviceProvider = servicesProvider;
            _commandService = commandService;
        }

        public string Echo(Update update)
        {
            if (update == null)
            {
                return "I can't understand your word yet :(";
            }
            var message = update.Message;
            if (message == null || string.IsNullOrEmpty(message.Text))
            {
                Logger.Warning(TAG, "message is null");
                return null;
            }
            Logger.Info(TAG, $"message received: {message.Chat.Id}");

            var type = message.Type;

            ICommand command = _commandService.GetCommandFromMessageText(message.Text);
            if (command != null)
            {
                command?.HandleCommand(message);
            }
            else
            {
                _botService.Client.SendTextMessageAsync(message.Chat.Id, "Message received.");
            }

            return null;
        }

        public string Echo(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}
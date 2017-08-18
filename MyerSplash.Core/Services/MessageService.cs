using System;
using System.Collections.Generic;
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

        public MessageService(IBotService service)
        {
            _botService = service;
        }

        public string Echo(Update update)
        {
            if (update == null)
            {
                return "Message received";
            }
            var message = update.Message;
            if (message == null || string.IsNullOrEmpty(message.Text))
            {
                Logger.Warning(TAG, "message is null");
                return null;
            }
            Logger.Info(TAG, $"message received: {message.Chat.Id}");

            var type = message.Type;

            ICommand command = new CommandParser(_botService).ParseMessage(message.Text);
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
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyerSplash.Core.Handlers;
using MyerSplash.Shared.Logger;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyerSplash.Core.Services
{
    public class MessageService : IMessageService
    {
        private string TAG => nameof(MessageService);
        
        private string MAINTAINING_TEXT => "Sit back and relax. I am in upgrade progress. :P";

        private IBotService _botService;
        private ICommandService _commandService;
        private IHostingEnvironment _env;

        public MessageService(IBotService service, ICommandService commandService, IHostingEnvironment env)
        {
            _botService = service;
            _commandService = commandService;
            _env = env;
        }

        public string Echo(Message message)
        {
            if (message == null &&
                string.IsNullOrEmpty(message.Text) &&
                string.IsNullOrEmpty(message.Caption))
            {
                Logger.Warning(TAG, "message or its text is null");
                return null;
            }

            Logger.Info(TAG, $"message received: {message.Chat.Id} {message.Text}");

            var type = message.Type;

            var text = message.Text;
            if (string.IsNullOrEmpty(text))
            {
                text = message.Caption;
            }

            ICommand command = _commandService.GetCommandFromMessageText(text);
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
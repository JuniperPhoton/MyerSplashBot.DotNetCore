using MyerSplash.Shared.Logger;
using Telegram.Bot.Types;

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

        public void Echo(Update update)
        {
            var message = update.Message;
            Logger.Info(TAG, $"text received: {message}");
        }
    }
}
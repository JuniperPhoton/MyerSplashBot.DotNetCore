using MyerSplash.Core.Services;
using Telegram.Bot.Types;

namespace MyerSplash.Core.Handlers
{
    public class StartCommand : ICommand
    {
        public const string NAME = "start";

        private IBotService _service;

        public StartCommand(IBotService service)
        {
            _service = service;
        }

        public void HandleCommand(Message message)
        {
            _service.Client.SendTextMessageAsync(message.Chat.Id, "Typing '/' to view all supported commands.");
        }
    }
}
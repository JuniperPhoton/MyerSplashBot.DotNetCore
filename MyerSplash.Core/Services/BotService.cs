using Telegram.Bot;

namespace MyerSplash.Core.Services
{
    public class BotService : IBotService
    {
        public TelegramBotClient Client { get; set; }

        private readonly BotConfiguration _config;

        public BotService(BotConfiguration config)
        {
            _config = config;
            Client = new TelegramBotClient(_config.Token);
        }
    }
}
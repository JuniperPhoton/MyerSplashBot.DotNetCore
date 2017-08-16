using Telegram.Bot;

namespace MyerSplash.Core.Services
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}
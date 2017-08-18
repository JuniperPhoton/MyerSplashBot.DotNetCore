using Telegram.Bot;

namespace MyerSplash.Core.Services
{
    public interface IBotService
    {
        /// <summary>
        /// The client to communicate with telegram bot.
        /// </summary>
        /// <returns></returns>
        TelegramBotClient Client { get; }
    }
}
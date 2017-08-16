using Telegram.Bot.Types;

namespace MyerSplash.Core.Services
{
    public interface IMessageService
    {
        void Echo(Update update);
    }
}
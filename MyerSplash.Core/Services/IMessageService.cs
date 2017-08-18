using Telegram.Bot.Types;

namespace MyerSplash.Core.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// Echo a response when receiving update.
        /// </summary>
        /// <param name="update">Update object</param>
        /// <returns></returns>
        string Echo(Update update);

        /// <summary>
        /// Echo a text response
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string Echo(string text);
    }
}
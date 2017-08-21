using Telegram.Bot.Types;

namespace MyerSplash.Core.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// Echo a response when receiving message.
        /// </summary>
        /// <param name="message">message object</param>
        /// <returns></returns>
        string Echo(Message message);

        /// <summary>
        /// Echo a text response
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string Echo(string text);
    }
}
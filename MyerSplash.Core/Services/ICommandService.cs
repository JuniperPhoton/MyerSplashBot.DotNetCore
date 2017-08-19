using MyerSplash.Core.Handlers;

namespace MyerSplash.Core.Services
{
    public interface ICommandService
    {
        /// <summary>
        /// Get command from message text.
        /// For example, the text is like: "/today I want to get today's wallpaper"
        /// </summary>
        /// <param name="text">text from <see cref=Telegram.Bot.Types.Message.Text></param>
        /// <returns></returns>
        ICommand GetCommandFromMessageText(string text);
    }
}
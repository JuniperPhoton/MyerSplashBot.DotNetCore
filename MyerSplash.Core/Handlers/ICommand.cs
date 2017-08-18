using Telegram.Bot.Types;

namespace MyerSplash.Core.Handlers
{
    /// <summary>
    /// Implement this method to handle a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Handle command.
        /// </summary>
        /// <param name="message">Message from a <see cref=Telegram.Bot.Types.Update></param>
        void HandleCommand(Message message);
    }
}
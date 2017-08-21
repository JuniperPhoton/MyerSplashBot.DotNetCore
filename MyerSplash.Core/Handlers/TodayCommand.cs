using System;
using MyerSplash.Core.Services;
using MyerSplash.Shared.Logger;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyerSplash.Core.Handlers
{
    public class TodayCommand : ICommand
    {
        public const string NAME = "today";

        private readonly IBotService _botService;
        private readonly IAssetService _assetService;

        public TodayCommand(IBotService botService, IAssetService assetService)
        {
            _botService = botService;
            _assetService = assetService;
        }

        public void HandleCommand(Message message)
        {
            var time = DateTime.Now;
            var timeString = time.ToString("yyyyMMdd");
            var thumbUrl = _assetService.GetThumburl(timeString);
            var rawUrl = _assetService.GetRawurl(timeString);

            _botService.Client.SendTextMessageAsync(message.Chat.Id,
                    _assetService.GetResponseText(thumbUrl, rawUrl),
                    ParseMode.Markdown, false);

            Logger.Info(NAME, $"sent callback {thumbUrl}");
        }
    }
}
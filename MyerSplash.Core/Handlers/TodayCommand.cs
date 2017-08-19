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

        private IBotService _service;

        public TodayCommand(IBotService service)
        {
            _service = service;
        }

        public void HandleCommand(Message message)
        {
            var time = DateTime.Now;
            var timeString = time.ToString("yyyyMMdd");
            var thumbUrl = $"http://juniperphoton.net/myersplash/wallpapers/thumbs/{timeString}.jpg";
            var largeUrl = $"http://juniperphoton.net/myersplash/wallpapers/{timeString}.jpg";
            _service.Client.SendTextMessageAsync(message.Chat.Id,
                    $"Here is today's wallpaper, enjoy [it]({thumbUrl}) :P. [Download this]({largeUrl}).",
                    ParseMode.Markdown, false);

            Logger.Info(NAME, $"sent callback {thumbUrl}");
        }
    }
}
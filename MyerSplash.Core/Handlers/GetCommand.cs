using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyerSplash.Core.Services;
using MyerSplash.Shared.Logger;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using IOFile = System.IO.File;

namespace MyerSplash.Core.Handlers
{
    public class GetCommand : ICommand
    {
        private string TAG => nameof(GetCommand);

        public const string NAME = "get";

        private static readonly IEnumerable<Regex> FullDateRegexs = new List<Regex>(){
                new Regex(@"\d\d\d\d\d\d\d\d"),
                new Regex(@"\d\d\d\d.\d\d.\d\d")
        };

        private static readonly IEnumerable<Regex> DateRegexs = new List<Regex>(){
                new Regex(@"\d\d\d\d"),
                new Regex(@"\d\d.\d\d")
        };

        private readonly IBotService _botService;
        private readonly IAssetService _assetService;

        public GetCommand(IBotService service, IAssetService assetService)
        {
            _botService = service;
            _assetService = assetService;
        }

        public void HandleCommand(Message message)
        {
            var text = message.Text;

            var fileName = ExtractFileName(text);
            if (string.IsNullOrEmpty(fileName))
            {
                _botService.Client.SendTextMessageAsync(message.Chat.Id, "Sorry, I can't understand this message.");
                Logger.Warning(NAME, $"message not recognized: {text}");
                return;
            }

            var thumbPath = _assetService.GetThumbPath(fileName);
            var rawPath = _assetService.GetRawPath(fileName);
            if (IOFile.Exists(thumbPath) && IOFile.Exists(rawPath))
            {
                Logger.Info(NAME, $"{fileName}.jpg file found and sending response");
                var thumbUrl = _assetService.GetThumburl(fileName);
                var rawUrl = _assetService.GetRawurl(fileName);
                var time = DateTime.Now;
                var timeString = time.ToString("yyyyMMdd");
                var response = _assetService.GetResponseText(timeString == fileName, thumbUrl, rawUrl);
                _botService.Client.SendTextMessageAsync(message.Chat.Id,
                                            response,
                                            ParseMode.Markdown);
            }
            else
            {
                Logger.Warning(NAME, $"{fileName}.jpg file NOT found");
                _botService.Client.SendTextMessageAsync(message.Chat.Id, $"Can't find the wallpaer in {fileName} :(");
            }
        }

        public string ExtractFileName(string text)
        {
            var name = "";
            name = ExtractFileName(text, FullDateRegexs, (s) =>
            {
                return s;
            });
            if (string.IsNullOrEmpty(name))
            {
                name = ExtractFileName(text, DateRegexs, (s) =>
                {
                    var year = DateTime.Now.ToString("yyyy");
                    return $"{year}{s}";
                });
            }
            return name;
        }

        public string ExtractFileName(string text, IEnumerable<Regex> regexs, Func<string, string> getName)
        {
            var name = "";
            foreach (var regex in regexs)
            {
                var match = regex.Match(text);
                if (match.Success)
                {
                    name = getName.Invoke(match.Value.Replace(".", ""));
                    break;
                }
            }
            return name;
        }
    }
}
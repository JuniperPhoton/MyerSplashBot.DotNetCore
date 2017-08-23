using MyerSplash.Core.Services;
using MyerSplash.Shared.Logger;
using Telegram.Bot.Types;

namespace MyerSplash.Core.Handlers
{
    public class UploadCommand : ICommand
    {
        private enum Kind
        {
            Thumb,
            Large
        }

        public const string NAME = "UploadCommand";

        private BotConfiguration _config;
        private IBotService _botService;
        private IFileService _fileService;

        public UploadCommand(IBotService botService, IFileService fileService, BotConfiguration configuration)
        {
            _config = configuration;
            _botService = botService;
            _fileService = fileService;
        }

        public void HandleCommand(Message message)
        {
            if (message.Document == null)
            {
                return;
            }

            var id = message.Chat.Id;

            var kind = message.Caption.Contains(_config.UploadThumbCommand) ? Kind.Thumb : Kind.Large;

            var fileId = message.Document.FileId;

            var file = _fileService.QueryFileAsync(fileId).GetAwaiter().GetResult();
            if (file == null)
            {
                _botService.Client.SendTextMessageAsync(id, $"some thing went wrong while quering file by id: {fileId}");
                return;
            }

            var downloaded = _fileService.DownloadFileByPathAsync(file.FilePath, "d:/test.jpg").GetAwaiter().GetResult();
            if (downloaded)
            {
                _botService.Client.SendTextMessageAsync(id, "file saved");
            }
            else
            {
                _botService.Client.SendTextMessageAsync(id, "failed to save file");
            }
        }
    }
}
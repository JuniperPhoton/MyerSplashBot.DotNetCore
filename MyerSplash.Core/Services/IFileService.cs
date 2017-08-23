using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MyerSplash.Core.Services
{
    public interface IFileService
    {
        Task<Telegram.Bot.Types.File> QueryFileAsync(string fileId);

        Task<bool> DownloadFileByPathAsync(string filePath, string targetFilePath);

        Task<bool> DownloadFileByUrlAsync(string url, string targetFilePath);
    }
}
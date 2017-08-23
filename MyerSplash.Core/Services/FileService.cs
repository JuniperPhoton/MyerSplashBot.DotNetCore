using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MyerSplash.Core.Data;
using MyerSplash.Shared.Logger;
using Newtonsoft.Json;
using TGFile = Telegram.Bot.Types.File;

namespace MyerSplash.Core.Services
{
    public class FileService : IFileService
    {
        private string TAG => nameof(FileService);

        private string GET_FILE
        {
            get
            {
                return $"{Request.BASE_URL}/bot{_config.Token}/getFile?file_id=";
            }
        }

        private string DOWNLOAD_FILE
        {
            get
            {
                return $"{Request.BASE_URL}/file/bot{_config.Token}/";
            }
        }

        private BotConfiguration _config;
        private CancellationTokenSource _cts = new CancellationTokenSource(5000);

        public FileService(BotConfiguration config)
        {
            _config = config;
        }

        public async Task<TGFile> QueryFileAsync(string fileId)
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"{GET_FILE}{fileId}";
                Logger.Info(TAG, $"query file:{url}");
                try
                {
                    var response = await httpClient.GetAsync(url, _cts.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var respString = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<GetFileResponse>(respString);
                    if (resp.IsOk)
                    {
                        return resp.File;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
                return null;
            }
        }

        public async Task<bool> DownloadFileByPathAsync(string filePath, string targetFilePath)
        {
            var url = $"{DOWNLOAD_FILE}{filePath}";
            return await DownloadFileByUrlAsync(url, targetFilePath);
        }

        public async Task<bool> DownloadFileByUrlAsync(string url, string targetFilePath)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(url, _cts.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    using (response.Content)
                    {
                        var content = await response.Content.ReadAsByteArrayAsync();
                        await System.IO.File.WriteAllBytesAsync(targetFilePath, content);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
                return false;
            }
        }
    }
}
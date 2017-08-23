using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace MyerSplash.Core.Data
{
    public class GetFileResponse
    {
        [JsonProperty("ok")]
        public string Result { get; set; }

        [JsonProperty("result")]
        public File File { get; set; }

        [JsonIgnore]
        public bool IsOk
        {
            get
            {
                return Result.ToLower() == "ok";
            }
        }
    }
}
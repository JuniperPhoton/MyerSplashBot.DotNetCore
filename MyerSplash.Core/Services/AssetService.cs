using MyerSplash.Core.Data;

namespace MyerSplash.Core.Services
{
    public class AssetService : IAssetService
    {
        public string GetThumburl(string name)
        {
            return $"{Request.BASE_SELF_URL}/myersplash/wallpapers/thumbs/{name}.jpg";
        }

        public string GetRawurl(string name)
        {
            return $"{Request.BASE_SELF_URL}/myersplash/wallpapers/{name}.jpg";
        }

        public string GetThumbPath(string name)
        {
            return $"/var/www/myersplash/wallpapers/thumbs/{name}.jpg";
        }

        public string GetRawPath(string name)
        {
            return $"/var/www/myersplash/wallpapers/{name}.jpg";
        }

        public string GetResponseText(bool today, string thumb, string raw)
        {
            var date = today ? "today" : "that day";
            return $"Here is {date}'s wallpaper, enjoy [it]({thumb}) :P. [Download this]({raw}).";
        }

        public string GetResponseText(string thumb, string raw)
        {
            return GetResponseText(true, thumb, raw);
        }
    }
}
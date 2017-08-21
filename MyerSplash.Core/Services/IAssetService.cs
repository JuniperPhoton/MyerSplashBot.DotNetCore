namespace MyerSplash.Core.Services
{
    public interface IAssetService
    {
        string GetThumburl(string name);
        string GetRawurl(string name);

        string GetThumbPath(string name);
        string GetRawPath(string name);

        string GetResponseText(string thumb, string raw);
        string GetResponseText(bool today, string thumb, string raw);
    }
}
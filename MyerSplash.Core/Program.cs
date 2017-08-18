using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;

namespace MyerSplash.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                             .UseKestrel()
                             .UseUrls("http://127.0.0.1:8443")
                             .UseContentRoot(Directory.GetCurrentDirectory())
                             .UseIISIntegration()
                             .UseStartup<Startup>()
                             .Build();
            host.Run();
        }
    }
}
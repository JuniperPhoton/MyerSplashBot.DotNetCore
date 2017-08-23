using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;

namespace MyerSplash.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                        .AddCommandLine(args)
                        .Build();
            var host = new WebHostBuilder()
                             .UseKestrel()
                             .UseConfiguration(config)
                             .UseUrls("http://127.0.0.1:8443")
                             .UseContentRoot(Directory.GetCurrentDirectory())
                             .UseIISIntegration()
                             .UseStartup<Startup>()
                             .Build();
            host.Run();
        }
    }
}
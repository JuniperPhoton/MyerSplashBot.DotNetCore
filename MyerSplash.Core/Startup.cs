using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyerSplash.Core.Handlers;
using MyerSplash.Core.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyerSplash.Core
{
    public class Startup
    {
        /// <summary>
        /// The configuration should look like this:
        /// {
        //   "Logging": {
        //     "IncludeScopes": false,
        //     "LogLevel": {
        //       "Default": "Warning"
        //     }
        //   },
        //   "BotConfiguration": {
        //     "Token": ""
        //   }
        // }
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IAssetService, AssetService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<ICommandService, CommandService>();
            services.AddScoped<UploadCommand>();
            services.AddSingleton<TodayCommand>();
            services.AddSingleton<StartCommand>();
            services.AddSingleton<GetCommand>();
            services.AddScoped<IFileService, FileService>();
            services.AddSingleton(Configuration.GetSection("BotConfiguration").Get<BotConfiguration>());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            if (env.IsDevelopment())
            {
                var s = app.ApplicationServices.GetService<ICommandService>();
                var st = s.GetCommandFromMessageText("/uploadttt");
                var isn = st == null;
            }
        }
    }
}

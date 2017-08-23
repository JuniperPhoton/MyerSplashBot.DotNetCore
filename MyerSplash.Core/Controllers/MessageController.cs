using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyerSplash.Core.Services;
using Telegram.Bot.Types;

namespace MyerSplash.Core.Controllers
{
    /// <summary>
    /// Controller for handling requests.
    /// Note that the <see cref=Microsoft.AspNetCore.Mvc.RouteAttribute> specified the
    /// route path of requesting url.
    /// If the domain is example.com, then the whole url should be https://example.com/api/message,
    /// which is the exact same url when setting webhook.
    /// </summary>
    [Route("/api/message")]
    public class MessageController : Controller
    {
        private IMessageService _messageService;
        private IHostingEnvironment _env;

        private string DefaultResponse => "I can't understand your word yet :(";

        public MessageController(IMessageService service, IHostingEnvironment env)
        {
            _messageService = service;
            _env = env;
        }

        [HttpPost]
        public string ReceivedPostRequest([FromBody]Update update)
        {
            if (update == null || update.Message == null)
            {
                return DefaultResponse;
            }
            return _messageService.Echo(update.Message);
        }

        [HttpGet]
        public string ReceivedGetRequest([FromQuery]string url)
        {
            return "Got you";
        }
    }
}

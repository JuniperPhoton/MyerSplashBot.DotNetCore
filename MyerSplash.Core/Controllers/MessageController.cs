using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyerSplash.Core.Services;
using Telegram.Bot.Types;

namespace MyerSplash.Core.Controllers
{
    [Route("/api/Message")]
    public class MessageController : Controller
    {
        private IMessageService _messageService;

        public MessageController(IMessageService service)
        {
            _messageService = service;
        }

        [HttpPost]
        public void GetPost(Update update)
        {
            _messageService.Echo(update);
        }
    }
}

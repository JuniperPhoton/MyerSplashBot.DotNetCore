using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MyerSplash.Core.Handlers;
using MyerSplash.Core.Services;

namespace MyerSplash.Core.Services
{
    public class CommandService : ICommandService
    {
        private static Dictionary<string, Type> CommandDictionary = new Dictionary<string, Type>()
        {
            {StartCommand.NAME,typeof(StartCommand)},
            {TodayCommand.NAME,typeof(TodayCommand)}
        };

        private IBotService _service;
        private IServiceProvider _provider;

        public CommandService(IBotService service, IServiceProvider provider)
        {
            _service = service;
            _provider = provider;
        }

        public ICommand GetCommandFromMessageText(string text)
        {
            ICommand command = null;
            if (!text.StartsWith('/'))
            {
                command = null;
            }
            var blankIndex = text.IndexOf(' ');
            if (blankIndex < 0) blankIndex = text.Length - 1;
            var commandName = text.Substring(1, blankIndex);
            if (CommandDictionary.ContainsKey(commandName))
            {
                var commandType = CommandDictionary[commandName];
                command = _provider.GetService(commandType) as ICommand;
            }
            return command;
        }
    }
}
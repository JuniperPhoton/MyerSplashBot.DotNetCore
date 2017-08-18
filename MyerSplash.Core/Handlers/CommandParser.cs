using System;
using System.Collections.Generic;
using System.Linq;
using MyerSplash.Core.Services;

namespace MyerSplash.Core.Handlers
{
    public class CommandParser
    {
        private static Dictionary<string, Type> CommandDictionary = new Dictionary<string, Type>()
        {
            {StartCommand.NAME,typeof(StartCommand)},
            {TodayCommand.NAME,typeof(TodayCommand)}
        };

        private IBotService _service;

        public CommandParser(IBotService service)
        {
            _service = service;
        }

        public ICommand ParseMessage(string text)
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
                command = Activator.CreateInstance(commandType, _service) as ICommand;
            }
            return command;
        }
    }
}
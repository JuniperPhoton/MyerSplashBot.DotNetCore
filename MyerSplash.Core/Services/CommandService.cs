using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MyerSplash.Core.Handlers;
using MyerSplash.Core.Services;
using MyerSplash.Shared.Logger;

namespace MyerSplash.Core.Services
{
    public class CommandService : ICommandService
    {
        private const string TAG = "CommandService";

        public static Dictionary<string, Type> CommandDictionary = new Dictionary<string, Type>()
        {
            {StartCommand.NAME,typeof(StartCommand)},
            {TodayCommand.NAME,typeof(TodayCommand)},
            {GetCommand.NAME,typeof(GetCommand)}
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
            if (blankIndex < 0) blankIndex = text.Length;
            var commandName = text.Substring(1, blankIndex - 1);
            Logger.Debug(TAG, $"command name is {commandName}.");
            if (CommandDictionary.ContainsKey(commandName))
            {
                Logger.Debug(TAG, $"has command: {commandName}.");
                var commandType = CommandDictionary[commandName];
                command = _provider.GetService(commandType) as ICommand;
            }
            else
            {
                Logger.Debug(TAG, $"command not found for: {commandName}.");
            }
            return command;
        }
    }
}
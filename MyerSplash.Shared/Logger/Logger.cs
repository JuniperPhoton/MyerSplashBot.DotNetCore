using System;

namespace MyerSplash.Shared.Logger
{
    /// <summary>
    /// A wrapper of Console class to write pretty log.
    /// </summary>
    public static class Logger
    {
        private static string PREFIX => "=============";

        public static void Debug(string tag, string msg)
        {
            var level = LogLevel.Debug;
            Console.WriteLine($"{PREFIX} {Enum.GetName(typeof(LogLevel), level)} {msg}");
        }

        public static void Info(string tag, string msg)
        {
            var level = LogLevel.Info;
            Console.WriteLine($"{Enum.GetName(typeof(LogLevel), level)}{PREFIX} {msg}");
        }

        public static void Warning(string tag, string msg)
        {
            var level = LogLevel.Warning;
            Console.WriteLine($"{Enum.GetName(typeof(LogLevel), level)}{PREFIX} {msg}");
        }

        public static void Error(string tag, string msg)
        {
            var level = LogLevel.Error;
            Console.WriteLine($"{Enum.GetName(typeof(LogLevel), level)}{PREFIX} {msg}");
        }
    }

    enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }
}
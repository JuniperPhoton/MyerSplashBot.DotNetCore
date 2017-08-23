using System;

namespace MyerSplash.Shared.Logger
{
    enum LogLevel
    {
        // For debug purpose
        Debug,

        // Info something
        Info,

        // There is a warning
        Warning,

        //Error occurs
        Error
    }

    /// <summary>
    /// A wrapper of Console class to write pretty log.
    /// It has four levels <see cref=LogLevel>.
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

        public static void Warning(Exception e)
        {
            var level = LogLevel.Warning;
            Console.WriteLine($"{Enum.GetName(typeof(LogLevel), level)}{PREFIX} {e.Message}");
        }

        public static void Error(Exception e)
        {
            var level = LogLevel.Error;
            Console.WriteLine($"{Enum.GetName(typeof(LogLevel), level)}{PREFIX} {e.Message}");
        }
    }
}
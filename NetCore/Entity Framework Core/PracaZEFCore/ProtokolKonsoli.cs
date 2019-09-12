using Microsoft.Extensions.Logging;
using System;
using static System.Console;

namespace PracaZEFCore
{
    public class DostawcaProtokoluKonsli : ILoggerProvider 
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ProtokolKonsoli();
        }

        public void Dispose() { }
    }

    public class ProtokolKonsoli : ILogger
    {
        public IDisposable BeginScope<Tstate>(Tstate state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Information:
                case LogLevel.None:
                    return false;
                case LogLevel.Debug:
                case LogLevel.Warning:
                case LogLevel.Error:
                case LogLevel.Critical:
                default:
                    return true;
            };

        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (eventId.Id == 200100)
            {
                Write($"Poziom: {logLevel}, ID zdzrzenia {eventId}");

                if (state != null)
                {
                    Write($", Stan: {state}");
                }
                if (exception != null)
                {
                    Write($", Wyjatek: {exception.Message}");
                }
                WriteLine();
            }
        }
    }
}

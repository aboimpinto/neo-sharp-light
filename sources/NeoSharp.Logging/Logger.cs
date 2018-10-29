using System;
using Microsoft.Extensions.Logging;

namespace NeoSharp.Logging
{
    public class Logger<TCategory> : ILogger
    {
        private readonly ILogger<TCategory> logger;

        public Logger(ILogProvider logProvider)
        {
            this.logger = logProvider.CreateLogger<TCategory>();
        }

        public void LogDebug(string message)
        {
            this.logger.Log(LogLevel.Debug, message);
        }

        public void LogTrace(string message)
        {
            this.logger.Log(LogLevel.Trace, message);
        }

        public void LogError(string message)
        {
            this.logger.Log(LogLevel.Error, message);
        }

        public void LogException(string message, Exception exception)
        {
            this.logger.Log(LogLevel.Error, exception, message);
        }
    }
}

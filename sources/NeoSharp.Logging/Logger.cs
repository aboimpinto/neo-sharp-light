using System;
using Microsoft.Extensions.Logging;

namespace NeoSharp.Logging
{
    public class Logger<TCategory> : ILogger
    {
        private readonly ILogger<TCategory> _logger;

        #region Constructor 
        public Logger(ILogProvider logProvider)
        {
            this._logger = logProvider.CreateLogger<TCategory>();
        }
        #endregion

        #region ILogger implementation 
        public void LogDebug(string message)
        {
            this._logger.Log(LogLevel.Debug, message);
        }

        public void LogTrace(string message)
        {
            this._logger.Log(LogLevel.Trace, message);
        }

        public void LogError(string message)
        {
            this._logger.Log(LogLevel.Error, message);
        }

        public void LogException(string message, Exception exception)
        {
            this._logger.Log(LogLevel.Error, exception, message);
        }
        #endregion
    }
}

using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace NeoSharp.Logging.NLog
{
    public class NLogProvider : ILogProvider
    {
        #region Private Fields 
        private readonly LoggerFactory _loggerFactory;
        #endregion

        #region Constructor 
        public NLogProvider()
        {
            this._loggerFactory = new LoggerFactory();
            this._loggerFactory.AddNLog(new NLogProviderOptions
            {
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true
            });

            LogManager.LoadConfiguration("nlog.config");
        }
        #endregion

        #region ILogProvider implementation 
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string category)
        {
            return this._loggerFactory.CreateLogger(category);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace NeoSharp.Logging.NLog
{
    public class NLogProvider : ILogProvider
    {
        private readonly LoggerFactory loggerFactory;

        public NLogProvider()
        {
            this.loggerFactory = new LoggerFactory();
            this.loggerFactory.AddNLog(new NLogProviderOptions
            {
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true
            });

            LogManager.LoadConfiguration("nlog.config");
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string category)
        {
            return this.loggerFactory.CreateLogger(category);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}

using System;

namespace NeoSharp.Logging
{
    public interface ILogger
    {
        void LogDebug(string message);

        void LogTrace(string message);

        void LogError(string message);

        void LogException(string message, Exception exception);
    }
}

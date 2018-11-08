using System;
using Microsoft.Extensions.Logging;

namespace NeoSharp.Logging
{
    public class Logger<TCategory> : NeoSharp.Logging.ILogger<TCategory>
    {
        private readonly Microsoft.Extensions.Logging.ILogger<TCategory> logger;

        /// <inheritdoc />
        public Logger(ILogProvider logProvider)
        {
            this.logger = logProvider.CreateLogger<TCategory>();
        }

        /// <inheritdoc />
        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            this.logger.Log(logLevel, message, args);
        }

        /// <inheritdoc />
        public void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            this.logger.Log(logLevel, exception, message, args);
        }

        /// <inheritdoc />
        public void LogCritical(Exception exception, string message, params object[] args)
        {
            this.logger.LogCritical(exception,message, args);
        }

        /// <inheritdoc />
        public void LogCritical(string message, params object[] args)
        {
            this.LogCritical(message, args);
        }

        /// <inheritdoc />
        public void LogDebug(Exception exception, string message, params object[] args)
        {
            this.logger.LogDebug(exception, message, args);
        }

        /// <inheritdoc />
        public void LogDebug(string message, params object[] args)
        {
            this.logger.LogDebug(message, args);
        }

        /// <inheritdoc />
        public void LogError(Exception exception, string message, params object[] args)
        {
            this.logger.LogError(exception, message, args);
        }

        /// <inheritdoc />
        public void LogError(string message, params object[] args)
        {
            this.logger.LogError(message, args);
        }

        /// <inheritdoc />
        public void LogInformation(Exception exception, string message, params object[] args)
        {
            this.logger.LogInformation(exception, message, args);
        }

        /// <inheritdoc />
        public void LogInformation(string message, params object[] args)
        {
            this.logger.LogInformation(message, args);
        }

        /// <inheritdoc />
        public void LogTrace(Exception exception, string message, params object[] args)
        {
            this.logger.LogTrace(exception, message, args);
        }

        /// <inheritdoc />
        public void LogTrace(string message, params object[] args)
        {
            this.logger.LogTrace(message, args);
        }

        /// <inheritdoc />
        public void LogWarning(Exception exception, string message, params object[] args)
        {
            this.logger.LogWarning(exception, message, args);
        }

        /// <inheritdoc />
        public void LogWarning(string message, params object[] args)
        {
            this.logger.LogWarning(message, args);
        }
    }

    public interface ILogger<TCategory>
    {
        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogDebug(exception, "Error while processing request from {Address}", address)</example>
        void LogDebug(Exception exception, string message, params object[] args);

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogDebug("Processing request from {Address}", address)</example>
        void LogDebug(string message, params object[] args);

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogTrace(exception, "Error while processing request from {Address}", address)</example>
        void LogTrace(Exception exception, string message, params object[] args);

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogTrace("Processing request from {Address}", address)</example>
        void LogTrace(string message, params object[] args);

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogInformation(exception, "Error while processing request from {Address}", address)</example>
        void LogInformation(Exception exception, string message, params object[] args);

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogInformation("Processing request from {Address}", address)</example>
        void LogInformation(string message, params object[] args);

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogWarning(exception, "Error while processing request from {Address}", address)</example>
        void LogWarning(Exception exception, string message, params object[] args);

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogWarning("Processing request from {Address}", address)</example>
        void LogWarning(string message, params object[] args);

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogError(exception, "Error while processing request from {Address}", address)</example>
        void LogError(Exception exception, string message, params object[] args);

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogError("Processing request from {Address}", address)</example>
        void LogError(string message, params object[] args);

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogCritical(exception, "Error while processing request from {Address}", address)</example>
        void LogCritical(Exception exception, string message, params object[] args);

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="message">Format string of the log message in message template format. Example: <code>"User {User} logged in from {Address}"</code></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>LogCritical("Processing request from {Address}", address)</example>
        void LogCritical(string message, params object[] args);

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void Log(LogLevel logLevel, string message, params object[] args);

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void Log(LogLevel logLevel, Exception exception, string message, params object[] args);
    }
}

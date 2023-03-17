using Microsoft.Extensions.Logging;

namespace MobileTemplate.Core.Logger
{
    /// <summary>
    /// Default logger provider
    /// </summary>
    public class LoggerProvider : ILoggerProvider
    {
        private ILogger logger;

        /// <summary>
        /// Returns a logger to the specified category
        /// </summary>
        public ILogger CreateLogger(string categoryName)
        {
            return this.logger ??= new Logger();
        }

        /// <summary>
        /// Clean up
        /// </summary>
        public void Dispose()
        {            
             return;
        }
    }
}

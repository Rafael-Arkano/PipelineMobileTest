using Microsoft.Extensions.Logging;

namespace MobileTemplate.Core.Logger
{
    /// <summary>
    /// Default logger factory to return the available provider
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        private ILoggerProvider provider;

        /// <summary>
        /// Adds a provider
        /// </summary>
        public void AddProvider(ILoggerProvider provider)
        {
            this.provider = provider;
        }


        /// <summary>
        /// Creates a logger
        /// </summary>
        public ILogger CreateLogger(string categoryName)
        {
            return this.provider.CreateLogger(categoryName);
        }


        /// <summary>
        /// Clean Up
        /// </summary>
        public void Dispose()
        {
            return;
        }
    }
}

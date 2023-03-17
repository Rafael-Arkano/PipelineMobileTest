using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MobileTemplate.Core.Logger
{
    /// <summary>
    /// Logger based on App Center
    /// </summary>
    public class Logger : ILogger
    {
        private bool started = false;
        private bool enabled = true;


        /// <summary>
        /// Initializes AppCenter
        /// </summary>
        private void SetupsAppCenter()
        {
            try
            {
                if (this.started) return;
                this.started = true;

                if (!string.IsNullOrEmpty(Constants.AppCenterDroid)
                    || string.IsNullOrEmpty(Constants.AppCenteriOS))
                {
                    this.enabled = true;

                }
                var appCentenerId = $"{Constants.AppCenterDroid}{Constants.AppCenteriOS}";
                AppCenter.Start(appCentenerId, typeof(Analytics), typeof(Crashes));
                Analytics.SetEnabledAsync(true);
                this.enabled = true;
            }
            catch
            {
                // Ignored, unable to start the analitics
            }
        }


        /// <inheritdoc//>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }


        /// <inheritdoc//>
        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {            
           return this.enabled;
        }


        /// <inheritdoc//>
        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!enabled) return;

            SetupsAppCenter();
            var properties = GetExtraParamters(state);
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    Analytics.TrackEvent(formatter(state, exception), properties);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    Analytics.TrackEvent(formatter(state, exception), properties);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    Crashes.TrackError(exception, properties);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    Crashes.TrackError(exception, properties);
                    break;
                default:
                    return;
            }
            return;
        }


        /// <summary>
        /// Creates a dictionary of parameters, if the value includes a ":" then
        /// takes the left part as the name of parameter and rest as the value
        /// </summary>
        private Dictionary<string, string> GetExtraParamters(params object[] parameters)
        {
            var properties = new Dictionary<string, string>();
            try
            {
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var value = parameters[i].ToString();
                        if (value.IndexOf(":") != -1)
                        {
                            var values = value.Split(new[] { ":" }, 2, StringSplitOptions.RemoveEmptyEntries);
                            properties.Add(values[0], values[1]);
                        }
                        else
                        {
                            properties.Add("Param " + i, parameters[i].ToString());
                        }
                    }
                }
            }
            catch
            {
                // Ignored. Failed to create the parameters dictionary
            }
            return properties;
        }
    }
}

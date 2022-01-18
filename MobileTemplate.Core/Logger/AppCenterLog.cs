using MvvmCross.Logging;
using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;


namespace MobileTemplate.Core.Logger
{
    public class AppCenterLog : IMvxLog
    {
        private bool started = false;
        private bool enabled = false;

        /// <summary>
        /// Initializes AppCenter
        /// </summary>
        private void StartAppCenter()
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
        }


        /// <inheriddoc/>
        public bool IsLogLevelEnabled(MvxLogLevel logLevel)
        {
            StartAppCenter();
            switch (logLevel)
            {
                case MvxLogLevel.Trace:
                    return false;
                case MvxLogLevel.Debug:
                    return false;
                case MvxLogLevel.Info:
                    return true;
                case MvxLogLevel.Warn:
                case MvxLogLevel.Error:
                    return true;
                case MvxLogLevel.Fatal:
                    return true;
                default:
                    return false;
            }
        }

        /// <inheriddoc/>
        public bool Log(MvxLogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] parameters)
        {
            if (!enabled) return true;
            
            StartAppCenter();
            var properties = GetExtraParamters(parameters);
            switch (logLevel)
            {
                case MvxLogLevel.Trace:
                    Analytics.TrackEvent(messageFunc(), properties);
                    break;
                case MvxLogLevel.Debug:
                    break;
                case MvxLogLevel.Info:
                    Analytics.TrackEvent(messageFunc(), properties);
                    break;
                case MvxLogLevel.Warn:
                    break;
                case MvxLogLevel.Error:
                    Crashes.TrackError(exception, properties);
                    break;
                case MvxLogLevel.Fatal:
                    Crashes.TrackError(exception, properties);
                    break;
                default:
                    return true;
            }
            return true;
        }


        /// <summary>
        /// Creates a dictionary of parameters, if the value includes a ":" then
        /// takes the left part as the name of parameter and rest as the value
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
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
                // Ignored. Failed to create the paramteres dictionary
            }
            return properties;
        }
    }
}

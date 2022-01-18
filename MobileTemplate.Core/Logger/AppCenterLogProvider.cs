
namespace MobileTemplate.Core.Logger
{
    using MvvmCross.Logging;
    using System;


    /// <summary>
    /// AppCenter log provider for MvvmCross
    /// </summary>
    public class AppCenterLogProvider : IMvxLogProvider
    {
        private AppCenterLog loggger = new AppCenterLog();

        /// <inheridtdoc/>
        public IMvxLog GetLogFor(Type type)
        {
            return loggger;
        }


        /// <inheridtdoc/>
        public IMvxLog GetLogFor<T>()
        {
            return loggger;
        }


        /// <inheridtdoc/>
        public IMvxLog GetLogFor(string name)
        {
            return loggger;
        }


        /// <inheridtdoc/>
        public IDisposable OpenMappedContext(string key, string value)
        {
            return null;
        }


        /// <inheridtdoc/>
        public IDisposable OpenNestedContext(string message)
        {
            return null;
        }
    }
}

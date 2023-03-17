namespace MobileTemplate.iOS
{
    using MvvmCross.Forms.Platforms.Ios.Core;
    using MvvmCross.Logging;
    using MobileTemplate.Core;
    using MobileTemplate.Core.Interfaces;
    using MobileTemplate.iOS.Services;
    using MvvmCross;
    using MvvmCross.Base;
    using MvvmCross.Plugin.Json;
    using MobileTemplate.Core.Logger;
    using LoggerFactory = Core.Logger.LoggerFactory;
    using LoggerProvider = Core.Logger.LoggerProvider;
    using MvvmCross.IoC;
    using Microsoft.Extensions.Logging;

    public class Setup : MvxFormsIosSetup<MvxApp, App>
    {
        /// <summary>
        /// Initializes the platform services
        /// </summary>
        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            Mvx.IoCProvider.RegisterSingleton<IMvxJsonConverter>(new MvxJsonConverter());
            Mvx.IoCProvider.RegisterSingleton<IPlatformService>(new PlatformService());
            base.InitializeFirstChance(iocProvider);
        }

        /// <summary>
        /// Setups the LogProvider
        /// </summary>
        /// <returns></returns>
        protected override ILoggerProvider CreateLogProvider()
        {
            return new LoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            return new LoggerFactory();
        }

    }
}
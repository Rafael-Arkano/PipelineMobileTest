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

    public class Setup : MvxFormsIosSetup<MvxApp, App>
    {
        /// <summary>
        /// Sets the log provider
        /// </summary>
        /// <returns></returns>
        public override MvxLogProviderType GetDefaultLogProviderType()
            => MvxLogProviderType.Console;


        /// <summary>
        /// Initializes the platform services
        /// </summary>
        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterSingleton<IMvxJsonConverter>(new MvxJsonConverter());
            Mvx.IoCProvider.RegisterSingleton<IPlatformService>(new PlatformService());
            base.InitializeFirstChance();

        }

        /// <summary>
        /// Setups the LogProvider
        /// </summary>
        /// <returns></returns>
        protected override IMvxLogProvider CreateLogProvider()
        {
            return new AppCenterLogProvider();
        }

    }
}
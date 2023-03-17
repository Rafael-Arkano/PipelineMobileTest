namespace MobileTemplate.Droid
{
    using Core;
    using MvvmCross.Forms.Platforms.Android.Core;
    using MvvmCross;
    using MvvmCross.Forms.Presenters;
    using Core.Interfaces;
    using Services;
    using MvvmCross.IoC;
    using MobileTemplate.Core.Logger;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Android setup class
    /// </summary>
    public class Setup : MvxFormsAndroidSetup<MvxApp, App>
    {
        /// <summary>
        /// Register the form presenter (MvvmCross)
        /// </summary>        
        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            var formsPresenter = base.CreateFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton(formsPresenter);
            return formsPresenter;
        }


        /// <summary>
        /// Initializes the platform services
        /// </summary>
        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            iocProvider.RegisterSingleton<IPlatformService>(new PlatformService());
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


        /// <summary>
        /// Creates the logger to use
        /// </summary>
        /// <returns></returns>
        protected override ILoggerFactory CreateLogFactory()
        {
            return new Core.Logger.LoggerFactory();
        }


    }
}
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System.Globalization;
using MobileTemplate.Core.ViewModels;
using MobileTemplate.Core.Services;
using MvvmCross;
using MvvmCross.Plugin.JsonLocalization;
using Microsoft.Extensions.Logging;

namespace MobileTemplate.Core
{
    /// <summary>
    /// MvvmCross Application
    /// </summary>
    public class MvxApp : MvxApplication
    {
        /// <summary>
        /// Initialize the services and defines the first viewmodel
        /// </summary>
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            var currentCulture = CultureInfo.CreateSpecificCulture("es-UY");
            CultureInfo.DefaultThreadCurrentCulture = currentCulture;
            RegisterAppStart<LoginViewModel>();
            InitializeTextProvider();
        }


        /// <summary>
        /// Initializes the localization provider
        /// </summary>
        private void InitializeTextProvider()
        {
            var builder = new TextProviderBuilder();
            Mvx.IoCProvider.RegisterSingleton<IMvxTextProviderBuilder>(builder);
            Mvx.IoCProvider.RegisterSingleton(builder.TextProvider);
        }
    }

}

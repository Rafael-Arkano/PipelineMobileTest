namespace MobileTemplate.Core.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// Root empty ViewModel for the AppCompact Activity
    /// </summary>
    public class StartUpViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public StartUpViewModel(ILoggerFactory logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}

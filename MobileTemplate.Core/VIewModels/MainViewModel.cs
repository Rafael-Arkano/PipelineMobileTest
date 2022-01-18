namespace MobileTemplate.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Main logic.
    /// Loasd the tabs to show on startup
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private bool viewAppeared;


        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }


        /// <summary>
        /// Load the tabs
        /// </summary>
        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            if (!this.viewAppeared)
            {
                this.viewAppeared = true;
                var tasks = new List<Task>();
                tasks.Add(NavigationService.Navigate<HomeViewModel>());
                tasks.Add(NavigationService.Navigate<ListItemsViewModel>());
                tasks.Add(NavigationService.Navigate<SettingsViewModel>());
                await Task.WhenAll(tasks);
            }
        }
    }
}

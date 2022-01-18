namespace MobileTemplate.Core.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
   

    /// <summary>
    /// Settings UI logic
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        private IMvxCommand logoutCommand;


        /// <summary>
        /// Command to login
        /// </summary>
        public IMvxCommand LogoutCommand
        {
            get
            {
                return logoutCommand ?? (logoutCommand = new MvxCommand(async () =>
                {
                    await NotificationService.ConfirmAsync("Confirmation", "LogoutQuestion", async confirmed =>
                    {
                        if (!confirmed) return;
                        await this.AuthenticationService.LogOutAsync();
                    });
                }));
            }
        }


        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }     
    }
}

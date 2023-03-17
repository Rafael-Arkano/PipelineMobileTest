namespace MobileTemplate.Core.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MobileTemplate.Core.Helpers;
    using MobileTemplate.Core.Models;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
     using System.Collections.Generic;


    /// <summary>
    /// Home logic
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        /// <summary>
        /// Menu
        /// </summary>
        public List<MenuOption> Menu { get; private set; }


        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public HomeViewModel(ILoggerFactory logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }



        /// <summary>
        /// Prepares the local variables
        /// </summary>
        public override void Prepare()
        {
            this.Menu = new List<MenuOption>(new List<MenuOption> {
                new MenuOption
                {
                    Text = GetText("Option"),
                    Icon = IconConstants.Alert,
                    Command = new MvxCommand(() =>
                    {                        
                    }),
                },
                new MenuOption
                {
                    Text = GetText("EntryForm"),
                    Icon = IconConstants.FileDocumentEdit,
                    Command = new MvxCommand(async () =>
                    {
                         await this.NavigationService.Navigate<EntryFormViewModel>();
                    }),
                },
                new MenuOption
                {
                    Text = GetText("Camera"),
                    Icon = IconConstants.Camera,
                    Command = new MvxCommand(() =>
                    {                       
                    }),
                },
                new MenuOption
                {
                    Text = GetText("Synchronize"),
                    Icon = IconConstants.Refresh,
                    Command = new MvxCommand(async () =>
                    {
                        await this.NavigationService.Navigate<SynchronizationViewModel>();
                    }),
                },
            });
        }

    }
}

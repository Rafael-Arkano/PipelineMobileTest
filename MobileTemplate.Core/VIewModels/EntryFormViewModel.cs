namespace MobileTemplate.Core.ViewModels
{
    using MobileTemplate.Core.Helpers;
    using MobileTemplate.Core.Models;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
     using System.Collections.Generic;


    /// <summary>
    /// Home view logic
    /// </summary>
    public class EntryFormViewModel : BaseViewModel
    {
        /// <summary>
        /// Menu
        /// </summary>
        public List<MenuOption> Menu { get; private set; }

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public EntryFormViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }




    }
}

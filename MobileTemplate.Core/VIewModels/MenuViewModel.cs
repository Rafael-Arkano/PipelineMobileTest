//namespace MobileTemplate.Core.ViewModels
//{
//    using Messages;
//    using MobileTemplate.Core.Models;
//    using MobileTemplate.Models;
//    using MvvmCross.Commands;
//    using MvvmCross.Logging;
//    using MvvmCross.Navigation;
//    using MvvmCross.Plugin.Messenger;
//    using System.Collections.Generic;
//    using System.Collections.ObjectModel;
//    using System.Threading.Tasks;
//    using MobileTemplate.Core.Helpers;

//    /// <summary>
//    /// Navigation drawer (menu) logic
//    /// </summary>
//    public class MenuViewModel : BaseViewModel
//    {
//        private MenuOption selectedOption;
//        private MvxSubscriptionToken userUpdatedSubscriptionToken;
//        private IMvxCommand logoutCommand;

//        /// <summary>
//        /// Menu
//        /// </summary>
//        public ObservableCollection<MenuOption> Menu { get; set; }


//        /// <summary>
//        /// User
//        /// </summary>
//        public User User { get; set; }


//        /// <summary>
//        /// Selected account
//        /// </summary>
//        public MenuOption SelectedOption
//        {
//            get => this.selectedOption;
//            set
//            {
//                this.selectedOption = value;
//                if (value?.Command == null) return;
//                this.selectedOption = null;
//                RaisePropertyChanged(() => this.SelectedOption);
//                value.Command.Execute(null);
//            }
//        }


//        /// <summary>
//        /// Gets by DI the required services
//        /// </summary>
//        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
//        {
//        }



//        /// <summary>
//        /// Prepares the local variables
//        /// </summary>
//        public override void Prepare()
//        {
//            SubscribeToUserUpdatedMessage();
         
//            this.Menu = new ObservableCollection<MenuOption>(new List<MenuOption> {
//                new MenuOption
//                {
//                    Description = GetText("Sync"),
//                    Icon = IconConstants.Refresh,
//                    Command = new MvxCommand(async () =>
//                    {
//                        await NotificationService.ConfirmAsync("Confirmation", "SynchronizeQuestion", async confirmed =>
//                        {
//                            if (!confirmed) return;
//                            await this.NavigationService.Navigate<SynchronizationViewModel>();
//                        });
//                    }),
//                },
//                new MenuOption
//                {
//                    Description = GetText("Logout"),
//                    Icon = IconConstants.Close,
//                    Command = new MvxCommand(async () =>
//                    {
//                        await NotificationService.ConfirmAsync("Confirmation", "LogoutQuestion", async confirmed =>
//                        {
//                            if (!confirmed) return;
//                            await this.AuthenticationService.LogOutAsync();
//                        });
//                    }),
//                },             
//            });
//        }


//        /// <summary>
//        /// Initializes the menu
//        /// </summary>
//        /// <returns></returns>
//        public override Task Initialize()
//        {
//            return Task.Run(() =>
//            {
//            });
//        }


//        /// <summary>
//        /// Subscribes the viewmodel to handle theme updated messages 
//        /// </summary>
//        public void SubscribeToUserUpdatedMessage()
//        {
//            this.userUpdatedSubscriptionToken = this.MessengerService.Subscribe<UserUpdatedMessage>(message =>
//            {
//                this.User = message.User;
//                RaisePropertyChanged(() => this.User);
//            });
//        }


//        /// <summary>
//        /// Release the inner objects
//        /// </summary>
//        public void Release()
//        {
//            if (userUpdatedSubscriptionToken == null) return;
//            this.MessengerService.Unsubscribe<UserAuthenticatedMessage>(userUpdatedSubscriptionToken);
//            this.userUpdatedSubscriptionToken = null;
//        }
//    }
//}

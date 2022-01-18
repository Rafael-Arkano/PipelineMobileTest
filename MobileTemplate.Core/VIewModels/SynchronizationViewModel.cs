namespace MobileTemplate.Core.ViewModels
{
    using MobileTemplate.Core.Messages;
    using MobileTemplate.Core.Models;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Synchronization ViewModel
    /// </summary>
    public class SynchronizationViewModel : BaseViewModel
    {
        private IMvxCommand startSynchronizationCommand;
        private IMvxCommand okCommand;
        private MvxSubscriptionToken synchronizationMessageSubscriptionToken;
        private CancellationTokenSource cancellationTokenSource;
        private bool canBeCanceled = false;

        /// <summary>
        /// Synchronization Event Message
        /// </summary>
        public string SynchronizationMessage { get; set; }


        /// <summary>
        /// Progress bar porcentage
        /// </summary>
        public double Progress { get; set; }


        /// <summary>
        /// Progress message (like 20%)
        /// </summary>
        public string ProgressMessage { get; set; }

        /// <summary>
        /// True if the process finish
        /// </summary>
        public bool Finished { get; set; }


        /// <summary>
        /// True if the cancel button is visible
        /// </summary>
        public bool IsCancelVisible { get; set; }


        /// <summary>
        /// True if the Ok button is visible
        /// </summary>
        public bool IsOkVisible { get; set; }


        /// <summary>
        /// True if the restart again button is visible
        /// </summary>
        public bool IsRestartVisible { get; set; }

        public List<SynchronizationItem> SynchronizationItems { get; set; } = new List<SynchronizationItem>();

        /// <summary>
        /// Closes the view
        /// </summary>
        public IMvxCommand OkCommand
        {
            get
            {
                return this.okCommand ?? (
                    this.okCommand = new MvxCommand(async () =>
                    {
                        await this.NavigationService.Navigate<MainViewModel>();                       
                    })
                );
            }
        }

        /// <summary>
        /// Starts the synchronization process
        /// </summary>
        public IMvxCommand StartSynchronizationCommand => this.startSynchronizationCommand ?? (
            this.startSynchronizationCommand = new MvxCommand(() =>
            {
                this.IsRestartVisible = false;
                RaisePropertyChanged(() => this.IsRestartVisible);
                StartSynchronization();
            }));


        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public SynchronizationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            SubscribeToSynchronizationMessages();
        }


        /// <summary>
        /// Prepare the list of elementos to synchronize
        /// </summary>
        public override void Prepare()
        {

            this.SynchronizationItems = new List<SynchronizationItem>(new[]
            {
                new SynchronizationItem(SyncElementsProgress.GettingProccess1Start, SyncElementsProgress.GettingProccess1End, GetText("GettingProccess1")),
                new SynchronizationItem(SyncElementsProgress.GettingProccess2Start, SyncElementsProgress.GettingProccess2End, GetText("GettingProccess2")),
                new SynchronizationItem(SyncElementsProgress.SendingProccess1Start, SyncElementsProgress.SendingProccess1End, GetText("SendingProccess1")),

             });
        }


        /// <summary>
        /// Initilizes
        /// </summary>
        /// <returns></returns>
        public override Task Initialize()
        {
            this.Progress = 0;
            this.ProgressMessage = "0%";
            this.StartSynchronizationCommand.Execute(null);
            RaisePropertyChanged(() => this.Progress);
            RaisePropertyChanged(() => this.ProgressMessage);
            return base.Initialize();
        }


        /// <summary>
        /// Starts the process when the page is visible
        /// </summary>
        public override async void ViewAppeared()
        {
            await Task.Delay(250);
            this.StartSynchronizationCommand.Execute(null);
        }


        /// <summary>
        /// Subscribes this ViewModel to listen for synchronization messages
        /// </summary>
        private void SubscribeToSynchronizationMessages()
        {
            this.synchronizationMessageSubscriptionToken = MessengerService.Subscribe<SynchronizationMessage>(message =>
            {
                // Updates the item status
                var item = SynchronizationItems.FirstOrDefault(x =>
                    x.SyncElementsProgressStart == message.SyncElement || x.SyncElementsProgressEnd == message.SyncElement);
                if (item != null)
                {
                    item.Status = message.ProgressStatus;
                }

                var progress = Convert.ToDouble(message.Progress);
                this.Progress = progress / 100;
                this.ProgressMessage = $"{message.Progress}%";
                this.SynchronizationMessage = message.Message;
                RaisePropertyChanged(() => this.SynchronizationMessage);
                RaisePropertyChanged(() => this.Progress);
                RaisePropertyChanged(() => this.ProgressMessage);
              
            });
        }

        /// <summary>
        /// Start the synchronization process
        /// </summary>
        public void StartSynchronization()
        {
            if (IsBusy) return;

            this.cancellationTokenSource = new CancellationTokenSource();

            this.IsBusy = true;
            this.Finished = false;
            this.IsOkVisible = false;
            RaisePropertyChanged(() => this.Finished);
            RaisePropertyChanged(() => this.IsOkVisible);

            if (this.canBeCanceled)
            {
                this.IsCancelVisible = true;
                RaisePropertyChanged(() => this.IsCancelVisible);
            }

            Task.Run(async () =>
            {
                try
                {

                    var result = await DataService.SynchronizeAsync(this.Version, cancellationTokenSource.Token);
                    if (result != null && result.Success)
                    {
                        this.Finished = true;
                        this.IsOkVisible = true;
                        this.IsCancelVisible = false;
                        await RaisePropertyChanged(() => this.Finished);
                        await RaisePropertyChanged(() => this.IsOkVisible);
                        await RaisePropertyChanged(() => this.IsCancelVisible);
                    }
                    else
                    {
                        //TODO: Show message error 
                        //InvokeOnMainThread(async () =>
                        //{


                        //    await this.NotificationService.NotifyAsync(GetText("Attention"), GetText("EmptyCycle"), GetText("Ok"), confirmed =>
                        //    {
                        //unlog user
                        //        DataService.DeleteLoggedUser();
                        //go to login page
                        //        NavigationService.Navigate<LoginViewModel>();
                        //    });
                        //});

                        //TODO: or thro exception
                        //throw new Exception();
                    }

                }
                catch
                {
                    InvokeOnMainThread(async () =>
                    {
                        this.IsRestartVisible = true;
                        this.Finished = true;
                        await RaisePropertyChanged(() => this.Finished);
                        await RaisePropertyChanged(() => this.IsRestartVisible);
                    });
                }
                finally
                {
                    IsBusy = false;
                }
            }, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Release the inner objects
        /// </summary>
        public void Release()
        {
            this.MessengerService.Unsubscribe<SynchronizationMessage>(synchronizationMessageSubscriptionToken);
            this.synchronizationMessageSubscriptionToken = null;
        }

    }
}

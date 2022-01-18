namespace MobileTemplate.Core.ViewModels
{
    using Interfaces;
    using MvvmCross;
    using MvvmCross.Localization;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.JsonLocalization;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using Services;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using Xamarin.Forms;


    /// <summary>
    /// Base class for all our ViewModels
    /// </summary>
    public abstract class BaseViewModel : MvxNavigationViewModel
    {
        private bool isBusy;
        private readonly IMvxTextProviderBuilder textProviderBuilder;
        private bool isTabletLandscape;

        /// <summary>
        /// Page title 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Current messenger service
        /// </summary>
        protected IMvxMessenger MessengerService { get; }

        /// <summary>
        /// The App Current Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Get's if the viewModel is busy doing something
        /// </summary>
        public bool IsBusy
        {
            get => this.isBusy;
            set
            {
                this.isBusy = value;
                RaisePropertyChanged(() => IsBusy);
                RaisePropertyChanged(() => IsEnabled);
            }
        }


        /// <summary>
        /// Gets if the ViewModel is enabled.
        /// It's the inverse of IsBusy for easier binding. (If IsBusy = true them IsEnabled = false)
        /// </summary>
        public bool IsEnabled
        {
            get => !this.isBusy;
            set
            {
                this.isBusy = !value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }

        /// <summary>
        /// Indicates if the ViewModel is executing on
        /// tablet device with landscape orientation
        /// </summary>
        public bool IsTabletLandscape
        {
            get => this.isTabletLandscape;
            set
            {
                this.isTabletLandscape = value;
                RaisePropertyChanged(() => IsTabletLandscape);
            }
        }

        /// <summary>
        /// Authentication service
        /// </summary>
        protected IDataService DataService { get; }


        /// <summary>
        /// Platform service
        /// </summary>
        protected IPlatformService PlatformService { get; }


        /// <summary>
        /// Notification service
        /// </summary>
        protected INotificationService NotificationService { get; }


        /// <summary>
        /// Authentication service
        /// </summary>
        protected IAuthenticationService AuthenticationService { get; }
        


        /// <summary>
        /// Source for localized texts
        /// </summary>
        public IMvxLanguageBinder TextSource =>
            new MvxLanguageBinder(TextProviderConstants.GeneralNamespace, TextProviderConstants.ClassName);


        /// <summary>
        /// Helper method for getting a localized text
        /// </summary>
        /// <param name="text">Text to get</param>
        /// <returns>Localized text</returns>
        public string GetText(string text)
        {
            return this.textProviderBuilder.TextProvider.GetText(
                TextProviderConstants.GeneralNamespace, TextProviderConstants.ClassName, text);
        }


        /// <summary>
        /// Converts a DateTime into a TimeSpan
        /// </summary>
        public static TimeSpan FromDate(DateTime date)
        {
            return new TimeSpan(date.Hour, date.Minute, date.Second);
        }


        /// <summary>
        /// Converts a TimeStan into a DateTime
        /// </summary>
        public static DateTime FromTimeSpan(TimeSpan timespan)
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, timespan.Hours,
                timespan.Minutes, timespan.Seconds);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logProvider"></param>
        /// <param name="navigationService"></param>
        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.isBusy = false;
            this.DataService = Mvx.IoCProvider.GetSingleton<IDataService>();
            this.textProviderBuilder = Mvx.IoCProvider.GetSingleton<IMvxTextProviderBuilder>();
            this.MessengerService = Mvx.IoCProvider.GetSingleton<IMvxMessenger>();
            this.PlatformService = Mvx.IoCProvider.GetSingleton<IPlatformService>();
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            this.Version = VersionTracking.CurrentVersion;
            this.NotificationService = Mvx.IoCProvider.GetSingleton<INotificationService>();
            this.AuthenticationService = Mvx.IoCProvider.GetSingleton<IAuthenticationService>();
        }


        /// <summary>
        /// Handler to DeviceDisplay.MainDisplayInfoChanged event
        /// that is triggered whenever any screen metrics changes
        /// </summary>
        private void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            //TODO: Test on Android Tablet Device
            //Xamarin.Essentials have a bug with emulators and rotation: [Bug] DisplayInfoChanged invoked with wrong orientation #1355
            var displayInfo = e.DisplayInfo;
            this.IsTabletLandscape = Device.Idiom == TargetIdiom.Tablet && displayInfo.Orientation == DisplayOrientation.Landscape;
            OnOrientationChangedAsync();
        }


        /// <summary>
        /// Called when the orientation changes
        /// </summary>
        /// <returns></returns>
        public virtual Task OnOrientationChangedAsync() { return Task.FromResult(true); }


        /// <summary>
        /// Returns if the devices is tablet and landscape
        /// </summary>
        protected void CheckIfTabletIsLandscape()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            this.IsTabletLandscape = Device.Idiom == TargetIdiom.Tablet && mainDisplayInfo.Orientation == DisplayOrientation.Landscape;
        }




        /// <summary>
        /// Show an error message and logs an exception message
        /// </summary>
        public async Task ShowErrorErrorMessageAsyc(Exception ex)
        {
            await InvokeOnMainThreadAsync(async () =>
            {
                await LogExceptionAsync(ex);
                await this.NotificationService.NotifyErrorAsync("Attention", ex.Message);
            });
        }


        /// <summary>
        /// Logs an exception message
        /// </summary>
        public Task LogExceptionAsync(Exception ex)
        {
            return Task.FromResult(true);
        }
    }


    /// <summary>
    /// Base ViewModel with parameters
    /// </summary>
    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter> where TParameter : class
    {
        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Called with the ViewModel's parameters
        /// </summary>
        public abstract void Prepare(TParameter parameter);
    }


    /// <summary>
    /// Base ViewModel with parameters and result
    /// </summary>
    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModel, IMvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Called with the ViewModel's parameters
        /// </summary>
        public abstract void Prepare(TParameter parameter);

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }
    }


    /// <summary>
    /// Base ViewModel with parameters and result
    /// </summary>
    public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
        where TResult : class
    {
        protected BaseViewModelResult(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }
    }
}

namespace MobileTemplate.Core.Services
{
    using MobileTemplate.Core.Interfaces;
    using MvvmCross;
    using MvvmCross.Plugin.JsonLocalization;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;


    /// <inheritdoc />
    public class NotificationService :  INotificationService
    {
        private readonly IMvxTextProviderBuilder textProviderBuilder;


        /// <summary>
        /// Initializes the service
        /// </summary>
        public NotificationService()
        {
            this.textProviderBuilder = Mvx.IoCProvider.GetSingleton<IMvxTextProviderBuilder>();
        }


        /// <inheritdoc />
        public async Task<bool> NotifyAsync(string title, string message, string buttonText = "")
        {
            if (Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert(GetText(title), GetText(message), GetText(buttonText));
            }
            return true;
        }


        /// <inheritdoc />
        public async Task<bool> NotifyErrorAsync(string title, string message)
        {
            if (Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert(GetText(title), message, GetText("Ok"));
            }
            return true;
        }


        /// <inheritdoc />
        public async Task<bool> NotifyAsync(string title, string message, string buttonText, Action<bool> callback)
        {
            if (Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert(GetText(title), GetText(message), GetText(buttonText));
                callback(true);
            }
            return true;
        }


        /// <inheritdoc />
        public async Task<bool> ConfirmAsync(string title, string message, Action<bool> callback)
        {
            if (Application.Current.MainPage != null)
            {
                var result = await Application.Current.MainPage.DisplayAlert(GetText(title), GetText(message), GetText("Ok"), GetText("Cancel"));
                callback(result);
            }
            return true;
        }


        /// <inheritdoc />
        public async Task<bool> ActionSheetAsync(string title, bool deleteButton, Action<string> callback, params string[] buttons)
        {
            if (Application.Current.MainPage != null)
            {
                string deleteVisible=null;
                if(deleteButton)
                {
                    deleteVisible = GetText("Delete");
                }

                var result = await Application.Current.MainPage.DisplayActionSheet(title, GetText("Cancel"), deleteVisible, buttons);
                callback(result);
            }
            return true;
        }


        /// <inheritdoc />
        public async Task<bool> DisplayPrompAsync(string title, string subTitle, Action<string> callback)
        {
            if (Xamarin.Forms.Application.Current.MainPage != null)
            {
                var result = await Application.Current.MainPage.DisplayPromptAsync (title, subTitle);
                callback(result);
            }
            return true;
        }



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
    }
}

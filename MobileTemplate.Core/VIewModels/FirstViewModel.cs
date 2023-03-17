namespace MobileTemplate.Core.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// Root empty ViewModel for the AppCompact Activity
    /// </summary>
    public class FirstViewModel : BaseViewModel
    {
        //private IMvxCommand messageCommand;

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public FirstViewModel(ILoggerFactory logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }


        ///// <summary>
        ///// Show notifications
        ///// </summary>
        //public IMvxCommand MessageCommand => messageCommand ?? (messageCommand = new MvxCommand(async () =>
        //{
        //    //notificacion simple
        //    await this.NotificationService.NotifyAsync("Attention", "ChangesSaved", "Ok");

        //    //notificacion con callback para cerrar la ventana o etc
        //    await this.NotificationService.NotifyAsync("Warning", "ChangesSaved", "GoBack", confirmed =>
        //    {

        //        // NavigationService.Close(this);
        //    });

        //    //notificacion con opcion de aceptar/cancelar
        //    await NotificationService.ConfirmAsync("Info", "Confirm", async confirmed =>
        //    {
        //        if (confirmed)
        //        {
        //            await this.NotificationService.NotifyAsync("Info", "SuccessMessage", "Ok");
        //        }
        //    });

        //    //notificacion con varias opciones de acciones
        //    await NotificationService.ActionSheetAsync("Info", false, async confirmed =>
        //   {
        //       if (confirmed == "Action 1")
        //       {
        //            //do something
        //        }
        //   }, "Action 1", "Action2");

        //    //notificacion con varias opciones de acciones y con delete
        //    await NotificationService.ActionSheetAsync("Titulo", true, action =>
        //  {
        //      if (action == "Action 1")
        //      {
        //            //do something
        //        }
        //      if (action == "Action 2")
        //      {
        //            //do something
        //        }

        //  }, "Action 1", "Action2");


        //    //display promp

        //    await NotificationService.DisplayPrompAsync("Titulo", "Ingresa algo", async confirmed =>
        //    {
        //        if (!string.IsNullOrEmpty(confirmed))
        //        {
        //            //do something
        //        }
        //    });


        //}));
    }
}

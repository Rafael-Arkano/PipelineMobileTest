namespace MobileTemplate.Core.Pages
{
    using MvvmCross.Forms.Presenters.Attributes;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Login UI
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = false, NoHistory = true)]
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
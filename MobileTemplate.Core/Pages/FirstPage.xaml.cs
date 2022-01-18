namespace MobileTemplate.Core.Pages
{
    using MvvmCross.Forms.Presenters.Attributes;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Main UI
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation]
    public partial class FirstPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public FirstPage()
        {
            InitializeComponent();
        }
    }
}
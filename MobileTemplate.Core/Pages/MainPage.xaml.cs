namespace MobileTemplate.Core.Pages
{
    using Xamarin.Forms.Xaml;
    using MvvmCross.Forms.Presenters.Attributes;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    /// <summary>
    /// Main UI
    /// This acts a host to the other views 
    /// The base calls can be changd to from MvxTabbedPage to MvxMasterDetailPage 
    /// and the attribute from MvxTabbedPagePresentation  to MvxMasterDetailPagePresentation
    /// in order to user navigation drawer instead of tabs
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Root, WrapInNavigationPage = true)]
    public partial class MainPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}
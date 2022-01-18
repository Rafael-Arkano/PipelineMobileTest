namespace MobileTemplate.Core.Pages
{
    using MvvmCross.Forms.Presenters.Attributes;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Login UI
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class ListItemsPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public ListItemsPage()
        {
            InitializeComponent();
        }
    }
}
namespace MobileTemplate.Core.Pages
{
    using MvvmCross.Forms.Presenters.Attributes;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Entry form UI
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation()]
    public partial class ItemPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public ItemPage()
        {
            InitializeComponent();
        }
    }
}
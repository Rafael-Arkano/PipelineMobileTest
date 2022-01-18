namespace MobileTemplate.Core.Pages
{
    using MvvmCross.Forms.Presenters.Attributes;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Entry form UI
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation()]
    public partial class EntryFormPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public EntryFormPage()
        {
            InitializeComponent();
        }
    }
}
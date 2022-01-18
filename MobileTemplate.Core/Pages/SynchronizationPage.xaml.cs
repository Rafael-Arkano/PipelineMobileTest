using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace MobileTemplate.Core.Pages
{
    /// <summary>
    /// Synchronization page UI
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = false, NoHistory = true)]
    public partial class SynchronizationPage
    {
        /// <summary>
        /// Initializes the page
        /// </summary>
        public SynchronizationPage()
        {
            InitializeComponent();
        }
    }
}
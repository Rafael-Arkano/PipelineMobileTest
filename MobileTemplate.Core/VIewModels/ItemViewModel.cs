namespace MobileTemplate.Core.ViewModels
{
    using MobileTemplate.Models;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;


    /// <summary>
    /// Logic to show info from an item selected
    /// </summary>
    public class ItemViewModel : BaseViewModel<Item>
    {
        /// <summary>
        /// Menu
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public ItemViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }


        /// <summary>
        /// Receives the item parameter
        /// </summary>
        public override void Prepare(Item item)
        {
            this.Item = item;
            this.Title = this.Item.Title;
        }

    }
}

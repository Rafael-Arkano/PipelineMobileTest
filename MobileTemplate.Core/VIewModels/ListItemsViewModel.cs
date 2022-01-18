namespace MobileTemplate.Core.ViewModels
{
    using MobileTemplate.Core.Helpers;
    using MobileTemplate.Core.Models;
    using MobileTemplate.Models;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;


    /// <summary>
    /// Sample ViewModel to show a list of items
    /// </summary>
    public class ListItemsViewModel : BaseViewModel
    {
        private string filterText;
        private IEnumerable<Item> allItems;


        /// <summary>
        /// Filter text
        /// </summary>
        public string FilterText 
        { 
            get => filterText; 
            set
            {
                SetProperty(ref filterText, value);
                MvxNotifyTask.Create(Search(filterText));
            }
        }


        /// <summary>
        /// Collection of items (filtered)
        /// </summary>
        public MvxObservableCollection<Item> Items { get; private set; }


        /// <summary>
        /// Selected contact point item
        /// </summary>
        public Item SelectedItem
        {
            get => null;
            set
            {
                // Goes to the item view
                RaisePropertyChanged(() => this.SelectedItem);
                this.NavigationService.Navigate<ItemViewModel, Item>(value);
            }
        }


        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public ListItemsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }


        /// <summary>
        /// Loads the item collection
        /// </summary>
        /// <returns></returns>
        public override Task Initialize()
        {
            MvxNotifyTask.Create(LoadItemsAsync);
            return base.Initialize();
        }


        /// <summary>
        /// Load all items
        /// </summary>
        /// <returns></returns>
        private async Task LoadItemsAsync()
        {
            this.IsBusy = true;
            try
            {
                this.allItems = new List<Item>(await this.DataService.LoadItemsAsync());
                this.Items = new MvxObservableCollection<Item>(this.allItems);
            }
            catch (Exception ex)
            {
                await ShowErrorErrorMessageAsyc(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        /// <summary>
        /// Search Clients by a given string
        /// </summary>
        /// <returns></returns>
        private async Task Search(string searchText)
        {
            if (this.IsBusy) return;
            try
            {
                IsBusy = true;
                if (this.allItems is null || !this.allItems.Any())
                {
                    return;
                }

                this.Items.Clear();
                this.Items.AddRange(this.allItems.Where(x => string.IsNullOrWhiteSpace(searchText) ||
                        (x.Title?.Contains(searchText) ?? false)));
            }
            catch (Exception ex)
            {
                await LogExceptionAsync(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        
    }
}

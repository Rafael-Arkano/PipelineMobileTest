using System.Windows.Input;
using System.ComponentModel;
using MvvmCross.Localization;

namespace MobileTemplate.Core.Models
{
    
    /// <summary>
    /// Encapsulates an item inside a list and provides
    /// it with additional commands
    /// </summary>
    public class ListItem<T> : INotifyPropertyChanged
    {
        private bool isSelected;
        private bool isEnabled;
        private bool isVisible;

        /// <summary>
        /// PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Item
        /// </summary>
        public T Item { get; set; }


        /// <summary>
        /// Assigns an Optional Color to the List Item
        /// </summary>
        public string HexColor { get; set; }


        /// <summary>
        /// Edit the item
        /// </summary>
        public ICommand SelectCommand { get; set; }


        /// <summary>
        /// Accepts / completed the item
        /// </summary>
        public ICommand OkCommand { get; set; }


        /// <summary>
        /// <summary>
        /// Indicates if an item is being currently selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                this.isSelected = value;
                OnPropertyChanged("IsSelected");
                OnPropertyChanged("IsSelectedInverted");
            }
        }

        /// <summary>
        /// Opposite to IsSelected
        /// </summary>
        public bool IsSelectedInverted => !IsSelected;


        /// <summary>
        /// Defines the source file for texts
        /// </summary>
        public IMvxLanguageBinder TextSource { get; private set; }


        /// <summary>
        /// Gets or sets a value indicating whether this
        /// is last item on a list.
        /// </summary>        
        public bool IsLastItem { get; set; }


        /// <summary>
        /// Indicates if an item is enabled
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                this.isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        /// <summary>
        /// Indicates if an item is visible
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                this.isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item"></param>
        public ListItem(T item)
        {
            this.Item = item;
        }


        /// <summary>
        /// Constructor with localized text binder
        /// </summary>
        /// <param name="item">Wrapped Item</param>
        /// <param name="textSource">Text Source for text localization purpose</param>
        public ListItem(T item, IMvxLanguageBinder textSource)
        {
            this.Item = item;
            this.TextSource = textSource;
        }


        /// <summary>
        /// Constructor with localized text binder and hex color property
        /// </summary>
        /// <param name="item">Wrapped Item</param>
        /// <param name="textSource">Text Source for text localization purpose</param>
        /// <param name="hexColorString">Hex Color String</param>
        public ListItem(T item, IMvxLanguageBinder textSource, string hexColor)
        {
            this.Item = item;
            this.TextSource = textSource;
            this.HexColor = hexColor;
        }


        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

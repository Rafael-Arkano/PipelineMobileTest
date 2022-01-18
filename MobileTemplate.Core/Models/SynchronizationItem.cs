using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MobileTemplate.Core.Models
{

    /// <summary>
    /// Synchronization state item
    /// </summary>
    public class SynchronizationItem : INotifyPropertyChanged
    {
        private SyncLoadingStatus status = SyncLoadingStatus.Default;


        /// <summary>
        /// Sync status
        /// </summary>
        public SyncLoadingStatus Status
        {
            get => this.status;
            set
            {
                this.status = value;
                OnPropertyChanged("Status");
            }
        }


        /// <summary>
        /// Start element
        /// </summary>
        public SyncElementsProgress SyncElementsProgressStart { get; set; }


        /// <summary>
        /// End elemtn
        /// </summary>
        public SyncElementsProgress SyncElementsProgressEnd { get; set; }


        /// <summary>
        /// Text to show
        /// </summary>
        public string Text { get; set; }


        /// <summary>
        /// Handles the propety changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;



        /// <summary>
        /// Constructor
        /// </summary>
        public SynchronizationItem()
        {

        }


        /// <summary>
        /// Constructor
        /// </summary>
        public SynchronizationItem(SyncElementsProgress SyncElementsProgressStart, SyncElementsProgress SyncElementsProgressEnd, string text)
        {
            this.Status = SyncLoadingStatus.Default;
            this.SyncElementsProgressStart = SyncElementsProgressStart;
            this.SyncElementsProgressEnd = SyncElementsProgressEnd;
            this.Text = text;
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

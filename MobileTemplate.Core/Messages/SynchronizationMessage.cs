namespace MobileTemplate.Core.Messages
{
    using MobileTemplate.Core.Models;
    using MvvmCross.Plugin.Messenger;
   
    /// <summary>
    /// Sync message
    /// </summary>
    public class SynchronizationMessage : MvxMessage
    {
        /// <summary>
        /// Synchronization operation
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Progress porcentage
        /// </summary>
        public int Progress { get; set; }


        /// <summary>
        /// Element being sync
        /// </summary>
        public SyncElementsProgress SyncElement { get; set; }


        /// <summary>
        /// Progress Status
        /// </summary>
        public SyncLoadingStatus ProgressStatus { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public SynchronizationMessage(object sender, SyncElementsProgress syncElement, SyncLoadingStatus progressStatus) : base(sender)
        {
            this.SyncElement = syncElement;
            this.Progress = (int)syncElement;
            this.Message = syncElement.ToString();
            this.ProgressStatus = progressStatus;
        }




    }
}

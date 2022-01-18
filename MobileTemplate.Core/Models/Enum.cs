namespace MobileTemplate.Core.Models
{
    

    /// <summary>
    /// Synchronization status types
    /// </summary>
    public enum SyncLoadingStatus
    {
        Default,
        Error,
        Loading,
        Loaded
    }

    /// <summary>
    /// Synchronization Progress 
    /// </summary>
    public enum SyncElementsProgress
    {
        StartingSync = 1,
        GettingProccess1Start = 20,
        GettingProccess1End = 40,
        GettingProccess2Start = 51,
        GettingProccess2End = 70,
        SendingProccess1Start = 81,
        SendingProccess1End = 99,
        SyncFinished = 100
    }

}

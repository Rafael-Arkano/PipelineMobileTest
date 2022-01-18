namespace MobileTemplate.Core.Services
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using MvvmCross;
    using Newtonsoft.Json;
    using SQLite;
    using MobileTemplate.Core.Models;
    using MobileTemplate.Core.Messages;
    using MvvmCross.Logging;
    using System.Threading;
    using MvvmCross.Plugin.Messenger;
    using System.IO;
    using System.Collections.Generic;
    using MobileTemplate.Models;


    /// <summary>
    /// Data access service
    /// </summary>
    public class DataService : IDataService
    {
        private readonly SQLiteConnection database;
        private IHttpService httpService;


        /// <summary>
        /// Initialization
        /// </summary>
        public DataService(IHttpService httpService)
        {
            this.httpService = httpService;
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Constants.DatabaseName);
            this.database = CreateDatabase(databasePath);
        }


        /// <summary>
        /// CreateDatabase
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection CreateDatabase(string filePath)
        {
            var databasePath = filePath + Constants.DatabaseName;
            var database = new SQLiteConnection(databasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
            return database;
        }


       
        ///<inheritdoc/>
        public bool MustSynchronize()
        {
            return true;
        }


        public async Task<SynchronizeResult> SynchronizeAsync(string version, CancellationToken token)
        {
            return await SynchronizeAsync(this.database, this, true, version, token);
        }


        /// <summary>
        /// Starts the synchronization process
        /// </summary>
        public static async Task<SynchronizeResult> SynchronizeAsync(
            SQLiteConnection database, object messageSender, bool verbose, string version, CancellationToken token)
        {
            var startTime = DateTime.Now;
            // var synchronizationData = GetSynchronizationData(database);
            //  var isFirstSynchronization = synchronizationData == null;

            var logger = Mvx.IoCProvider.GetSingleton<IMvxLog>();
            try
            {
                IMvxMessenger messenger = null;
                if (verbose)
                {
                    messenger = Mvx.IoCProvider.GetSingleton<IMvxMessenger>();
                }

                messenger?.Publish(new SynchronizationMessage(messageSender, SyncElementsProgress.StartingSync, SyncLoadingStatus.Default));

                async Task<bool> SynchronizeElement<T>(string elementName, string serviceUri, SyncElementsProgress syncElementsProgressStart, SyncElementsProgress syncElementsProgressEnd)
                {
                    var success = true;
                    try
                    {
                        startTime = DateTime.Now;
                        messenger?.Publish(new SynchronizationMessage(messageSender, syncElementsProgressStart, SyncLoadingStatus.Loading));
                        await Task.Delay(2000);
                    }
                    catch (Exception ex)
                    {
                        logger?.Error(ex, ex.Message);
                        success = false;
                    }
                    finally
                    {
                        messenger?.Publish(new SynchronizationMessage(messageSender, syncElementsProgressEnd, success ? SyncLoadingStatus.Loaded : SyncLoadingStatus.Error));
                    }
                    return success;
                }

                await SynchronizeElement<Models.SynchronizationItem>("Element1", "uri1",
                    SyncElementsProgress.GettingProccess1Start, SyncElementsProgress.GettingProccess1End);

                await SynchronizeElement<Models.SynchronizeResult>("Element2", "uri2",
                    SyncElementsProgress.GettingProccess2Start, SyncElementsProgress.GettingProccess2End);

                await SynchronizeElement<Models.MenuOption>("Element3", "uri3",
                    SyncElementsProgress.SendingProccess1Start, SyncElementsProgress.SendingProccess1End);

                await Task.Delay(2000);
                messenger?.Publish(new SynchronizationMessage(messageSender, SyncElementsProgress.SyncFinished, SyncLoadingStatus.Loaded));
            }
            catch (Exception ex)
            {
                logger?.Error(ex, ex.Message);
                throw new Exception(ex.Message);
            }

            return new SynchronizeResult()
            {
                Success = true,
            };
        }
       

        /// <summary>
        /// Returns a collection of items 
        /// </summary>
        /// <returns></returns>
        public Task<List<Item>> LoadItemsAsync()
        {
            var result = new List<Item>();
            for (int i = 0; i < 200; i++)
            {
                result.Add(new Item
                {
                    Title = "Lorem ipsum " + i.ToString(),
                    Description = "Lorem ipsum dolor sit amet, consectetue",
                    Image = "",
                    ItemId = i.ToString()
                });
            }           
            return Task.FromResult(result);
        }
    }
}
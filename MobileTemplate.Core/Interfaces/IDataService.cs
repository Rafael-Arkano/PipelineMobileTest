using MobileTemplate.Core.Models;
using MobileTemplate.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MobileTemplate.Core.Interfaces
{

    /// <summary>
    /// Data access service interface
    /// </summary>
    public interface IDataService
    {

        /// <summary>
        /// Checks if the synchronization is mandatory
        /// </summary>
        /// <returns></returns>
        bool MustSynchronize();


        /// <summary>
        /// Returns a collection of items
        /// </summary>
        /// <returns></returns>
        Task<List<Item>> LoadItemsAsync();


        /// <summary>
        /// Sync Async 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<SynchronizeResult> SynchronizeAsync( string version, CancellationToken token);

    }
}

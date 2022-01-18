using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MobileTemplate.Core.Interfaces
{

    /// <summary>
    /// Http access service interface
    /// </summary>
    public interface IHttpService
    {

        /// <summary>
        /// Executes a post method 
        /// </summary>
        Task<T> PostAsync<T>(object postData, string uri, JsonSerializerSettings settings = null);


        /// <summary>
        /// Executes a GET method to the service
        /// </summary>
        Task<T> GetAsync<T>(string uri, JsonSerializerSettings settings = null);

    }
}

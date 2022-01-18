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
    public class HttpService : IHttpService
    {
        private static HttpClient httpClient;


        /// <summary>
        /// Initialization
        /// </summary>
        public HttpService()
        {
        }

        
        /// <summary>
        /// Creates and configures the HTTP client
        /// </summary>
        private void CreateHttpClient()
        {
            // Disables the default cache
            if (httpClient != null)
            {
                httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
            }
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add(Constants.ApiKeyHeader, Constants.WebApiKey);
            httpClient.Timeout = new TimeSpan(0, 0, 5, 0);
        }

       

        /// <summary>
        /// Post data 
        /// </summary>
        public async Task<T> PostAsync<T>(object postData, string uri, JsonSerializerSettings settings = null)
        {
            CreateHttpClient();
            var data = JsonConvert.SerializeObject(postData);
            var contentPost = new StringContent(data, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(uri, contentPost);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Error in PostAsync: {result.StatusCode}");
            }
            var response = await result.Content.ReadAsStringAsync();
            var resultData = JsonConvert.DeserializeObject<T>(response, settings);
            return resultData;
        }


        /// <summary>
        /// Executes a GET method to the service
        /// </summary>
        public async Task<T> GetAsync<T>(string uri, JsonSerializerSettings settings = null)
        {

            var result = await httpClient.GetAsync(uri);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Error in GetAsync: {result.StatusCode}");
            }
            var response = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(response);
            return data;
        }
    }
}
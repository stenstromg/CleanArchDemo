﻿using Demo.App.Interfaces;

namespace Demo.App.Services
{
    public interface IWebApiSvcService
    {
        /// <summary>
        ///  Returns the data retreived from the service found at the appsettings path identified 
        ///  by the <paramref name="apiKey"/> argument. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiKey">
        ///  Found in the WebServiceURLs section of the appsettings.json
        /// </param>
        /// <returns></returns>
        Task<T?> GetData<T>(string apiKey) where T : class;

        /// <summary>
        /// Returns the results of a POST request.
        /// </summary>
        /// <typeparam name="RetType">Type of the return object</typeparam>
        /// <typeparam name="ArgType">Type of the <paramref name="postData"/> argument. </typeparam>
        /// <param name="apiKey"></param>
        /// <param name="postData">
        ///  The data to post to the URL identified by the <paramref name="apiKey"/> argument
        ///  </param>
        /// <returns></returns>
        Task<RetType?> PostData<RetType, ArgType>(string apiKey, ArgType postData) where RetType : class;
    }

    public class WebApiSvcService(IWebAPIService webAPIService) : IWebApiSvcService
    {
        #region properties

        IWebAPIService _service { get; set; } = webAPIService;

        #endregion properties

        #region ctor
        #endregion ctor

        #region public

        public async Task<T?> GetData<T>(string apiKey) where T : class
        {
            T? ret = await this._service.GetData<T>(apiKey);
            return ret;
        }

        public async Task<RetType?> PostData<RetType, ArgType>(string apiKey, ArgType postData) where RetType : class
        {
            RetType? ret = await _service.PostData<RetType, ArgType>(apiKey, postData);
            return ret;
        }

        #endregion private
    }
}

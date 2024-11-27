using Demo.App.Models.DTO;

namespace Demo.App.Interfaces.WebAPI
{
    public interface IWebAPIUtilities
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
        //Task<T?> GetData<T>(string apiKey) where T : class;
        Task<WebServiceResponse> Get<T>(string apiKey);

        /// <summary>
        /// Returns the UserLogin object associated with the <paramref name="password"/> and 
        /// <paramref name="username"/> arguments
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //Task<UserLogin?> LoginWithCredentials(string apiKey, string username, string password);

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
        //Task<WebServiceResponse> PostData<RetType, ArgType>(string apiKey, ArgType postData) where RetType : class;

        Task<WebServiceResponse> PostData2<RetType, ArgType>(string apiKey, ArgType postData) where RetType : class;
    }
}

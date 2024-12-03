using Demo.App.Models.DTO;

namespace Demo.App.Interfaces.WebAPI
{
    public interface IWebAPIUtilities
    {

        /// <summary>
        /// Executes an Http DELETE request against the <paramref name="apiURL"/> argument.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="apiURL">
        ///  The URL that the DELETE request is being posted to.
        /// </param>
        /// <returns></returns>
        Task<WebServiceResponse> Delete<TReturn>(string apiURL);

        /// <summary>
        ///  Returns the data retreived from the service found at the appsettings path identified 
        ///  by the <paramref name="apiURL"/> argument. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiURL">
        ///  Found in the WebServiceURLs section of the appsettings.json
        /// </param>
        /// <returns></returns>
        //Task<T?> GetData<T>(string apiKey) where T : class;
        Task<WebServiceResponse> Get<T>(string apiURL);

        /// <summary>
        /// Returns the results of a POST request.
        /// </summary>
        /// <typeparam name="RetType">Type of the return object</typeparam>
        /// <typeparam name="ArgType">Type of the <paramref name="postData"/> argument. </typeparam>
        /// <param name="apiURL"></param>
        /// <param name="postData">
        ///  The data to post to the URL identified by the <paramref name="apiURL"/> argument
        ///  </param>
        /// <returns></returns>
        Task<WebServiceResponse> Post<RetType, ArgType>(string apiURL, ArgType postData) where RetType : class;
    }
}

using Demo.App.Interfaces.WebAPI;
using Demo.App.Models.DTO;
using Demo.App.Services.WebAPI;
using Demo.Domain.Models;

namespace Demo.Infrastructure.Repositories.WebAPI
{

    /// <summary>
    ///  Provides members for retreiving Contact data using the WebApiService whcih connects to the 
    ///  PresentationAPI for data.
    /// </summary>
    /// <param name="apiUtils"> 
    ///  DI reference to the service which abstracts the access details of the WebAPI
    /// </param>
    public class ContactApiRepository(IWebAPIUtilities apiUtils) : WebServiceBase, IContactApiRepository
    {
        #region properties

        /// <summary>
        /// Provides access to the data store via POST/GET/DELETE/PUT HttpRequests.
        /// </summary>
        IWebAPIUtilities _apiUtils { get; set; } = apiUtils;

        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region private
        #endregion private

        #region public

        public async Task<Contact?> GetContactById(string serviceUrl, long contactId)
        {
            // Assemble the WebAPI URL to request the contact from
            //
            string webApiURL = $"{serviceUrl}/{contactId}";

            // Call the method to post the requst to the WebService
            //
            WebServiceResponse response = await _apiUtils.Get<Contact>(webApiURL);

            // Process the response.
            //
            Contact? ret = ProcessResponse<Contact>(response);

            // Return the contact 
            //
            return ret;
        }

        public async Task<ICollection<Contact>?> GetContactsForUserID(string serviceUrl, long userID)
        {
            // Assemble the WebAPI URL to request the contact from
            //
            string webApiURL = $"{serviceUrl}/{userID}";

            // Call the method to post the requst to the WebService
            //
            WebServiceResponse response = await _apiUtils.Get<IEnumerable<Contact>>(webApiURL);

            // Process the response.
            //
            ICollection<Contact>? ret = ProcessResponse<ICollection<Contact>>(response);

            // Return the contact 
            //
            return ret;
        }

        #endregion public
    }
}

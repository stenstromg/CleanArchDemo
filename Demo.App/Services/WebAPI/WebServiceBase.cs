using Demo.App.Enums;
using Demo.App.Exceptions;
using Demo.App.Models.DTO;
using Demo.Domain.Models;

namespace Demo.App.Services.WebAPI
{
    /// <summary>
    /// Base class for services using WebServiceAPI
    /// </summary>
    public class WebServiceBase
    {
        #region properties
        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region private
        #endregion private

        #region public

        /// <summary>
        /// Processes the <paramref name="response"/> argument and returns the result or an appropriate exception.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return value</typeparam>
        /// <param name="response">The web service response received from the web service</param>
        protected TReturn? ProcessResponse<TReturn>(WebServiceResponse response) where TReturn : class
        {
            TReturn? ret = null;

            if (response != null)
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        ret = (TReturn?)response.Payload;
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException();

                    case (System.Net.HttpStatusCode)CustomHttpStatusCodes.DuplicateUserName:
                        throw new DuplicateUserNameException();

                    case (System.Net.HttpStatusCode)CustomHttpStatusCodes.InvalidCredentials:
                        throw new InvalidCredentialsException("Username/Password not found");

                    default:
                        throw new ApplicationException($"Api Request failed : {response.StatusCode} : {response.Message}");
                }
            }

            return ret;
        }

        #endregion public
    }
}

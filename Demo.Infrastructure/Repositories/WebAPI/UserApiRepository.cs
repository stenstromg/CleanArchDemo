
using Demo.App.Interfaces.WebAPI;
using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.App.Services.WebAPI;
using Demo.Domain.Models;

namespace Demo.Infrastructure.Repositories.WebAPI
{
    public class UserApiRepository(IWebAPIUtilities apiUtils) : WebServiceBase, IUserApiRepository
    {
        #region properties

        IWebAPIUtilities _apiUtils { get; set; } = apiUtils;

        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region private
        #endregion private

        #region public

        public async Task<UserLogin?> Login(string serviceURL, CredentialsModel creds)
        {
            WebServiceResponse? response = await this._apiUtils.Post<UserLogin, CredentialsModel>(serviceURL, creds);
            UserLogin? userLogin = base.ProcessResponse<UserLogin>(response);

            return userLogin;
        }

        public async Task<Contact?> Register(string serviceURL, UserLoginRegistrationModel registrationData)
        {
            WebServiceResponse? response = await this._apiUtils.Post<Contact, UserLoginRegistrationModel>(serviceURL, registrationData);
            Contact? ret = base.ProcessResponse<Contact>(response);

            return ret;
        }

        #endregion public
    }
}

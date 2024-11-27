
using Demo.App.Exceptions;
using Demo.App.Models;
using Demo.Domain.Models;
using Demo.App.Interfaces.WebAPI;

namespace Demo.App.Services.WebAPI
{
    public interface IUserApiService
    {
        /// <summary>
        /// Returns a UserLosigninstance if the credential arguments are matched. Use this for logging 
        /// users into the system.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCredentialsException"></exception>
        Task<UserLogin?> Login(string serviceURL, CredentialsModel creds);

        /// <summary>
        /// Registers a new UserLogin, including associated Contact, Email, and Person objects. 
        /// Returns the resultiing Contact object.
        /// </summary>
        /// <param name="serviceURL"></param>
        /// <param name="registrationData"></param>
        /// <returns></returns>
        Task<Contact?> Register(string serviceURL, UserLoginRegistrationModel registrationData);
    }

    public class UserApiService(IUserApiRepository apiRepository) : IUserApiService
    {
        #region properties

        IUserApiRepository _repository { get; set; } = apiRepository;

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
            UserLogin? response = await this._repository.Login(serviceURL, creds);
            return response;
        }

        public async Task<Contact?> Register(string serviceURL, UserLoginRegistrationModel registrationData)
        {
            Contact? contact = await this._repository.Register(serviceURL, registrationData);
            return contact;
        }

        #endregion public
    }
}

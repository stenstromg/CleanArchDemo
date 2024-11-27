using Demo.App.Exceptions;
using Demo.App.Models;
using Demo.Domain.Models;

namespace Demo.App.Interfaces.WebAPI
{
    public interface IUserApiRepository
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


        Task<Contact?> Register(string serviceURL, UserLoginRegistrationModel registrationData);
    }
}

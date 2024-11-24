using Demo.App.Interfaces;
using Demo.Domain.Models;
using Demo.Domain.Security;
using System.Linq.Expressions;

namespace Demo.App.Services
{
    public interface IAuthenticatorService
    {
        UserLogin? Login(string username, string password);
    }

    public class AuthenticatorService(IUserLoginRepository repository) : IAuthenticatorService
    {
        #region properties

        IUserLoginRepository _repository { get; set; } = repository;

        #endregion properties

        #region public

        /// <summary>
        ///  Returns the UserLogin object for the credential arguments. If one is not found then 
        ///  the login is invalid.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserLogin? Login(string username, string password)
        {
            // Has the entered password. We will compare this hash with the user's PWD value in the
            // database.
            //
            string hashPWD = HashService.ComputeMD5Hash(password);

            // Create a filter to filter on the hashed password and username. There will be only one. 
            //
            List<Expression<Func<UserLogin, bool>>> filter = new List<Expression<Func<UserLogin, bool>>>();
            filter.Add(e => e.Password == hashPWD);
            filter.Add(e => string.Equals(e.Username, username, StringComparison.OrdinalIgnoreCase));

            // Run the query with the filter
            //
            List<UserLogin>? stage = _repository.GetUserLogins(filter);

            // Return the return value (or null if nothing was returned)
            //
            return stage?.FirstOrDefault();
        }

        #endregion public
    }
}

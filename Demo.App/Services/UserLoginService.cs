using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.Domain.Models;
using Demo.Domain.Security;
using System.Linq.Expressions;

namespace Demo.App.Services
{
    public interface IUserLoginService
    {

        /// <summary>
        /// Creates a new User record in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        UserLogin CreateUserLogin(UserLogin user);

        /// <summary>
        /// Peranently deletes the UserLogin record associated with the loginid defined in the 
        /// <paramref name="options"/>  argument and any realted records such as email, phone, etc.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>TRUE if record was successfully delted</returns>
        Boolean DeleteUserLogin(DeleteUserLoginOptions options);

        /// <summary>
        /// Returns a UserLosigninstance if the credential arguments are matched. Use this for logging 
        /// users into the system.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserLogin? GetUserLogin(string password, string username);

        /// <summary>
        /// Returns a single user that is associated with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserLogin? GetUserLoginByID(long loginId);

        /// <summary>
        /// Returns a filtered list of Users
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<UserLogin>? GetUserLogins(List<Expression<Func<UserLogin, bool>>>? filters = null);

    }

    public class UserLoginService(IUserLoginRepository userRepository) : IUserLoginService
    {
        #region properties

        AuthenticatorService _authService { get; set; } = new AuthenticatorService(userRepository);

        IUserLoginRepository _repository { get; set; } = userRepository;

        #endregion properties

        #region public

        public UserLogin CreateUserLogin(UserLogin user)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(user.Password);

            // Validate the user
            //
            // Unique Email and/or username

            // hash the user password
            //
            user.Password = HashService.ComputeMD5Hash(user.Password);

            return this._repository.CreateUserLogin(user);
        }

        public bool DeleteUserLogin(DeleteUserLoginOptions options)
        {
            bool ret = this._repository.DeleteUserLogin(options);
            return ret;
        }

        public UserLogin? GetUserLogin(string password, string username)
        {
            UserLogin? ret = this._repository.GetUserLogin(password, username);
            return ret;
        }

        public UserLogin? GetUserLoginByID(long loginId)
        {
            return this._repository?.GetUserLoginByID(loginId);
        }

        public List<UserLogin>? GetUserLogins(List<Expression<Func<UserLogin, bool>>>? filters = null)
        {
            return this._repository?.GetUserLogins(filters);
        }

        #endregion public
    }
}

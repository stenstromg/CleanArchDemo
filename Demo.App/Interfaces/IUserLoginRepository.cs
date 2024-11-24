using Demo.App.Models;
using Demo.Domain.Models;
using System.Linq.Expressions;

namespace Demo.App.Interfaces
{
    public interface IUserLoginRepository
    {
        /// <summary>
        /// Adds a new User record to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserLogin CreateUserLogin(UserLogin user);

        /// <summary>
        /// Peranently deletes the UserLogin record associated with the <paramref name="loginId"/> 
        /// argument and any realted records such as email, phone, etc.
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns>TRUE if record was successfully deleted</returns>
        bool DeleteUserLogin(DeleteUserLoginOptions options);

        /// <summary>
        /// Returns a UserLosigninstance if the credential arguments are matched. Use this for logging 
        /// users into the system.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        UserLogin? GetUserLogin(string password, string loginId);

        /// <summary>
        /// Returns a single user that is associated with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserLogin? GetUserLoginByID(long id);

        /// <summary>
        /// Returns a filtered list of Users
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<UserLogin>? GetUserLogins(List<Expression<Func<UserLogin, bool>>> filters);

        /// <summary>
        /// Returns a flag indicating whether the <paramref name="email"/> argument is unique in the 
        /// table. If it's not being used.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsUniqueEmail(string email);

        /// <summary>
        /// Returns a flag indicating whether the <paramref name="username"/> argument is unique in the 
        /// table. (Meaning it's not being used).
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool IsUniqueUsername(string username);
    }
}

using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.Domain.Models;
using Demo.Domain.Security;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.Infrastructure.Repositories
{
    public class UserLoginRepository : IUserLoginRepository
    {
        #region properties

        DemoDbContext? _db { get; set; }

        #endregion properties

        #region ctor

        public UserLoginRepository(DemoDbContext ctx)
        {
            this._db = ctx;
        }

        #endregion ctor

        #region public

        public UserLogin CreateUserLogin(UserLogin user)
        {
            if (this._db != null)
            {
                this._db?.UserLogins.Add(user);
                this._db?.SaveChanges();
            }
            return user;
        }

        public bool DeleteUserLogin(DeleteUserLoginOptions options)
        {
            bool success = false;

            UserLogin? toDelete = this.GetUserLoginByID(options.UserLoginId);

            if (toDelete != null && this._db != null)
            {
                // Delete Email if specified
                //
                if (options.DeleteEmail && toDelete.Email != null)
                {
                    this._db.Emails.Remove(toDelete.Email);
                }

                // Delete Person if specified
                //
                if (options.DeletePerson && toDelete.Person != null)
                {
                    this._db.People.Remove(toDelete.Person);
                }

                // Delete UserLogin record
                //
                this._db.UserLogins.Remove(toDelete);

                // Save 
                //
                this._db.SaveChanges(true);

                // Make sure that the records were indeed deleted
                //
                toDelete = this.GetUserLoginByID(options.UserLoginId);
                success = (toDelete == null);
            }

            return success;
        }

        public UserLogin? GetUserLogin(string password, string username)
        {
            // hash the password 
            //
            string hashPWD = HashService.ComputeMD5Hash(password);

            UserLogin? userLogin = this._db.UserLogins.Where(e => e.Password == hashPWD && e.Username.ToLower() == username.ToLower())
                                                      .Include(user => user.Person)
                                                      .Include(user => user.Email)
                                                      .FirstOrDefault();

            if (userLogin != null)
            {
                // Update the login
                //
                userLogin.LastLoginDate = DateTime.UtcNow;
                this._db.SaveChanges();
            }

            return userLogin;
        }

        public UserLogin? GetUserLoginByID(long loginId)
        {
            return this._db?.UserLogins.Where(e => e.ID == loginId).Include(user => user.Person).Include(user => user.Email).FirstOrDefault() ?? null;
        }

        public List<UserLogin>? GetUserLogins(List<Expression<Func<UserLogin, bool>>> filters)
        {
            var query = from user in this._db?.UserLogins
                        select new UserLogin
                        {
                            ID = user.ID,
                            CreatedBy = user.CreatedBy,
                            CreatedDate = user.CreatedDate,
                            Email = user.Email,
                            FailedLoginCount = user.FailedLoginCount,
                            IsEnabled = user.IsEnabled,
                            IsLocked = user.IsLocked,
                            LastLoginDate = user.LastLoginDate,
                            Password = user.Password,
                            PasswordMustBeChanged = user.PasswordMustBeChanged, 
                            Person = user.Person,
                            UpdatedBy = user.UpdatedBy,
                            UpdatedDate = user.UpdatedDate,
                            Username = user.Username
                        };

            if (query != null && filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            return query?.ToList();
        }

        public bool IsUniqueEmail(string email)
        {
            bool ret = (_db == null) ? false : _db.Emails.Where(e=>e.ToString().ToLower() == email.ToLower()).Any();
            return ret;
        }

        public bool IsUniqueUsername(string username)
        {
            bool ret = (_db == null) ? false : _db.UserLogins.Any(e => e.Username != null && e.Username.Equals(username));
            return ret;
        }

        #endregion properties
    }
}

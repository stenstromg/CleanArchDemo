using Demo.App.Exceptions;
using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.Domain.Models;
using Demo.Domain.Security;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;

namespace Demo.Infrastructure.Repositories
{
    public class UserLoginRepository(DemoDbContext ctx) : IUserLoginRepository
    {
        #region properties

        DemoDbContext? _db { get; set; } = ctx;

        #endregion properties

        #region ctor
        #endregion ctor

        #region public

        public Contact RegisterUser(UserLoginRegistrationModel model, string author = "AUTO")
        {
            DateTime timestamp = DateTime.UtcNow;
            Contact? contact   = null;

            var errMsg = model.Validate();

            if (errMsg != null && errMsg.Count() > 0)
            {
                throw new InvalidDataException(errMsg.FirstOrDefault());
            }

            if (this._db != null)
            {
                using (IDbContextTransaction transaction = this._db.Database.BeginTransaction())
                {
                    // Prepare the model contents to be saved
                    //
                    model.PrepareForSave();

                    try
                    {
                        // Check to make sure that the Username is unique
                        //                        
                        UserLogin? userLogin = this._db.UserLogins?.Where(e => e.Username.ToLower() == model.Username.ToLower()).FirstOrDefault();

                        if (userLogin != null)
                        {
                            throw new DuplicateNameException("The username is already in use");
                        }

                        // Save associated PhoneNumber in order to get an ID. 
                        //
                        if (model.PhoneNumber != null)
                        {
                            model.PhoneNumber.UpdatedBy = model.PhoneNumber.CreatedBy = author;
                            model.PhoneNumber.UpdatedDate = model.PhoneNumber.CreatedDate = timestamp;
                            this._db.PhoneNumbers.Add(model.PhoneNumber);
                        }

                        // Save associated Email in order to get an ID. 
                        //
                        if (model.Email != null)
                        {
                            model.Email.UpdatedBy = model.Email.CreatedBy = author;
                            model.Email.UpdatedDate = model.Email.CreatedDate = timestamp;
                            this._db.Emails.Add(model.Email);
                        }

                        // Save associated Person in order to get an ID. 
                        //
                        model.Person.CreatedBy = model.Person.UpdatedBy = author;
                        model.Person.CreatedDate = model.Person.UpdatedDate = timestamp;
                        this._db.People.Add(model.Person);

                        // Save associated UserLogin in order to get an ID. 
                        //
                        model.UserLogin.CreatedBy = model.UserLogin.UpdatedBy = author;
                        model.UserLogin.CreatedDate = model.UserLogin.UpdatedDate = timestamp;
                        model.UserLogin.Person = model.Person;
                        //model.UserLogin.Email = model.Email;
                        this._db.UserLogins.Add(model.UserLogin);

                        this._db.SaveChanges();

                        contact = new Contact()
                        {
                            DbAction             = Domain.Enums.EntityActions.Add,
                            PrimaryEmailID       = model.Email?.ID,
                            PrimaryPhoneNumberID = model.PhoneNumber?.ID,
                            Emails               = (model.Email == null) ? new List<Email>() : new List<Email>() { model.Email },
                            PhoneNumbers         = (model.PhoneNumber == null) ? new List<PhoneNumber>() : new List<PhoneNumber>() { model.PhoneNumber },
                            Person               = model.Person,
                            UserProfile          = model.UserLogin,
                            UserID               = model.UserLogin.ID,
                            CreatedBy            = author,
                            CreatedDate          = timestamp,
                            UpdatedDate          = timestamp,
                            UpdatedBy            = author,
                        };

                        this._db.Contacts.Add(contact);
                        this._db.SaveChanges();
                    }
                    catch (DuplicateNameException dupeX)
                    {
                        throw new DuplicateNameException(dupeX.Message);
                    }
                    catch (Exception ex)
                    {
                        while (ex.InnerException != null) ex = ex.InnerException;
                        throw new DbUpdateException($"The Contact for Login failed to save : {ex.Message}");
                    }

                    transaction.Commit();
                }
            }
            return contact;
        }

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
                //// Delete Email if specified
                ////
                //if (options.DeleteEmail && toDelete.Email != null)
                //{
                //    this._db.Emails.Remove(toDelete.Email);
                //}

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
                UserLogin? userLogin = this._db?.UserLogins.Where(e => e.Password == hashPWD && e.Username.ToLower() == username.ToLower())
                                                          .Include(user => user.Person)
                                                          .FirstOrDefault();
            if (userLogin != null)
            {
                userLogin.LastLoginDate = DateTime.UtcNow;
                this._db?.SaveChanges();
                return userLogin;
            }
            else
            {
                throw new InvalidCredentialsException("Username/Password not found");
            }

        }

        public UserLogin? GetUserLoginByID(long loginId)
        {
            //return this._db?.UserLogins.Where(e => e.ID == loginId).Include(user => user.Person).Include(user => user.Email).FirstOrDefault() ?? null;
            return this._db?.UserLogins.Where(e => e.ID == loginId).Include(user => user.Person).FirstOrDefault() ?? null;
        }

        public List<UserLogin>? GetUserLogins(List<Expression<Func<UserLogin, bool>>> filters)
        {
            var query = from user in this._db?.UserLogins
                        select new UserLogin
                        {
                            ID = user.ID,
                            CreatedBy = user.CreatedBy,
                            CreatedDate = user.CreatedDate,
                            //Email = user.Email,
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

using Demo.App.Utilities;
using Demo.Domain.Models;
using Demo.Domain.Security;

namespace Demo.App.Models
{
    /// <summary>
    /// Model used to transport UserLogin registration information. When a user creates a new login 
    /// on the registration page. 
    /// </summary>
    public class UserLoginRegistrationModel
    {
        #region properties

        public Email? Email { get; set; }

        public String? EmailAddress { get; set; }

        public PasswordLoginModel Password { get; set; } = new PasswordLoginModel();

        public Person Person { get; set; } = new Person();

        public String? Phone { get; set; }

        public PhoneNumber? PhoneNumber { get; set; }

        public UserLogin UserLogin { get; set; } = new UserLogin();

        public String? Username { get; set; }

        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region private
        #endregion private

        #region public

        public void PrepareForSave()
        {
            // Prepare the Email object
            //
            if (!StringUtilities.IsUndefined(this.EmailAddress))
            {
                if (Email.TryParse(this.EmailAddress, out Email email))
                {
                    this.Email = email;
                    this.Email.DbAction = Domain.Enums.EntityActions.Add;
                }
            }

            // Prepare the PhoneNumber object
            //
            if (!StringUtilities.IsUndefined(this.Phone))
            {
                if (PhoneNumber.TryParse(this.Phone, out PhoneNumber phone))
                {
                    this.PhoneNumber = phone;
                    this.PhoneNumber.DbAction = Domain.Enums.EntityActions.Add;
                }
            }

            // Prepare the UserLogin object
            //
            this.UserLogin = new UserLogin()
            {
                DbAction            = Domain.Enums.EntityActions.Add,
                FailedLoginCount    = 0,
                IsEnabled           = true,
                IsLocked            = false,
                Username            = this.Username,
                Password            = HashService.ComputeMD5Hash(this.Password.ToString()),
                PasswordMustBeChanged = false,
            };
        }

        public IEnumerable<string> Validate()
        {

            List<string> errors = new List<string>();
            //return errors;

            // Verify the data provided in the model.
            //
            // email is required
            //
            if (StringUtilities.IsUndefined(this.EmailAddress))
            {
                errors.Add("Email Address was not provided");
                //throw new ArgumentNullException(nameof(this.EmailAddress), "EmailAddress is required when creating a new contact");
            }

            // First Name/Last Name are required
            // 
            if (StringUtilities.IsUndefined(this.Person?.FirstName) || StringUtilities.IsUndefined(this.Person?.LastName))
            {
                errors.Add("First Name and Last Name are required");
                //throw new ArgumentNullException(nameof(this.Person), "First Name and Last Name are required");
            }

            // Username is required
            //
            if (StringUtilities.IsUndefined(this.Username))
            {
                errors.Add("Username is required");
                //throw new ArgumentNullException(nameof(this.Username), "Username is required");
            }

            // Password must pass validation
            //
            if (!this.Password.IsValid(out string? errMsg))
            {
                errors.Add(errMsg);
                //throw new ArgumentNullException(nameof(this.Password.NewPassword), errMsg);
            }

            return errors;

        }

        #endregion public
    }
}


using Demo.Domain.Security;

namespace Demo.App.Models
{
    /// <summary>
    /// Rerpresents a Password including hash and validation functions
    /// </summary>
    public class PasswordLoginModel
    {
        #region properties

        public string NewPassword { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;

        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region private

        /// <summary>
        /// Returns a flag indicating whether the password satisfies password format requirements.
        /// </summary>
        /// <returns></returns>
        private bool FormatIsValid()
        {
            if (this.NewPassword.Length < 8)
            {
                return false;
            }

            char[] specialCharacters = ['~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '|', '}', '{', '[', ']'];
            if(!(this.NewPassword.IndexOfAny(specialCharacters) >= 0))
            {
                return false;
            }

            char[] upperCase = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
            if(!(this.NewPassword.IndexOfAny(upperCase) >= 0))
            {
                return false;
            }

            char[] lowerCase = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
            if (!(this.NewPassword.IndexOfAny(lowerCase) >= 0))
            {
                return false;
            }

            char[] numbers = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];
            if (!(this.NewPassword.IndexOfAny(numbers) >= 0))
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// Returns flag indicating whether the New and Confirm password values matched.
        /// </summary>
        /// <returns></returns>
        private bool IsMatched()
        {
            return this.NewPassword.Equals(this.ConfirmPassword);
        }

        #endregion private

        #region public

        /// <summary>
        /// Returns the hashed password
        /// </summary>
        /// <returns></returns>
        public string Hash()
        {
            return HashService.ComputeMD5Hash(NewPassword);
        }

        /// <summary>
        /// Returnss a flag indicating that the Password is valid. 
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool IsValid(out string? errMsg)
        {
            if (!this.IsMatched())
            {
                errMsg = "Passwords must match";
                return false;
            }

            if (!this.FormatIsValid())
            {
                errMsg = "Password is invalid";
                return false;
            }

            errMsg = null;
            return true;
        }

        public override string ToString()
        {
            return this.NewPassword;
        }

        #endregion public
    }
}

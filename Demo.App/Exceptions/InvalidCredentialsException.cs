using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.App.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        #region properties
        #endregion properties

        #region data
        #endregion data

        #region ctor

        public InvalidCredentialsException() : base() { }

        public InvalidCredentialsException(string message) : base(message) { }

        #endregion ctor

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.App.Enums
{
    public enum CustomHttpStatusCodes
    {
        DuplicateUserName       = 601,
        InvalidCredentials      = 602,
        UserAccountLocked       = 603,
        UserAccountDisabled     = 604,
        LoginAttemptsExceeded   = 605,
    }
}

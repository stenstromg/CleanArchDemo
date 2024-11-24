using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.App.Models
{
    /// <summary>
    /// Data transfer model which can be passed to an API containing the values required to delete 
    /// a UserLogin
    /// </summary>
    public class DeleteUserLoginOptions
    {
        #region properties

        /// <summary>
        /// Gets/Sets the Id of the record to delete
        /// </summary>
        public long UserLoginId { get; set; }

        /// <summary>
        /// Gets/Sets a flag indicating whether the associated Email object should also be deleted.
        /// </summary>
        public bool DeleteEmail { get; set; } = true;

        /// <summary>
        /// Gets/Sets a fag indicating whether the associated person should also be deleted.
        /// </summary>
        public bool DeletePerson {get;set; } = true;

        #endregion properties

        #region ctor

        public DeleteUserLoginOptions()
        {
            
        }

        #endregion ctor
    }
}

using Demo.Domain.Models;

namespace Demo.WebApp.Classes.Services
{
    public interface IApplicationSessionService
    {
        UserLogin? ActiveUser { get; set; }
    }

    public class ApplicationSessionService : IApplicationSessionService
    {
        #region properties

        /// <summary>
        /// Gets/Sets the currently logged in user
        /// </summary>
        public UserLogin? ActiveUser { get; set; }

        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

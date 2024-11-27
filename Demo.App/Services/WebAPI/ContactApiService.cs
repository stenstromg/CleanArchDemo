
using Demo.App.Interfaces.WebAPI;
using Demo.Domain.Models;
using System.Runtime.CompilerServices;

namespace Demo.App.Services.WebAPI
{
    public interface IContactApiService
    {
        /// <summary>
        /// Returns the Contact object associated with the <paramref name="contactID"/> argument.
        /// </summary>
        /// <param name="serviceURL">
        ///     The URL to the WebService to service the request. Should not include the
        ///     Ex// https://domain.com/api/Contact
        /// </param>
        /// <param name="contactID"></param>
        /// <returns></returns>
        Task<Contact?> GetContactByID(string serviceURL, long contactID);

        /// <summary>
        /// Returns a collection of Contact objects associated with the <param name="userID"/> argument.
        /// </summary>
        /// <param name="serviceURL">
        ///     The URL to the WebService to service the request. Should not include the
        ///     Ex// https://domain.com/api/Contact
        /// </param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<ICollection<Contact>?> GetContactsForUserID(string serviceURL, long userID);
    }

    public class ContactApiService(IContactApiRepository apiRepository) : IContactApiService
    {
        #region properties

        IContactApiRepository _repository { get; set; } = apiRepository;

        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region private
        #endregion private

        #region public

        public async Task<Contact?> GetContactByID(string serviceURL, long contactID)
        {
            Contact? contact = await this._repository.GetContactById(serviceURL, contactID);
            return contact;
        }

        public async Task<ICollection<Contact>?> GetContactsForUserID(string serviceURL, long userID)
        {
            ICollection<Contact>? contacts = await this._repository.GetContactsForUserID(serviceURL, userID);
            return contacts;
        }

        #endregion public
    }
}

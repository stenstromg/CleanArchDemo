
using Demo.App.Interfaces.WebAPI;
using Demo.Domain.Models;
using System.Runtime.CompilerServices;

namespace Demo.App.Services.WebAPI
{
    public interface IContactApiService
    {
        /// <summary>
        ///  Returns a flag indicating whether the contact associated with the <paramref name="contactID"/> 
        ///  argument was successfully deleted.
        /// </summary>
        /// <param name="serviceURL">
        ///     The URL to the WebService to service the request.
        ///     Ex// https://domain.com/api/Contact
        /// </param>
        /// <param name="contactID"></param>
        /// <returns></returns>
        Task<bool?> DeleteContact(string serviceURL, long contactID);

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

        /// <summary>
        /// Saves the Contact record to the underlying datastore via the WebAPI. 
        /// </summary>
        /// <param name="serviceUrl">
        ///     The url and path of the service that will be used to retreive the data
        /// </param>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task<Contact> SaveContact(string serviceURL, Contact contact);
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

        public async Task<bool?> DeleteContact(string serviceURL, long contactID)
        {
            bool? ret = await _repository.DeleteContact(serviceURL, contactID);
            return ret;
        }

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

        public async Task<Contact> SaveContact(string serviceURL, Contact contact)
        {
            Contact? ret = await this._repository.SaveContact(serviceURL, contact);
            return ret;
        }

        #endregion public
    }
}

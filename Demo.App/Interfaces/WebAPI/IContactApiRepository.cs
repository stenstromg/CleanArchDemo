using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.App.Services;
using Demo.Domain.Models;

namespace Demo.App.Interfaces.WebAPI
{
    public interface IContactApiRepository
    {
        /// <summary>
        /// Returns the existing Contact associted with the <paramref name="contactId"/> argument.
        /// </summary>
        /// <param name="serviceUrl">
        ///     The url and path of the service that will be used to retreive the data
        /// </param>
        /// <param name="contactId">
        ///     The contact id of the contact to be retrieved.
        /// </param>
        /// <returns></returns>
        Task<Contact?> GetContactById(string serviceUrl, long contactId);

        /// <summary>
        /// Returns a list of Contacts associated with the <paramref name="userID"/> argument.
        /// </summary>
        /// <param name="serviceUrl">
        ///     The url and path of the service that will be used to retreive the data
        /// </param>
        /// <param name="userID">
        ///     The id of the User who "owns" tha contacts being retrieved.
        /// </param>
        /// <returns></returns>
        Task<ICollection<Contact>?> GetContactsForUserID(string serviceUrl, long userID);

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
}

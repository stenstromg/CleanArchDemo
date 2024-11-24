using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.App.Utilities;
using Demo.Domain.Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Demo.App.Services
{
    public interface IContactService
    {

        /// <summary>
        /// Permanently deletes the COntact record associated with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteContact(long contactId);

        /// <summary>
        /// Returns the existing Contact associted with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Contact? GetContactById(long contactId);

        /// <summary>
        /// Retusn a filtered list of Contacts
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IEnumerable<Contact>? GetContacts(List<Expression<Func<Contact, bool>>>? filters = null);

        /// <summary>
        ///  Creates a new Contact record, including any associated Person, Email, Phone records.
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="updatedBy">
        ///  Username of the user/process responsible for creating the new contact
        /// </param>
        /// <returns></returns>
        Contact Register(UserLoginRegistrationModel contact, string updatedBy);

        /// <summary>
        /// Saves an existing Contact,  including any associated Person, Email, Phone records
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Contact SaveContact(Contact model, String updatedBy);
    }

    public class ContactService(IContactRepository repository) : IContactService
    {
        #region properties

        IContactRepository _repository { get; set; } = repository;

        #endregion properties

        #region public

        //public Contact CreateContact(Contact contact, string updatedBy)
        //{
        //    ArgumentNullException.ThrowIfNull(contact);
        //    ArgumentNullException.ThrowIfNull(contact.Person);

        //    this._repository.CreateContactForLogin(contact);
        //    return contact;
        //}

        public Contact Register(UserLoginRegistrationModel model, String author)
        {
            Contact contact = this._repository.CreateContactForLogin(model, author);
            return contact;
        }

        public bool DeleteContact(long contactId)
        {
            return this._repository.DeleteContact(contactId);
        }

        public Contact? GetContactById(long contactId)
        {
            Contact? ret = this._repository.GetContactByID(contactId);
            return ret;
        }

        public IEnumerable<Contact>? GetContacts(List<Expression<Func<Contact, bool>>>? filters = null)
        {
            return this._repository.GetContacts(filters);
        }

        public Contact SaveContact(Contact contact, string updatedBy)
        {
            Contact ret = this.SaveContact(contact, updatedBy);
            return ret;

        }

        #endregion public

        #region private
        #endregion private
    }
}

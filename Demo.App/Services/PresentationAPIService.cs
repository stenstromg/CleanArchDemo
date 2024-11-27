using Demo.App.Interfaces.WebAPI;
using Demo.App.Models.DTO;
using Demo.Domain.Models;

namespace Demo.App.Services
{
    public interface IPresentationAPIService
    {
        //    /// <summary>
        //    /// Creaes a new Contact record, including any associated Person, Email, Phone or other 
        //    /// navigation properties associated with the contact.
        //    /// </summary>
        //    /// <param name="contact"></param>
        //    /// <returns></returns>
        //    Task<Contact?> CreateContact(Contact contact);

        //    /// <summary>
        //    /// Returns the contact object associated with the <paramref name="contactId"/> argument
        //    /// </summary>
        //    /// <param name="contactId"></param>
        //    /// <returns></returns>
        //    Contact? GetContactByID(long contactId);

        //    /// <summary>
        //    /// Returns a list of contacts
        //    /// </summary>
        //    /// <returns></returns>
        //    IEnumerable<Contact>? GetContacts();
        //}

        ///// <summary>
        ///// Provides access to the WebAPI functions of the PresentationAPI WebAPI services
        ///// </summary>
        //public class PresentationAPIService(IWebAPIUtilities apiService) : IPresentationAPIService
        //{
        //    #region properties

        //    IWebAPIUtilities _service { get; set; } = apiService;

        //    string _apiKey { get; set; } = "PresentationAPI";

        //    #endregion properties

        //    #region ctor
        //    #endregion ctor

        //    #region public

        //    public async Task<Contact?> CreateContact(Contact contact)
        //    {
        //        string apiKey = $"{_apiKey}:CreateContact";
        //        Contact? ret = await this._service.PostData2<Contact, Contact>(apiKey, contact);
        //        return ret;
        //    }

        //    public Contact? GetContactByID(long contactId)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public IEnumerable<Contact>? GetContacts()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    #endregion public

    }
}

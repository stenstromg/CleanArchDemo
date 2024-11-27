using Demo.App.Services.WebAPI;
using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Pages
{
    public partial class Contacts : UnqPageBase
    {
        #region properties

        /// <summary>
        /// Gets/Sets the contact that is being displayed in the editor.
        /// </summary>
        Contact? SelectedContact { get; set; }

        bool ContactEditorIsVisible { get; set; } = false;

        #endregion properties

        #region parameters
        #endregion parameters

        #region properties

        [Inject]
        IContactApiService? ContactApiService { get; set; }

        #endregion properties

        #region data

        /// <summary>
        /// Returns the Contact record associated with the <paramref name="contactID"/> argument
        /// </summary>
        /// <param name="contactID"></param>
        /// <returns></returns>
        async Task<Contact?> LookupContact(long contactID)
        {
            Contact? ret = null;

            if (base.AppService != null && this.ContactApiService != null)
            {
                string? serviceURL = base.AppService.GetWebServiceFunctionURL("GetContactByID");
                ret = await this.ContactApiService.GetContactByID(serviceURL, contactID);
            }

            return ret;
        }

        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers

        async void ContactList_OnItemSelect(long contactID)
        {
            this.SelectedContact = await this.LookupContact(contactID);
            this.ContactEditorIsVisible  = true;
            base.StateHasChanged();
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

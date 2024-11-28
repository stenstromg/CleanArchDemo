using Demo.App.Services.WebAPI;
using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Demo.WebApp.Components.Modules.Contacts;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApp.Components.Pages
{
    public partial class Contacts : UnqPageBase
    {
        #region properties

        ContactListComponent? ContactList {get; set;}

        bool ContactEditorIsVisible { get; set; } = false;

        /// <summary>
        /// Gets/Sets the contact that is being displayed in the editor.
        /// </summary>
        Contact? SelectedContact { get; set; }

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

        protected override void OnInitialized()
        {
            base.Layout.ContactMenu.OnAddNewClick= new EventCallback(this, (Action)MenuBtnAdd_OnClick);

            base.OnInitialized();
        }

        #endregion lifecycle

        #region event handlers

        async void ContectEditor_OnSave(Contact contact)
        {
            await this.ContactList.Refresh();
            base.StateHasChanged();
        }

        async void ContactList_OnItemSelect(long contactID)
        {
            this.SelectedContact = await this.LookupContact(contactID);
            this.ContactEditorIsVisible  = true;
            base.StateHasChanged();
        }

        void MenuBtnAdd_OnClick()
        {
            this.SelectedContact = this.InitializeNewContact();
            this.ContactEditorIsVisible = true;
            base.StateHasChanged();
        }

        #endregion event handlers

        #region private

        Contact InitializeNewContact()
        {
            Contact ret = new Contact() 
            { 
                DbAction = Domain.Enums.EntityActions.Add,
                UserID = base.SessionService.ActiveUser.ID,
                Emails = new HashSet<Email>(),
                Person = new Person(),
                PhoneNumbers = new HashSet<PhoneNumber>(),
            };

            return ret;
        }

        #endregion private

        #region public
        #endregion public
    }
}

using Azure;
using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.App.Services.WebAPI;
using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Demo.WebApp.Components.Modules.Contacts
{
    public partial class ContactListComponent : UnqComponentBase
    {
        #region properties

        IEnumerable<Contact>? ContactList { get; set; } = null;

        string? ErrorMessage { get; set; } = null;

        bool ShowErrorMessage { get; set; } = false;

        Contact? SelectedContact { get; set; }

        #endregion properties

        #region parameters

        [Parameter]
        public EventCallback<long> OnItemClick { get; set; }

        #endregion parameters

        #region inject

        [Inject]
        IContactApiService? ContactApiService { get; set; }

        [Inject]
        DialogService DialogSvc { get; set; }

        #endregion inject

        #region data

        /// <summary>
        ///  Gets the Contacts associated with the <paramref name="userID"/> argument. 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        async Task<ICollection<Contact>?> GetContactsForUserID(long userID)
        {
            if (ContactApiService != null)
            {
                string servicePath = base.AppConfigService.GetWebServiceFunctionURL("GetContactsForUserID");
                ICollection<Contact>?  ret = await this.ContactApiService.GetContactsForUserID(servicePath, userID);

                return ret;
            }
            else
            {
                throw new NullReferenceException("ApiRepositoryService not available.");
            }
        }

        /// <summary>
        /// Deletes the contact associaated with the <paramref name="contactID"/> argument.
        /// </summary>
        /// <param name="contactID"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        async Task<bool?> DeleteContact(long contactID)
        {
            if (this.ContactApiService != null)
            {
                string servicePath = base.AppConfigService.GetWebServiceFunctionURL("DeleteContact");
                bool? ret = await this.ContactApiService.DeleteContact(servicePath, contactID);

                return ret;
            }
            else
            {
                throw new NullReferenceException("ApiRepositoryService not available.");
            }
        }

        #endregion data

        #region lifecycle

        protected override async Task OnInitializedAsync()
        {
            await this.RenderContactList();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        #endregion lifecycle

        #region event handlers

        async void ListItem_OnClick(Contact contact)
        {
            this.SelectedContact = contact;

            if (OnItemClick.HasDelegate)
            {
                await OnItemClick.InvokeAsync(contact.ID);
            }
        }

        async void ListItem_OnDelete(Contact contact)
        {
            ConfirmOptions confirmOptions = new ConfirmOptions() { OkButtonText = "OK", CancelButtonText = "No" };

            bool? doContinue = await DialogSvc.Confirm("This will permanently delete the Contact. DO you want to continue", "Confirm", confirmOptions);
            if (doContinue.HasValue && doContinue.Value)
            {
                bool? success = await this.DeleteContact(contact.ID);
                if (success.HasValue && success.Value)
                {
                    await this.Refresh();
                }
                else
                {
                    AlertOptions alertOptions = new AlertOptions() { OkButtonText = "OK" };
                    await DialogSvc.Alert("Contact was not deleted.", "Delete Failed", alertOptions);
                }
            }
        }

        #endregion event handlers

        #region private

        string GetListItemCSS(Contact contact)
        {
            string ret = "unq-contact-list-item";

            if (this.SelectedContact != null && this.SelectedContact.ID == contact.ID)
            {
                ret = $"{ret} unq-selected";
            }

            return ret;
        }

        async Task RenderContactList()
        {
            if (base.AppSession != null && base.AppSession.ActiveUser != null)
            {
                try
                {
                    this.ContactList = await this.GetContactsForUserID(base.AppSession.ActiveUser.ID);
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null) ex = ex.InnerException;
                    this.ErrorMessage = ex.Message;
                    this.ShowErrorMessage = true;
                }
            }
        }

        #endregion private

        #region public

        public async Task Refresh()
        {
            await this.RenderContactList();
            base.StateHasChanged();
        }

        #endregion public
    }
}

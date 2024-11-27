﻿using Azure;
using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.App.Services.WebAPI;
using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Modules.Contacts
{
    public partial class ContactListComponent : UnqComponentBase
    {
        #region properties

        IEnumerable<Contact>? ContactList { get; set; } = null;

        string? ErrorMessage { get; set; } = null;

        bool ShowErrorMessage { get; set; } = false;

        #endregion properties

        #region parameters

        [Parameter]
        public EventCallback<long> OnItemClick { get; set; }

        #endregion parameters

        #region properties

        [Inject]
        IContactApiService? ContactApiService { get; set; }

        #endregion properties


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

        #endregion data

        #region lifecycle

        protected override async Task OnInitializedAsync()
        {
            if (base.AppSession != null && base.AppSession.ActiveUser != null)
            {
                try
                {
                    this.ContactList = await this.GetContactsForUserID(base.AppSession.ActiveUser.ID);
                }
                catch (Exception ex)
                {
                    while(ex.InnerException != null) ex = ex.InnerException;
                    this.ErrorMessage = ex.Message;
                    this.ShowErrorMessage = true;
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        #endregion lifecycle

        #region event handlers

        async void ListItem_OnClick(long contactID)
        {
            if (OnItemClick.HasDelegate)
            {
                await OnItemClick.InvokeAsync(contactID);
            }
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}
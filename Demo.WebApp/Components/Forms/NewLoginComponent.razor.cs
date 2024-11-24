using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Demo.WebApp.Classes.Utilities;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Demo.WebApp.Components.Forms
{
    public partial class NewLoginComponent : UnqComponentBase
    {
        #region properties

        UserLoginRegistrationModel FormData { get; set; } = new UserLoginRegistrationModel();

        #endregion properties

        #region parameters

        [Parameter]
        public EventCallback OnCancelClick { get; set; }

        [Parameter]
        public EventCallback<UserLoginRegistrationModel> OnRegisterClick { get; set; }

        #endregion parameters

        #region inject

        [Inject]
        public IWebAPIService? ApiService { get; set; }

        #endregion inject

        #region data

        /// <summary>
        /// Calls WebAPI Service to Create a new Contact/UserLogin
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        async Task<Contact?> Save(UserLoginRegistrationModel formData)
        {
            if (this.ApiService != null)
            {
                return await this.ApiService.PostData<Contact, UserLoginRegistrationModel>("PresentationAPI:Register", formData);
            }
            else
            {
                throw new NullReferenceException("CreateContact key for PresentationAPI node was not found in appsettings.");
            }
        }

        #endregion data

        #region lifecycle

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        #endregion lifecycle

        #region event handlers

        void btnCancel_OnClick()
        {
            this.OnCancelClick.InvokeAsync();
        }

        void form_OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
            // fail the form
        }

        async void form_OnSubmit()
        {
            if (this.OnRegisterClick.HasDelegate)
            {
                //ContactModelUtilities.PrepareContactForSave(this.FormData);
                await this.OnRegisterClick.InvokeAsync(this.FormData);
            }
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

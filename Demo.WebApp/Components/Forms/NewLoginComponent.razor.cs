using Demo.App.Interfaces.WebAPI;
using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.App.Services.WebAPI;
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

        string? ErrorMessage { get; set; } = null;

        UserLoginRegistrationModel FormData { get; set; } = new UserLoginRegistrationModel();

        bool ShowErrorMessage { get; set; } = false;

        #endregion properties

        #region parameters

        [Parameter]
        public EventCallback OnCancelClick { get; set; }

        [Parameter]
        public EventCallback<Contact> OnRegisterSuccess { get; set; }

        #endregion parameters

        #region inject

        [Inject]
        public IUserApiService? UserApiService { get; set; }

        #endregion inject

        #region data

        /// <summary>
        /// Calls WebAPI Service to Create a new Contact/UserLogin
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        async Task<Contact?> Register(UserLoginRegistrationModel formData)
        {
            // Validate the form data
            //
            List<string> validationErrors = formData.Validate().ToList();
            if (validationErrors.Count > 0)
            {
                this.ErrorMessage = validationErrors.First();
                this.ShowErrorMessage = true;
                return null;
            }

            // If valid, then use the APIService to Register the data
            //
            if (this.UserApiService != null && base.AppConfigService != null)
            {
                string? serviceURL = base.AppConfigService.GetWebServiceFunctionURL("Register");

                Contact? ret = await this.UserApiService.Register(serviceURL, formData);
                return ret;
            }
            else
            {
                throw new NullReferenceException("WebAPI Service not available.");
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
            Contact? contact = await this.Register(this.FormData);

            if (contact != null)
            {
                await this.OnRegisterSuccess.InvokeAsync(contact);
            }
            else
            {
                base.StateHasChanged();
            }
        }

        void txBox_OnChange()
        {
            this.ErrorMessage = null;
            this.ShowErrorMessage = false;
            base.StateHasChanged();
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

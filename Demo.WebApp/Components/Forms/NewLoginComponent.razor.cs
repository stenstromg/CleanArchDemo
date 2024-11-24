using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.App.Models.DTO;
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
        public IWebAPIRepository? ApiService { get; set; }

        #endregion inject

        #region data

        /// <summary>
        /// Calls WebAPI Service to Create a new Contact/UserLogin
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        async Task<Contact?> RegisterNewUser(UserLoginRegistrationModel formData)
        {
            // Prepare and validate the form data
            //
            List<string> validationErrors = formData.Validate().ToList();
            if (validationErrors.Count > 0)
            {
                this.ErrorMessage = validationErrors.First();
                this.ShowErrorMessage = true;
                return null;
            }

            // If the form passed validation, prepare the form contents to be saved.
            //
            //formData.PrepareForSave();

            if (this.ApiRepositorySvc != null)
            {
                //Contact? retValue = await this.ApiRepositorySvc.PostData<Contact, UserLoginRegistrationModel>("PresentationAPI:Register", formData);
                //return retValue;
             
                WebServiceResponse? response = await this.ApiRepositorySvc.PostData2<Contact, UserLoginRegistrationModel>("PresentationAPI:Register", formData);

                Contact? ret = null;

                if (response != null)
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            ret = (Contact)response.Payload;
                            break;
                        default:
                            this.ErrorMessage = $"Api Request failed : {response.StatusCode}";
                            this.ShowErrorMessage = true;
                            break;
                    }
                }

                return ret;
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
            Contact? contact = await this.RegisterNewUser(this.FormData);
            if (contact != null)
            {
                await this.OnRegisterSuccess.InvokeAsync(contact);
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

using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.App.Services;
using Demo.App.Utilities;
using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor.Rendering;

namespace Demo.WebApp.Components.Forms
{
    public partial class LoginComponent : UnqComponentBase
    {
        #region properties

        string? ErrorMessage { get; set; } = null;

        public string? Password { get; set; }

        public bool ShowErrorMessage { get; set; } = false;

        public string? Username { get; set; }

        #endregion properties

        #region parameters

        [Parameter]
        public EventCallback<UserLogin> OnLoginSuccess { get; set; }

        [Parameter]
        public EventCallback OnRegisterClick { get; set; }

        #endregion parameters

        #region data

        /// <summary>
        /// Attempts to retreive a userlofin object associated with the <paramref name="password"/> 
        /// and <paramref name="username"/> arguments.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        async Task<UserLogin?> PostLoginRequest(string username, string password)
        {
            if (this.ApiRepositorySvc != null)
            {
                CredentialsModel credentialsModel = new CredentialsModel() { Username = username, Password = password };
                //UserLogin? userLogin = await this.ApiRepositorySvc.PostData<UserLogin, CredentialsModel>("PresentationAPI:Login", credentialsModel);
                WebServiceResponse? response = await this.ApiRepositorySvc.PostData2<UserLogin, CredentialsModel>("PresentationAPI:Login", credentialsModel);

                UserLogin? userLogin  = null;

                if (response != null)
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            userLogin = (UserLogin)response.Payload;
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                            this.ErrorMessage = "Invalid Username/Password";
                            this.ShowErrorMessage = true;
                            break;
                    }
                }

                return userLogin;
            }
            else
            {
                throw new NullReferenceException("CreateContact key for PresentationAPI node was not found in appsettings.");
            }
        }


        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers

        async void btnLogin_OnClick()
        {
            if (!StringUtilities.IsUndefined(this.Username) && !StringUtilities.IsUndefined(this.Password))
            {
                UserLogin? userLogin = await this.PostLoginRequest(this.Username, this.Password);

                if (userLogin != null)
                {
                    await OnLoginSuccess.InvokeAsync(userLogin);
                }
                else
                {
                    base.StateHasChanged();
                }
            }
            else
            {
                this.ErrorMessage = "Username and Password must be provided";
                this.ShowErrorMessage = true;
            }
        }

        void btnRegister_OnClick()
        {
            OnRegisterClick.InvokeAsync();
        }

        void txPassword_OnChange()
        {
            this.ShowErrorMessage = false;
            base.StateHasChanged();
        }

        void txUserID_OnChange()
        {
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

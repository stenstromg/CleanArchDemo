using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Demo.WebApp.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.SqlServer.Server;

namespace Demo.WebApp.Components.Pages
{
    public partial class Login : UnqPageBase
    {
        #region properties

        string PageName = "Login";

        bool ShowNewLoginForm { get; set; } = false;

        #endregion properties

        #region parameters
        #endregion parameters

        #region inject
        #endregion inject

        #region data

        async Task<UserLogin?> PostLoginRequest(string username, string password)
        {
            if (this.ApiService != null)
            {
                CredentialsModel credentialsModel = new CredentialsModel() {Username = username, Password = password};
                UserLogin? result = await this.ApiService.PostData<UserLogin, CredentialsModel>("PresentationAPI:Login", credentialsModel);

                return result;
            }
            else
            {
                throw new NullReferenceException("CreateContact key for PresentationAPI node was not found in appsettings.");
            }
        }

        /// <summary>
        /// Processes the request to register a new user and contact (every user must have a contact).
        /// </summary>
        /// <param name="registrationData"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        async Task<Contact?> RegisterNewUser(UserLoginRegistrationModel registrationData)
        {
            if (this.ApiService != null)
            {
                return await this.ApiService.PostData<Contact, UserLoginRegistrationModel>("PresentationAPI:Register", registrationData);
            }
            else
            {
                throw new NullReferenceException("CreateContact key for PresentationAPI node was not found in appsettings.");
            }
        }

        #endregion data

        #region lifecycle

        /// <summary>
        /// Override the baseclass implementation. 
        /// </summary>
        protected override void OnInitialized()
        {
        }

        #endregion lifecycle

        #region event handlers

        async void LoginComponent_OnLogin(LoginComponent source)
        {
            string? username = source.Username;
            string? password = source.Password;  

            UserLogin? userLogin = await this.PostLoginRequest(username, password);

            if (userLogin != null)
            {
                this.SessionService.ActiveUser = userLogin;
                this.NavManager.NavigateTo("/");
            }
            else
            {
                // handle error
            }

        }

        void LoginComponent_OnRegister()
        {
            this.ShowNewLoginForm = true;
            StateHasChanged();
        }

        void NewLoginCoponent_OnCancel()
        {
            this.ShowNewLoginForm = false;
            StateHasChanged();
        }

        async void NewLoginCoponent_OnRegister(UserLoginRegistrationModel registrationData)
        {
            try
            {
                await this.RegisterNewUser(registrationData);
                this.ShowNewLoginForm = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

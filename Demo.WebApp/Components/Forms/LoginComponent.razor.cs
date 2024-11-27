using Demo.App.Exceptions;
using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.App.Services;
using Demo.App.Services.WebAPI;
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


        #region inject

        [Inject]
        public IUserApiService? UserApiService { get; set; }

        #endregion inject

        #region data

        /// <summary>
        /// Attempts to retreive a userlofin object associated with the <paramref name="password"/> 
        /// and <paramref name="username"/> arguments.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        async Task<UserLogin?> Login(string username, string password)
        {
            // populate the crdentials object
            //
            CredentialsModel credentialsModel = new CredentialsModel() { Password = password, Username = username };

            // Log the user in.
            //
            if (this.UserApiService != null)
            {
                string? serviceURL = base.AppConfigService?.GetWebServiceFunctionURL("Login");
                UserLogin? user = await this.UserApiService.Login(serviceURL, credentialsModel);
                return user;
            }
            else
            {
                throw new NullReferenceException("WebAPI Service not available.");
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
                try
                {
                    UserLogin? userLogin = await this.Login(this.Username, this.Password);
                    await OnLoginSuccess.InvokeAsync(userLogin);

                }
                catch (InvalidCredentialsException credX)
                {
                    this.ErrorMessage = credX.Message;
                    this.ShowErrorMessage = true;
                }
                finally
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

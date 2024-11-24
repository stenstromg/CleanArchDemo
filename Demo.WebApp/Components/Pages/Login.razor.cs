using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.App.Models.DTO;
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

        /// <summary>
        /// Processes the request to register a new user and contact (every user must have a contact).
        /// </summary>
        /// <param name="registrationData"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        async Task<Contact?> RegisterNewUser(UserLoginRegistrationModel registrationData)
        {
            if (this.ApiRepositorySvc != null)
            {
                Contact? ret = await this.ApiRepositorySvc.PostData<Contact, UserLoginRegistrationModel>("PresentationAPI:Register", registrationData);
                return ret;
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

        void LoginComponent_OnLogin(UserLogin userLogin)
        {
            if (userLogin != null)
            {
                if (this.SessionService != null)
                {
                    this.SessionService.ActiveUser = userLogin;
                }

                if (this.NavManager != null)
                {
                    this.NavManager.NavigateTo("/");
                }
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

        void NewLoginCoponent_OnRegisterSuccess(Contact contact)
        {
            this.ShowNewLoginForm = false;

        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

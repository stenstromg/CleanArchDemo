using Demo.Domain.Models;
using Demo.WebApp.Classes;

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

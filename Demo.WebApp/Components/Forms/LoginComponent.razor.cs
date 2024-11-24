using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Forms
{
    public partial class LoginComponent : UnqComponentBase
    {
        #region properties

        public string? Username { get; set; }

        public string? Password { get; set; }

        #endregion properties

        #region parameters

        [Parameter]
        public EventCallback<LoginComponent> OnLoginClick { get; set; }

        [Parameter]
        public EventCallback OnRegisterClick { get; set; }

        #endregion parameters

        #region data
        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers

        void btnLogin_OnClick()
        {
            OnLoginClick.InvokeAsync(this);
        }

        void btnRegister_OnClick()
        {
            OnRegisterClick.InvokeAsync();
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

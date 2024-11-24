using Demo.App.Interfaces;
using Demo.WebApp.Classes.Services;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Classes
{
    public class UnqPageBase : ComponentBase
    {

        #region inject

        [Inject]
        public IWebAPIService? ApiService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        [Inject]
        public IApplicationSessionService? SessionService { get; set; }

        #endregion inject

        #region lifecycle

        protected override Task OnInitializedAsync()
        {

            return base.OnInitializedAsync();
        }


        protected override void OnAfterRender(bool firstRender)
        {
            if (this.SessionService != null && this.SessionService.ActiveUser == null)
            {
                if (this.NavManager != null)
                {
                    this.NavManager.NavigateTo("/login");
                }
            }
            base.OnAfterRender(firstRender);
        }


        #endregion lifecycle
    }
}

using Demo.App.Interfaces;
using Demo.App.Services;
using Demo.WebApp.Classes.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Demo.WebApp.Classes
{
    public class UnqPageBase : ComponentBase
    {

        #region inject

        [Inject]
        public IWebApiRepoService? ApiRepositorySvc { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        [Inject]
        public IApplicationSessionService? SessionService { get; set; }

        [Inject]
        public ThemeService? ThemeService{ get; set; }

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

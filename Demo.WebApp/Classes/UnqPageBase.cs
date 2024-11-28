using Demo.App.Interfaces;
using Demo.App.Interfaces.WebAPI;
using Demo.App.Services;
using Demo.App.Services.WebAPI;
using Demo.WebApp.Classes.Services;
using Demo.WebApp.Components.Layout;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Demo.WebApp.Classes
{
    public class UnqPageBase : ComponentBase
    {

        #region parameters

        [CascadingParameter(Name ="Layout")]
        protected MainLayout? Layout { get; set; }

        #endregion parameters

        #region inject

        [Inject]
        public NavigationManager? NavManager { get; set; }

        [Inject]
        public IApplicationSessionService? SessionService { get; set; }

        [Inject]
        public ThemeService? ThemeService{ get; set; }

        [Inject]
        public ApplicationConfigurationService? AppService{ get; set; }

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

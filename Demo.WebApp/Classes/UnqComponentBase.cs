using Demo.App.Interfaces;
using Demo.App.Interfaces.WebAPI;
using Demo.App.Services;
using Demo.WebApp.Classes.Services;
using Demo.WebApp.Components.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Demo.WebApp.Classes
{
    public partial class UnqComponentBase : ComponentBase
    {

        #region parameters

        [CascadingParameter(Name = "Layout")]
        protected MainLayout? Layout { get; set; }

        #endregion parameters


        #region inject

        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        [Inject]
        public IApplicationSessionService? AppSession { get; set; }

        [Inject]
        public ApplicationConfigurationService? AppConfigService { get; set; }

        #endregion inject
    }
}

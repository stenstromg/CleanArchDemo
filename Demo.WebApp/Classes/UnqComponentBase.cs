using Demo.App.Interfaces;
using Demo.App.Services;
using Demo.WebApp.Classes.Services;
using Demo.WebApp.Components.Layout;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Classes
{
    public partial class UnqComponentBase : ComponentBase
    {

        #region parameters

        [CascadingParameter(Name = "Layout")]
        MainLayout? Layout { get; set; }

        #endregion parameters


        #region inject

        [Inject]
        public IWebApiRepoService? ApiRepositorySvc { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        [Inject]
        public IApplicationSessionService? SessionService { get; set; }

        #endregion inject
    }
}

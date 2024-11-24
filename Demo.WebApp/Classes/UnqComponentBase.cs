using Demo.App.Interfaces;
using Demo.WebApp.Classes.Services;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Classes
{
    public partial class UnqComponentBase : ComponentBase
    {

        #region inject

        [Inject]
        public IWebAPIService? ApiService { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        [Inject]
        public IApplicationSessionService? SessionService { get; set; }

        #endregion inject
    }
}

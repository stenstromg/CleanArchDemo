
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        #region properties

        bool sidebar1Expanded { get; set; } = true;

        #endregion properties

        #region data
        #endregion data

        #region ctor
        #endregion ctor

        #region event handlers


        void Toggle_OnClick()
        {
            sidebar1Expanded = !sidebar1Expanded;
            StateHasChanged();
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

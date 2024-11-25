
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {

        #region data

        public enum Menus
        {
            None = 0,
            ContactMenu = 1,
            BudgetMenu = 2,
            SalesMenu = 3
        }

        #endregion data

        #region properties

        bool ContactMenuIsVisible { get; set; } = false;

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

        public void SetActiveMenu(Menus activeMenu)
        {
            this.ContactMenuIsVisible = false;

            switch (activeMenu)
            {
                case Menus.None:
                    break;
                case Menus.ContactMenu:
                    this.ContactMenuIsVisible = true;
                    break;
                case Menus.BudgetMenu:
                    break;
                case Menus.SalesMenu:
                    break;
            }

        }

        #endregion public
    }
}

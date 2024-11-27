using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Modules.Contacts
{
    public partial class ContactMenuComponent : UnqComponentBase
    {
        #region properties

        bool SaveButtonIsEnabled { get; set; } = false;

        #endregion properties

        #region parameters

        [Parameter]
        public bool Visible { get; set; } = false;

        [Parameter]
        public EventCallback OnSaveClick { get; set; }

        #endregion parameters

        #region data
        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers

        void btnSave_OnClick()
        {
            if (OnSaveClick.HasDelegate)
            {
                this.OnSaveClick.InvokeAsync();
            }
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public

        public void EnableSaveButton(bool isEnabled)
        {
            this.SaveButtonIsEnabled = isEnabled;
            base.StateHasChanged();
        }

        #endregion public
    }
}

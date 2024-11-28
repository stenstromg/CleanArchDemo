using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

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
        public EventCallback OnAddNewClick { get; set; }

        [Parameter]
        public EventCallback OnSaveClick { get; set; }

        #endregion parameters

        #region data
        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers

        void btnContact_OnClick(RadzenSplitButtonItem arg)
        {
            switch (arg.Value.ToLower())
            {
                case "add-contact":
                    if (this.OnAddNewClick.HasDelegate)
                    {
                        this.OnAddNewClick.InvokeAsync();
                    }
                    break;
            }
        }

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

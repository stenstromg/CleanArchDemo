using Demo.App.Interfaces.WebAPI;
using Demo.App.Services.WebAPI;
using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Demo.WebApp.Components.Forms;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Demo.WebApp.Components.Modules.Contacts
{
    public partial class ContactEditorComponent : UnqComponentBase
    {
        #region properties

        DialogOptions? DialogOptions { get; set; }

        #endregion properties

        #region parameters

        [Parameter]
        public Contact? Contact { get; set; } = null;

        [Parameter]
        public bool Visible { get; set; } = false;

        #endregion parameters

        #region inject

        [Inject]
        IContactApiService? ContactApiService { get; set; }

        [Inject]
        DialogService? DialogService { get; set; }

        #endregion inject

        #region data

        private async Task SaveContact()
        {
        }

        #endregion data

        #region delegates
        #endregion delegates

        #region lifecycle

        protected override void OnInitialized()
        {
            base.Layout.ContactMenu.EnableSaveButton(false);
            base.Layout.ContactMenu.OnSaveClick = new EventCallback(this, (Action)MenuBtnSave_OnClick);

            base.OnInitialized();
        }

        #endregion lifecycle

        #region event handlers

        void btAddEmail_OnClick()
        {
            this.Contact.Emails.Add(new Email() { DbAction = Domain.Enums.EntityActions.Add });
            base.StateHasChanged();
        }

        void btnEditEmail_OnClick(long emailID)
        {
            this.LaunchEmailEditor(emailID);
        }

        void EmailEditor_OnChange()
        {
            base.Layout?.ContactMenu?.EnableSaveButton(true);
        }

        void MenuBtnSave_OnClick()
        {
            string save = "Now is the time to save";
        }

        #endregion event handlers

        #region private

        void SetDialogOptions()
        {
            this.DialogOptions = new DialogOptions()
            {
                //Resizable = true,
                //Draggable = true,
                //Resize = OnResize,
                //Drag = OnDrag,
                Width   = this.DialogOptions != null ? this.DialogOptions.Width : "700px",
                Height  = this.DialogOptions != null ? this.DialogOptions.Height : "512px",
                Left    = this.DialogOptions != null ? this.DialogOptions.Left : null,
                Top     = this.DialogOptions != null ? this.DialogOptions.Top : null
            };
        }

        async void LaunchEmailEditor(long emailID)
        {
            this.SetDialogOptions();

            // Get the selected email
            //
            Email? email = this.Contact?.Emails?.Where(e=>e.ID == emailID).FirstOrDefault();

            Dictionary<string, object?> parms = new Dictionary<string, object?>() { { "Email", email} };

            await this.DialogService.OpenAsync<EmailEditorComponent>("Email Editor", parms, this.DialogOptions);
        }

        #endregion private

        #region public
        #endregion public
    }
}

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

        bool ShowErrorMessage { get; set; } = false;

        string? ErrorMessage { get; set; }

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

        private async Task<Contact?> SaveContact()
        {
            Contact? ret = this.Contact;

            try
            {
                if (this.ContactApiService != null && this.Contact != null)
                {
                    string? apiURL = base.AppConfigService?.GetWebServiceFunctionURL("SaveContact");

                    ret = await this.ContactApiService.SaveContact(apiURL, this.Contact);
                }
            }
            catch(Exception ex)
            {
                this.ErrorMessage = ex.Message;
                this.ShowErrorMessage = true;
            }


            return ret;
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

        void EmailEditor_OnChange()
        {
            base.Layout?.ContactMenu?.EnableSaveButton(true);
        }

        async void MenuBtnSave_OnClick()
        {
            // Open the "Busy" dialog
            //
            this.ShowBusyDialog("Saving Contact ...");

            await this.SaveContact();

            // When the contact is finished being saved, disable the Save button
            //
            base.Layout?.ContactMenu?.EnableSaveButton(false);

            // Close the "Busy" dialog
            //
            this.DialogService?.Close();

            // Alert and Confirm
            //
            this.ShowAlert("Saved");

            // Open the acknowledgment dialog
            //

            base.StateHasChanged();
        }

        #endregion event handlers

        #region private

        DialogOptions GetDialogOptionsFor(string action)
        {
            DialogOptions options = new DialogOptions();

            switch (action.ToLower())
            {
                case "save":
                    options = new DialogOptions() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto", CloseDialogOnEsc = false };
                    break;
                case "save-success":
                    options = new DialogOptions() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto", CloseDialogOnEsc = false };
                    break;
                case "save-failure":
                    options = new DialogOptions() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto", CloseDialogOnEsc = false };
                    break;
            }

            return options;
        }

        void ShowBusyDialog(string? message)
        {
            DialogOptions options = this.GetDialogOptionsFor("save");

            if (this.DialogService != null)
            {
                this.DialogService.OpenAsync("", ds =>
                {
                    RenderFragment? dialogContent = builder =>
                    {
                        builder.OpenElement(0, "RadzenRow");
                        builder.AddAttribute(1, "class", "rz-text-align-center rz-p-4");

                        builder.OpenElement(2, "RadzenColumn");
                        builder.AddAttribute(3, "Size", "12");

                        builder.AddContent(4, message);

                        builder.CloseElement();
                        builder.CloseElement();
                    };

                    return dialogContent;

                }, options);
            }
        }

        void ShowAlert(string message, string? title = null)
        {

            bool showTitle = (title == null) ? false : true;

            if (this.DialogService != null)
            {
                this.DialogService.Alert(message, title, new AlertOptions() { ShowTitle = showTitle, OkButtonText = "Ok", CssClass= "rz-align-items-center rz-p-12" });
            }
        }

        #endregion private

        #region public
        #endregion public
    }
}

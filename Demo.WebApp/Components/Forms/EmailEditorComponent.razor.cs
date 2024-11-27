using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Forms
{
    public partial class EmailEditorComponent : UnqComponentBase
    {
        #region properties
        #endregion properties

        #region parameters

        [Parameter]
        public Contact Contact { get; set; }

        [Parameter]
        public EventCallback OnEmailHasChanged { get; set; }

        #endregion parameters

        #region data
        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers

        void btnAddEmail_OnClick()
        {
            this.Contact.Emails.Add(new Email() { DbAction = Domain.Enums.EntityActions.Add });
            base.StateHasChanged();

            this.OnEmailHasChanged.InvokeAsync();
        }

        void btnDeleteEmail_OnClick(Email email)
        {
            this.RemoveEmailFromContact(email);
            base.StateHasChanged();

            this.OnEmailHasChanged.InvokeAsync();
        }

        void btnPrimary_OnClick(Email email)
        {
            Email? prevEmail = this.Contact.Emails.FirstOrDefault(e=>e.IsPrimary);
            if (prevEmail != null) 
            { 
                prevEmail.IsPrimary = false;
                if (prevEmail.ID != default(long))
                {
                    prevEmail.DbAction = Domain.Enums.EntityActions.Update;
                }
            }

            email.IsPrimary = true;
            base.StateHasChanged();

            this.OnEmailHasChanged.InvokeAsync();
        }

        void txtEmail_OnChange(Email email, string value)
        {
            email.SetEmailAddress(value);
            email.DbAction = (email.ID != default(long)) ? Domain.Enums.EntityActions.Update : email.DbAction;
            base.StateHasChanged();

            this.OnEmailHasChanged.InvokeAsync();
        }


        #endregion event handlers

        #region private

        bool IsPrimaryEmail(Email email)
        {
            return (email.ID == this.Contact.PrimaryEmailID);
        }

        void RemoveEmailFromContact(Email email)
        {
            // Capture whether the email being deleted is the primary email
            //
            bool replacePrimary = email.IsPrimary;

            // If this is the UserLogin contact information, then there must always be at least one
            // email.
            //
            if (this.Contact.UserID == this.AppSession.ActiveUser.ID)
            {
                if (this.Contact.Emails.Count <= 1)
                {
                    // cannot delete the email
                    return;
                }
            }

            // If the email has not yet been saved, then just remove the email from the contact.
            //
            if (email.ID == default(long))
            {
                this.Contact.Emails.Remove(email);
            }

            // Otherwise, flag the email for deletion when the contact is saved.
            //
            else
            {
                email.DbAction = Domain.Enums.EntityActions.Remove;
                email.IsPrimary = false;
            }

            // Theis means that we deleted the primary email address. Randomly select another until
            // the user does it.
            //
            if (replacePrimary)
            {
                Email? newPrimaryEmail = this.Contact.Emails.FirstOrDefault(e => e.DbAction != Domain.Enums.EntityActions.Remove);
                if (newPrimaryEmail != null)
                {
                    newPrimaryEmail.IsPrimary = true;
                    newPrimaryEmail.DbAction = Domain.Enums.EntityActions.Update;
                }
            }

        }

        #endregion private

        #region public
        #endregion public
    }
}

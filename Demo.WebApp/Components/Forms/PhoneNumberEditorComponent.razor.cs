using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Forms
{
    public partial class PhoneNumberEditorComponent : UnqComponentBase
    {
        #region properties
        #endregion properties

        #region parameters

        [Parameter]
        public Contact Contact { get; set; }

        [Parameter]
        public EventCallback OnPhoneHasChanged { get; set; }

        #endregion parameters

        #region data
        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers

        void btnAddPhone_OnClick()
        {
            this.Contact.PhoneNumbers.Add(new PhoneNumber() { DbAction = Domain.Enums.EntityActions.Add });
            base.StateHasChanged();

            this.OnPhoneHasChanged.InvokeAsync();
        }

        void btnDeletePhone_OnClick(PhoneNumber phone)
        {
            this.RemovePhoneFromContact(phone);
            base.StateHasChanged();

            this.OnPhoneHasChanged.InvokeAsync();
        }

        void btnPrimaryPhone_OnClick(PhoneNumber phone)
        {
            PhoneNumber? prevPhone = this.Contact.PhoneNumbers.FirstOrDefault(e => e.IsPrimary);
            if (prevPhone != null)
            {
                prevPhone.IsPrimary = false;
                if (prevPhone.ID != default(long))
                {
                    prevPhone.DbAction = Domain.Enums.EntityActions.Update;
                }
            }

            phone.IsPrimary = true;
            phone.DbAction  = Domain.Enums.EntityActions.Update;
            base.StateHasChanged();

            this.OnPhoneHasChanged.InvokeAsync();
        }

        void txExtension_OnChange(PhoneNumber phone, string value)
        {
            phone.Extension = value;

            this.OnPhoneHasChanged.InvokeAsync(phone);
        }

        void txPhone_OnChange(PhoneNumber phone, string value)
        {
            phone.SetPhoneNumber(value);
            phone.DbAction = (phone.ID != default(long)) ? Domain.Enums.EntityActions.Update : phone.DbAction;
            base.StateHasChanged();

            this.OnPhoneHasChanged.InvokeAsync(phone);
        }

        #endregion event handlers

        #region private

        bool IsPrimaryPhone(PhoneNumber phone)
        {
            return (phone.ID == this.Contact.PrimaryPhoneNumberID);
        }

        void RemovePhoneFromContact(PhoneNumber phone)
        {
            // Capture whether the PhoneNumber being deleted is the primary PhoneNumber
            //
            bool replacePrimary = phone.IsPrimary;

            // If the PhoneNumber has not yet been saved, then just remove the PhoneNumber from the
            // contact.
            //
            if (phone.ID == default(long))
            {
                this.Contact.PhoneNumbers.Remove(phone);
            }

            // Otherwise, flag the PhoneNumber for deletion when the contact is saved.
            //
            else
            {
                phone.DbAction = Domain.Enums.EntityActions.Remove;
                phone.IsPrimary = false;
            }

            // Theis means that we deleted the primary phone number. Randomly select another until
            // the user does it.
            //
            if (replacePrimary)
            {
                PhoneNumber? newPrimaryPhoneNumber = this.Contact.PhoneNumbers.FirstOrDefault(e => e.DbAction != Domain.Enums.EntityActions.Remove);
                if (newPrimaryPhoneNumber != null)
                {
                    newPrimaryPhoneNumber.IsPrimary = true;
                    newPrimaryPhoneNumber.DbAction = Domain.Enums.EntityActions.Update;
                }
            }

        }

        #endregion private

        #region public
        #endregion public
    }
}

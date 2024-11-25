using Azure;
using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.Domain.Models;
using Demo.WebApp.Classes;

namespace Demo.WebApp.Components.Modules.Contacts
{
    public partial class ContactListComponent : UnqComponentBase
    {
        #region properties

        IEnumerable<Contact>? ContactList { get; set; } = null;

        string? ErrorMessage { get; set; } = null;

        bool ShowErrorMessage { get; set; } = false;

        #endregion properties

        #region parameters
        #endregion parameters

        #region data

        async Task<ICollection<Contact>?> GetContactsForUserID(long userID)
        {
            if (this.ApiRepositorySvc != null)
            {
                string apiKey = "PresentationAPI:GetContactsForUserID";
                dynamic dto = new { id  = userID };
                WebServiceResponse response = await this.ApiRepositorySvc.PostData2<ICollection<Contact>, dynamic>(apiKey, dto);

                List<Contact>? ret = null;

                if (response != null)
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            ret = (List<Contact>?)response.Payload;
                            break;
                        default:
                            this.ErrorMessage = $"Api Request failed : {response.Message}";
                            this.ShowErrorMessage = true;
                            break;
                    }
                }

                return ret;
            }
            else
            {
                throw new NullReferenceException("ApiRepositoryService not available.");
            }
        }

        #endregion data

        #region lifecycle

        protected override async Task OnInitializedAsync()
        {
            if (base.SessionService != null && base.SessionService.ActiveUser != null)
            {
                this.ContactList = await this.GetContactsForUserID(base.SessionService.ActiveUser.ID);
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        #endregion lifecycle

        #region event handlers
        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}

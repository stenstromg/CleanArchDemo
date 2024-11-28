using Demo.Domain.Models;
using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;

namespace Demo.WebApp.Components.Controls
{
    public partial class AvatarComponent : UnqComponentBase
    {
        #region properties

        string AvatarSizeCSS { get; set; } = "unq-profile-icon unq-profile-icon-regular";

        #endregion properties

        #region parameters

        [Parameter]
        public Person? Person { get; set; }

        [Parameter]
        public AvatarSizes Size { get; set; } = AvatarSizes.Regular;

        #endregion parameters

        #region enum

        public enum AvatarSizes 
        {
            Small,
            Regular, 
            Large
        }

        #endregion enum

        #region data
        #endregion data

        #region lifecycle

        protected override void OnInitialized()
        {
            this.InitializeAvatarSize();
            base.OnInitialized();
        }

        #endregion lifecycle

        #region event handlers
        #endregion event handlers

        #region private

        string GetInitials()
        {
            string ret = "A";

            if (this.Person != null)
            {
                string? firstInitial = this.Person.FirstName?.Substring(0, 1);
                string? lastInitial  = this.Person.LastName?.Substring(0, 1);

                ret = $"{firstInitial}{lastInitial}";
            }

            return ret;
        }

        void InitializeAvatarSize()
        {
            string stage = "unq-profile-icon";

            switch (this.Size)
            {
                case AvatarSizes.Small:
                    this.AvatarSizeCSS = $"{stage} unq-profile-icon-regular";
                    break;
                case AvatarSizes.Regular:
                    this.AvatarSizeCSS = $"{stage} unq-profile-icon-regular";
                    break;
                case AvatarSizes.Large:
                    this.AvatarSizeCSS = $"{stage} unq-profile-icon-large";
                    break;
            }
        }


        #endregion private

        #region public
        #endregion public
    }
}

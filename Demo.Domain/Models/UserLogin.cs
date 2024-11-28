using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Demo.Domain.Models
{
    /// <summary>
    /// Represents a user's login to the system
    /// </summary>
    [Table("USER_LOGIN")]
    public class UserLogin : EntityBase
    {
        #region properties

        //[JsonPropertyName("email")]
        //[Column("email_id")]
        //public Email? Email { get; set; }

        [JsonPropertyName("failedLoginCount")]
        [Column("failed_login_count")]
        public int FailedLoginCount { get; set; } = 0;

        [JsonPropertyName("isEnabled")]
        [Column("enabled", TypeName = "bit")]
        public bool IsEnabled{ get; set; } = true;

        [JsonPropertyName("isLocked")]
        [Column("locked", TypeName ="bit")]
        public bool IsLocked { get; set; } = false;

        [JsonPropertyName("password")]
        [Column("password")]
        [StringLength (1000, MinimumLength = 5)]
        public string? Password{ get; set; }

        [JsonPropertyName("lastLoginDate")]
        [Column("last_login_date", TypeName ="datetime2")]
        public DateTime LastLoginDate { get; set; }

        [JsonPropertyName("passwordMustBeChanged")]
        [Column("password_must_be_changed", TypeName = "bit")]
        public bool PasswordMustBeChanged { get; set; } = false;

        [JsonPropertyName("username")]
        [Column("username")]
        [StringLength(200, MinimumLength = 5)]        
        public string? Username{ get; set; }

        #endregion properties

        #region navigation properties

        [JsonPropertyName("person")]
        public Person? Person { get; set; }

        #endregion navigation properties

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Demo.Domain.Models
{
    /// <summary>
    /// Represents a person. A person may be associated with a user account, contact for a business, etc.
    /// </summary>
    [Table("PEOPLE")]
    public class Person : EntityBase
    {
        #region properties

        /// <summary>
        /// Gets/Sets a person's First Name
        /// </summary>
        [JsonPropertyName("firstName")]
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [Column("first_name")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets/Sets a person's Last Name
        /// </summary>
        [JsonPropertyName("lastName")]
        [Required]
        [StringLength(maximumLength: 120, MinimumLength = 2)]
        [Column("last_name")]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets/Sets a person's date of birth
        /// </summary>
        [JsonPropertyName("dateOfBirth")]
        [Column("dob", TypeName ="datetime2")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets/Sets a person's gender
        /// </summary>
        [JsonPropertyName("gender")]
        [Column("gender")]
        [StringLength(1)]
        public string? Gender { get; set; }

        #endregion properties

        #region navigation properties

        /// <summary>
        /// Gets the List of User accounts associated with the Person
        /// </summary>
        //[JsonPropertyName("userLogins")]
        public List<UserLogin>? UserLogins { get; set; }

        #endregion navigation properties
    }
}

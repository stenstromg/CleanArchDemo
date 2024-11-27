using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Demo.Domain.Models
{
    [Table("EMAILS")]
    public class Email : EntityBase
    {
        #region properties

        /// <summary>
        /// Gets/Sets the domain section of the email address
        /// </summary>
        [JsonPropertyName("domain")]
        [Column("email_domain")]
        public string? Domain { get; set; }

        /// <summary>
        /// Gets/Sets username section of the email address. 
        /// </summary>
        [JsonPropertyName("emailUsername")]
        [Column("email_username")]
        public string?  EmailUsername { get; set; }

        /// <summary>
        ///  Gets/Sets a flag indicating whether this is the primary email address in a contact 
        ///  record.
        /// </summary>
        [JsonPropertyName("isPrimary")]
        [NotMapped]
        public bool IsPrimary { get; set; } = false;

        /// <summary>
        /// Gets/Sets the label of the email address (e.g. Cell, Business, etc.)
        /// </summary>
        [JsonPropertyName("label")]
        [Column("label")]
        public string? Label { get; set; }

        #endregion properties

        #region navigation properties

        [JsonPropertyName("contactId")]
        [ForeignKey("Contact")]
        [Column("contact_id")]
        public long? ContactId { get; set; }

        [JsonPropertyName("contact")]
        public virtual Contact? Contact { get; set; }

        #endregion navigation properties

        #region ctor

        public Email() { }

        public Email(string emailAddress)
        {
            this.SetEmailAddress(emailAddress);
        }

        #endregion ctor

        #region public

        /// <summary>
        /// Sets the Email address. 
        /// </summary>
        /// <param name="emailAddress"></param>
        public void SetEmailAddress(string emailAddress)
        {
            this.ParseDomain(emailAddress);
            this.ParseUsername(emailAddress);
        }

        /// <summary>
        /// Returns the fully assembled email address
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{EmailUsername}@{Domain}";
        }

        /// <summary>
        /// Creates a new Email instance by parsing the <paramref name="emailAddress"/> argument.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool TryParse(string emailAddress, out Email email)
        {
            email = new Email(emailAddress);
            return true;
        }

        #endregion public 

        #region private

        /// <summary>
        /// Parses the domain from the <paramref name="emailAddress"/> argument and assigns it to 
        /// the Domain property
        /// </summary>
        /// <param name="emailAddress"></param>
        void ParseDomain(string emailAddress)
        {
            int startIDX = (emailAddress.IndexOf("@") + 1);
            this.Domain  = emailAddress.Substring(startIDX);
        }

        /// <summary>
        /// Parses the username from the <paramref name="emailAddress"/> argument and assigns it to 
        /// the username property
        /// </summary>
        /// <param name="emailAddress"></param>
        void ParseUsername(string emailAddress)
        {
            int length = (emailAddress.IndexOf("@"));
            this.EmailUsername = emailAddress.Substring(0, length);
        }

        #endregion private
    }
}

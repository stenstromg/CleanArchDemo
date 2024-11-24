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
    [Table("CONTACTS")]
    public class Contact : EntityBase
    {
        #region properties

        /// <summary>
        /// Gets/Sets the Person associated with this contact record
        /// </summary>
        public Person? Person { get; set; }

        /// <summary>
        /// Gets/Sets the userprofile / login associated with this contract record
        /// </summary>
        public UserLogin? UserProfile { get; set; }

        /// <summary>
        /// Gets/Sets the Label for this contact (e.g. Mom, Daughter, Hiring Manager, etc.)
        /// </summary>
        [Column("label")]
        [MaxLength(120)]
        public string? Label { get; set; }

        /// <summary>
        /// Gets/Sets the Primary Email for this contact
        /// </summary>
        //[ForeignKey("PrimaryEmail")]
        [Column("primary_email_id")]
        public long? PrimaryEmailID { get; set; }

        [NotMapped]
        public Email? PrimaryEmail { get { return this.Emails?.FirstOrDefault(e => e.ID == this.PrimaryEmailID); } }

        /// <summary>
        /// Gets/Sets the Primary phone number for this contact
        /// </summary>
        [Column("primary_phone_id")]
        public long? PrimaryPhoneNumberID { get; set; }

        #endregion properties

        #region ctor

        public Contact()
        {
            
        }

        #endregion ctor

        #region navigation properties

        /// <summary>
        /// Gets List of all Email addresses associated with this contact. When adding and removing 
        /// emails use AddEmail and RemoveEmail methods.
        /// </summary>
        public virtual ICollection<Email> Emails { get; set; } = new HashSet<Email>();

        /// <summary>
        /// Gets List of all phone numbers associated with this contact. When adding and removing 
        /// phoneNumbers use AddPhoneNumbers and RemovePhoneNumbers methods.
        /// </summary>
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new HashSet<PhoneNumber>();   

        #endregion navigation properties

        #region public

        //public void AddEmail(Email email)
        //{
        //    if (this.Emails == null)
        //    {
        //        this.Emails = new List<Email>();
        //    }

        //    email.DbAction = Enums.EntityActions.Add;
        //    this.Emails.Add(email);
        //}

        //public void AddPhoneNumber(PhoneNumber phoneNumber)
        //{
        //    if (this.PhoneNumbers == null)
        //    {
        //        this.PhoneNumbers = new List<PhoneNumber>();
        //    }

        //    phoneNumber.DbAction = Enums.EntityActions.Add;
        //    this.PhoneNumbers.Add(phoneNumber);
        //}

        //public void RemoveEmail(int id)
        //{
        //    if (this.Emails != null)
        //    {
        //        Email? emailToRemove = this.Emails.FirstOrDefault(e => e.ID == id);

        //        if (emailToRemove != null)
        //        {
        //            emailToRemove.DbAction = Enums.EntityActions.Remove;
        //        }
        //    }
        //}

        //public void RemovePhoneNumber(int id)
        //{
        //    if (this.PhoneNumbers != null)
        //    {
        //        PhoneNumber? phoneToRemove = this.PhoneNumbers.FirstOrDefault(e => e.ID == id);

        //        if (phoneToRemove != null)
        //        {
        //            phoneToRemove.DbAction = Enums.EntityActions.Remove;
        //        }
        //    }
        //}

        //public void SetPrimaryEmail(Email email)
        //{
        //    this.PrimaryEmail = email;
        //}

        //public void SetPrimaryPhone(PhoneNumber phone)
        //{
        //    this.PrimaryPhone = phone;
        //}

        #endregion public
    }
}

using Demo.App.Interfaces;
using Demo.App.Models;
using Demo.Domain.Models;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Demo.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        #region properties

        DemoDbContext _db { get; set; }

        DateTime _timestamp { get; set; }

        string _updatedBy { get; set; }

        #endregion properties

        #region ctor

        public ContactRepository(DemoDbContext ctx)
        {
            this._db = ctx;
        }

        #endregion ctor

        #region public

        public Contact CreateContact(Contact contact, string author = "AUTO")
        {
            bool success = this.SaveContact(contact, author);
            //this._db.Contacts.Add(contact);
            //this._db.SaveChanges();
            return contact;
        }

        public Contact CreateContactForLogin(UserLoginRegistrationModel model, string author = "AUTO")
        {
            this._timestamp  = DateTime.UtcNow;
            this._updatedBy  = author;
            Contact? contact = null;

            var errMsg = model.Validate();

            if (errMsg != null && errMsg.Count() > 0)
            {
                throw new InvalidDataException(errMsg.FirstOrDefault());
            }

            using (IDbContextTransaction transaction = this._db.Database.BeginTransaction())
            {
                // Prepare the model contents to be saved
                //
                model.PrepareForSave();

                try
                {
                    // Check to make sure that the Username is unique
                    //
                    UserLogin userLogin = this._db.UserLogins.Where(e => e.Username.ToLower() == model.Username.ToLower()).FirstOrDefault();

                    if (userLogin != null)
                    {
                        throw new DuplicateNameException("The username is already in use");
                    }

                    // Save associated PhoneNumber in order to get an ID. 
                    //
                    if (model.PhoneNumber != null)
                    {
                        model.PhoneNumber.UpdatedBy = model.PhoneNumber.CreatedBy = this._updatedBy;
                        model.PhoneNumber.UpdatedDate = model.PhoneNumber.CreatedDate = this._timestamp;
                        this._db.PhoneNumbers.Add(model.PhoneNumber);
                    }

                    // Save associated Email in order to get an ID. 
                    //
                    if (model.Email != null)
                    {
                        model.Email.UpdatedBy = model.Email.CreatedBy = this._updatedBy;
                        model.Email.UpdatedDate = model.Email.CreatedDate = this._timestamp;
                        this._db.Emails.Add(model.Email);
                    }

                    // Save associated Person in order to get an ID. 
                    //
                    model.Person.CreatedBy = model.Person.UpdatedBy = this._updatedBy;
                    model.Person.CreatedDate = model.Person.UpdatedDate = this._timestamp;
                    this._db.People.Add(model.Person);

                    // Save associated UserLogin in order to get an ID. 
                    //
                    model.UserLogin.CreatedBy = model.UserLogin.UpdatedBy = this._updatedBy;
                    model.UserLogin.CreatedDate = model.UserLogin.UpdatedDate = this._timestamp;
                    model.UserLogin.Person = model.Person;
                    model.UserLogin.Email = model.Email;
                    this._db.UserLogins.Add(model.UserLogin);

                    this._db.SaveChanges();

                    contact = new Contact()
                    {
                        DbAction = Domain.Enums.EntityActions.Add,
                        PrimaryEmailID = model.Email?.ID,
                        PrimaryPhoneNumberID = model.PhoneNumber?.ID,
                        Emails = (model.Email == null) ? new List<Email>() : new List<Email>() { model.Email },
                        PhoneNumbers = (model.PhoneNumber == null) ? new List<PhoneNumber>() : new List<PhoneNumber>() { model.PhoneNumber },
                        Person = model.Person,
                        UserProfile = model.UserLogin,
                        UserID = model.UserLogin.ID,
                        CreatedBy = this._updatedBy,
                        CreatedDate = this._timestamp,
                        UpdatedDate = this._timestamp,
                        UpdatedBy = this._updatedBy,
                    };

                    this._db.Contacts.Add(contact);
                    this._db.SaveChanges();
                }
                catch (DuplicateNameException dupeX)
                {
                    throw new DuplicateNameException(dupeX.Message);
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null) ex = ex.InnerException;
                    throw new DbUpdateException($"The Contact for Login failed to save : {ex.Message}");
                }

                transaction.Commit();
            }

            return contact;
        }

        public bool DeleteContact(long id)
        {
            Contact? contact = this._db.Contacts.Where(e => e.ID == id)
                                         .Include(c => c.Emails)
                                         .Include(c => c.PhoneNumbers)
                                         .Include(c => c.Person)
                                         .FirstOrDefault();

            if (contact != null)
            {
                try
                {
                    this._db.Contacts.Remove(contact);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;
        }

        public Contact? GetContactByID(long id)
        {
            Contact? ret = this._db.Contacts.Where(e=>e.ID == id)
                                         .Include(c=>c.Emails)
                                         .Include(c=>c.PhoneNumbers)
                                         .Include(c=>c.Person)
                                         .FirstOrDefault();
            return ret;
        }

        public List<Contact>? GetContacts(List<Expression<Func<Contact, bool>>>? filters = null)
        {
            var query = this._db.Contacts.Include(c => c.Emails)
                                         .Include(c => c.PhoneNumbers)
                                         .Include(c => c.Person)
                                         .AsQueryable();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            List<Contact> ret = query.ToList();

            return ret;
        }

        public bool SaveContact(Contact contactModel, String updatedBy)
        {
            this._timestamp = DateTime.UtcNow;
            this._updatedBy = updatedBy;

            var contactEntity = this._db.Contacts.Where(e => e.ID == contactModel.ID)
                                          .Include(c => c.Emails)
                                          .Include(c => c.PhoneNumbers)
                                          .Include(c => c.Person)
                                          .Include(c => c.UserProfile)
                                          .FirstOrDefault();
            if (contactEntity != null)
            {
                if (contactEntity.Emails != null)
                {
                    this.UpdateEmails(contactEntity, contactModel);
                }

                if (contactEntity.PhoneNumbers != null)
                {
                    this.UpdatePhoneNumbers(contactEntity, contactModel);
                }

                try
                {
                    this._db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                if (contactModel.Emails != null)
                {
                    foreach (var email in contactModel.Emails)
                    {
                        email.Contact = contactModel;
                    }
                }

                if (contactModel.UserProfile != null && contactModel.UserProfile.Email != null)
                {
                    contactModel.UserProfile.Email.Contact = contactModel;
                }

                //if (contactModel.PrimaryEmail != null)
                //{
                //    contactModel.PrimaryEmail.Contact = contactModel;
                //}

                try
                {
                    this._db.Contacts.Add(contactModel);
                    this._db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;

        }

        #endregion public

        #region private

        /// <summary>
        /// Updates the email objects associated with the <paramref name="contactEntity"/> argument with 
        /// values from the email objects associated with the <paramref name="contactModel"/> argument.
        /// </summary>
        /// <param name="contactEntity"></param>
        /// <param name="contactModel"></param>
        void UpdateEmails(Contact contactEntity, Contact contactModel)
        {
            if (contactModel.Emails != null)
            {
                foreach (var emailModel in contactModel.Emails)
                {
                    switch (emailModel.DbAction)
                    {
                        case Domain.Enums.EntityActions.Add:
                            this.AddEmailToContact(contactEntity, emailModel);
                            break;
                        case Domain.Enums.EntityActions.Remove:
                            this.RemoveEmailFromContact(contactEntity, emailModel);
                            break;
                        case Domain.Enums.EntityActions.Update:
                            this.UpdateEmailInContact(contactEntity, emailModel);
                            break;
                    }
                }
            }
        }

        void AddEmailToContact(Contact contactEntity, Email emailModel)
        {
            if (contactEntity != null)
            {
                if (contactEntity.Emails == null)
                {
                    contactEntity.Emails = new List<Email>();
                }

                contactEntity.Emails.Add(emailModel);
            }
        }

        void RemoveEmailFromContact(Contact contactEntity, Email emailModel)
        {
            if (contactEntity != null)
            {
                if (contactEntity.Emails != null)
                {
                    // Find the email in the Contact Emails collection
                    //
                    Email? emailEntity = contactEntity.Emails.Where(e=>e.ID == emailModel.ID)
                                                            .FirstOrDefault();
                    if (emailEntity != null)
                    {
                        contactEntity.Emails?.Remove(emailModel);
                    }
                }
            }
        }

        void UpdateEmailInContact(Contact contactEntity, Email emailModel)
        {
            if (contactEntity != null && contactEntity.Emails != null)
            {
                // Find the email in the Contact Emails collection
                //
                Email? emailEntity = contactEntity.Emails.Where(e => e.ID == emailModel.ID)
                                                        .FirstOrDefault();
                if (emailEntity != null)
                {
                    emailEntity.UpdatedBy     = this._updatedBy;
                    emailEntity.UpdatedDate   = this._timestamp;
                    emailEntity.EmailUsername = emailModel.EmailUsername;
                    emailEntity.Domain        = emailModel.Domain;
                    emailEntity.Label         = emailModel.Label;

                    contactEntity.UpdatedBy   = this._updatedBy;
                    contactEntity.UpdatedDate = this._timestamp;
                }
            }
        }


        /// <summary>
        /// Updates the phone number objects associated with the <paramref name="contactEntity"/> 
        /// argument with values from the PhoneNumber objects associated with the <paramref name="contactModel"/>
        /// argument.
        /// </summary>
        /// <param name="contactEntity"></param>
        /// <param name="contactModel"></param>
        void UpdatePhoneNumbers(Contact contactEntity, Contact contactModel)
        {
            if (contactModel.PhoneNumbers != null)
            {
                foreach (var phoneNumberModel in contactModel.PhoneNumbers)
                {
                    switch (phoneNumberModel.DbAction)
                    {
                        case Domain.Enums.EntityActions.Add:
                            this.AddPhoneNumberToContact(contactEntity, phoneNumberModel);
                            break;
                        case Domain.Enums.EntityActions.Remove:
                            break;
                        case Domain.Enums.EntityActions.Update: 
                            break;
                    }
                }
            }
        }

        void AddPhoneNumberToContact(Contact contactEntity, PhoneNumber phoneNumberModel)
        {
            if (contactEntity != null)
            {
                if (contactEntity.PhoneNumbers == null)
                {
                    contactEntity.PhoneNumbers  = new List<PhoneNumber>();
                }

                phoneNumberModel.UpdatedDate = phoneNumberModel.CreatedDate = this._timestamp;
                phoneNumberModel.UpdatedBy   = phoneNumberModel.CreatedBy   = this._updatedBy;

                contactEntity.PhoneNumbers.Add(phoneNumberModel);
            }
        }

        void RemovePhoneFromContact(Contact contactEntity, PhoneNumber phoneNumberModel)
        {
            if (contactEntity != null)
            {
                if (contactEntity.PhoneNumbers != null)
                {
                    // Find the email in the Contact Emails collection
                    //
                    PhoneNumber? phoneEntity = contactEntity.PhoneNumbers.Where(e => e.ID == phoneNumberModel.ID)
                                                                         .FirstOrDefault();
                    if (phoneEntity != null)
                    {
                        contactEntity.PhoneNumbers?.Remove(phoneNumberModel);
                    }
                }
            }
        }

        void UpdatePhoneNumberInContact(Contact contactEntity, PhoneNumber phoneNumberModel)
        {
            if (contactEntity != null && contactEntity.PhoneNumbers != null)
            {
                // Find the email in the Contact Emails collection
                //
                PhoneNumber? phoneEntity = contactEntity.PhoneNumbers.Where(e => e.ID == phoneNumberModel.ID)
                                                        .FirstOrDefault();
                if (phoneEntity != null)
                {
                    phoneEntity.UpdatedBy   = this._updatedBy;
                    phoneEntity.UpdatedDate = this._timestamp;
                    phoneEntity.Extension   = phoneNumberModel.Extension;
                    phoneEntity.Prefix      = phoneNumberModel.Prefix;
                    phoneEntity.AreaCode    = phoneNumberModel.AreaCode;
                    phoneEntity.CountryCode = phoneNumberModel.CountryCode;
                    phoneEntity.Label       = phoneNumberModel.Label;
                    phoneEntity.LineNumber  = phoneNumberModel.LineNumber;
                }
            }
        }

        #endregion private
    }
}

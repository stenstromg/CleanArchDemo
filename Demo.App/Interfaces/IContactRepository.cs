﻿using Demo.App.Models;
using Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.App.Interfaces
{
    public interface IContactRepository
    {
        /// <summary>
        /// Creates a new Contact record, including any associated Person, Email, Phone records.
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="updatedBy">
        ///  Username of the user/process responsible for creating the new contact
        /// </param>
        /// <returns></returns>
        Contact CreateContact(Contact contact, string updatedBy = "AUTO");

        /// <summary>
        /// Creates a NEW contact record from the Registration form data
        /// </summary>
        /// <param name="model"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        Contact CreateContactForLogin(UserLoginRegistrationModel model, string author = "AUTO");

        /// <summary>
        /// Permanently deletes the COntact record associated with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteContact(long id);

        /// <summary>
        /// Returns the existing Contact associted with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Contact? GetContactByID(long id);

        /// <summary>
        /// Retusn a filtered list of Contacts
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        List<Contact>? GetContacts(List<Expression<Func<Contact, bool>>>? filters = null);

        /// <summary>
        /// Saves an existing Contact,  including any associated Person, Email, Phone records
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveContact(Contact model, String updatedBy);

    }
}

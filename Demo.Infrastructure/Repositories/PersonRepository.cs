using Demo.App.Interfaces;
using Demo.Domain.Models;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        #region properties

        DemoDbContext _db { get; set; }

        #endregion properties

        #region ctor

        public PersonRepository(DemoDbContext ctx)
        {
            this._db = ctx;
        }

        #endregion ctor

        #region public

        /// <summary>
        /// Creates a new Person record in the People table
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Person CreatePerson(Person person)
        {
            this._db.People.Add(person);
            this._db.SaveChanges();
            return person;
        }

        /// <summary>
        /// Returns a filtered list of Person objects
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<Person>? GetPeople(List<Expression<Func<Person, bool>>> filters)
        {
            var query = from people in this._db.People
                        select people;

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            return query?.ToList();
        }

        /// <summary>
        /// Gets the Person record associated with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Person? GetPersonByID(long id)
        {
            var ret = this._db.People.Where(e=>e.ID == id).Include(e=>e.UserLogins).FirstOrDefault();
            return ret;
        }

        #endregion public
    }
}

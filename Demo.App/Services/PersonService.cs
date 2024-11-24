using Demo.App.Interfaces;
using Demo.Domain.Models;
using System.Linq.Expressions;

namespace Demo.App.Services
{
    public interface IPersonService
    {

        /// <summary>
        /// Creates a new Person record in the People table
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Person? CreatePerson(Person person);

        /// <summary>
        /// Returns a filtered list of Person objects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Person>? GetPeople(List<Expression<Func<Person, bool>>> filters);

        /// <summary>
        /// Gets the Person record associated with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Person? GetPersonById(long id);
    }

    public class PersonService(IPersonRepository _repositiory) : IPersonService
    {
        #region properties

        IPersonRepository _repository { get; set; } = _repositiory;

        #endregion properties

        #region public

        public Person? CreatePerson(Person person)
        {
            var ret = this._repository.CreatePerson(person);
            return ret;
        }

        public Person? GetPersonById(long id)
        {
            var ret = this._repository.GetPersonByID(id);
            return ret;
        }

        public List<Person>? GetPeople(List<Expression<Func<Person, bool>>> filters)
        {
            var ret = this._repository.GetPeople(filters);
            return ret;
        }

        #endregion public
    }
}

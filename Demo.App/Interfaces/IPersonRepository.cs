using Demo.Domain.Models;
using System.Linq.Expressions;

namespace Demo.App.Interfaces
{
    public interface IPersonRepository
    {
        Person CreatePerson(Person person);

        Person? GetPersonByID(long id);

        List<Person>? GetPeople(List<Expression<Func<Person, bool>>> filters);
    }
}

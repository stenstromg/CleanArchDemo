using Demo.Domain.Models;
using System.Linq.Expressions;

namespace Demo.App.Interfaces
{
    public interface IAccountRepository
    {
        Account CreateAccount(Account account);

        Account? GetAccountByID(long id);

        List<Account> GetAccounts(List<Expression<Func<Account, bool>>>? filter = null);
    }
}

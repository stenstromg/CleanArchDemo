using Demo.App.Interfaces;
using Demo.Domain.Models;
using System.Linq.Expressions;

namespace Demo.App.Services
{
    public interface IAccountService
    {

        Account? CreateAccount(Account account);

        Account? GetAccountByID(long accountId);

        List<Account>? GetAccountList(List<Expression<Func<Account, bool>>>? filter = null);
    }

    /// <summary>
    /// Service is injected into the the Presentation layer (WebAPI, BlazorUI, etc.) to 
    /// </summary>
    public class AccountService : IAccountService
    {
        #region properties

        /// <summary>
        ///  Gets/Sets the repository which is defined in the Infrastructure layer since it deals 
        ///  with accessing external information.
        /// </summary>
        IAccountRepository? _repository { get; set; }

        #endregion properties

        #region ctor

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        #endregion ctor

        #region public

        /// <summary>
        /// Creates a new Account record.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Account? CreateAccount(Account account)
        {
            _repository?.CreateAccount(account);
            return account;
        }

        /// <summary>
        /// Returns the Account associated with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account? GetAccountByID(long id)
        {
            Account? ret = _repository?.GetAccountByID(id);
            return ret;
        }

        /// <summary>
        /// Returns an optionally filtered list of accounts.
        /// </summary>
        /// <returns></returns>
        public List<Account>? GetAccountList(List<Expression<Func<Account, bool>>>? filter = null)
        {
            List<Account>? ret = _repository?.GetAccounts();
            return ret;
        }

        #endregion public
    }
}

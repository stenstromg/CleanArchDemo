using Demo.App.Interfaces;
using Demo.Domain.Models;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        #region properties

        private DemoDbContext _db;

        #endregion properties

        #region ctor

        public AccountRepository(DemoDbContext ctx) 
        { 
            _db = ctx;
        }

        #endregion ctor

        #region public

        /// <summary>
        ///  Creates a new Account and returns the <paramref name="account"/> argument, but this time 
        ///  it will be populated with an ID.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Account CreateAccount(Account account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();

            return account;
        }

        /// <summary>
        /// Returns the Account record associaed with the <paramref name="id"/> argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account? GetAccountByID(long id)
        {
            return _db.Accounts.Where(e => e.ID == id).Include(acct => acct.Transactions).FirstOrDefault();
        }

        /// <summary>
        /// Returns a filtered list of Accounts
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Account> GetAccounts(List<Expression<Func<Account, bool>>>? filter = null)
        {
            var query = from account in _db.Accounts
                        select account;

            if (filter != null)
            {
                foreach(var filterExpression in filter) 
                {
                    query = query.Where(filterExpression);
                }
            }

            return query.ToList();
        }

        #endregion public
    }
}

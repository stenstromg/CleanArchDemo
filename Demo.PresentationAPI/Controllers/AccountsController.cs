using Demo.App.Services;
using Demo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IAccountService accountService) : ControllerBase
    {
        #region properties

        IAccountService _service { get; set; } = accountService;

        #endregion properties

        #region public 

        //[HttpGet]
        //public ActionResult<List<Account>?> Get()
        //{
        //    List<Account>? accountList = this._service?.GetAccountList();
        //    return Ok(accountList);
        //}

        //[HttpGet("{id}")]
        //public ActionResult<Account> Get(int id)
        //{
        //    Account? ret = this._service.GetAccountByID(id);

        //    if (ret != null)
        //    {
        //        return Ok(ret);
        //    }
        //    else
        //    {
        //        return NotFound(ret);
        //    }

        //}

        //[HttpPost]
        //public ActionResult<Account?> Post([FromBody] Account account)
        //{
        //    Account? ret = this._service?.CreateAccount(account);
        //    return Ok(ret);
        //}

        //// PUT api/<AccountsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AccountsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        #endregion public
    }
}

using Demo.App.Enums;
using Demo.App.Exceptions;
using Demo.App.Models;
using Demo.App.Models.DTO;
using Demo.App.Services;
using Demo.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginsController(IUserLoginService service) : ControllerBase
    {

        #region properties

        public IUserLoginService _service { get; set; } = service;

        #endregion properties

        #region public

        //[HttpGet, ActionName("GetUserLogins")]
        //[Route("/api/UserAccounts")]
        //public ActionResult<IEnumerable<UserLogin>> GetUserLogins()
        //{
        //    List<UserLogin>? ret = this._service?.GetUserLogins();
        //    return Ok(ret);
        //}

        //[HttpGet, ActionName("GetUserLoginById")]
        //[Route("/api/UserAccount/{id}")]
        //public ActionResult<UserLogin> GetUserLoginById(int id)
        //{
        //    UserLogin? ret = this._service?.GetUserLoginByID(id);

        //    if (ret == null)
        //    {
        //        return NotFound(ret);
        //    }
        //    else
        //    {
        //        return Ok(ret);
        //    }
        //}



        //[HttpPost, ActionName("CreateUserLogin")]
        //[Route("/api/UserAccount/Create")]
        //public ActionResult<UserLogin?> CreateUserLogin([FromBody] UserLogin user)
        //{
        //    UserLogin? ret = this._service?.CreateUserLogin(user);
        //    return Ok(ret);
        //}

        [HttpPost, ActionName("Login")]
        [Route("/api/User/Login")]
        public ActionResult<UserLogin?> Login([FromBody] CredentialsModel creds)
        {
            try
            {
                UserLogin? ret = this._service.GetUserLogin(creds.Password, creds.Username);
                return Ok(ret);
            }
            catch (InvalidCredentialsException credX)
            {
                return StatusCode((int)CustomHttpStatusCodes.InvalidCredentials, new { Message = credX.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, ActionName("Register")]
        [Route("/api/User/Register")]
        public ActionResult<Contact> Register([FromBody] UserLoginRegistrationModel dataModel)
        {
            try
            {
                Contact ret = this._service.RegisterUser(dataModel, "SYSTEM");
                return Ok(ret);
            }
            catch (DuplicateNameException dupeX)
            {
                return StatusCode((int)CustomHttpStatusCodes.DuplicateUserName, new { Message = dupeX.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //[HttpDelete, ActionName("DeleteUserLogin")]
        //[Route("/api/UserAccount")]
        //public ActionResult<UserLogin?> DeleteUserLogin([FromBody] DeleteUserLoginOptions options)
        //{
        //    if (this._service != null && this._service.DeleteUserLogin(options))
        //    {
        //        return Ok(true);
        //    }
        //    else
        //    {
        //        return Problem("Unable to delete Login");
        //    }
        //}


        //// PUT api/<UsersCOntroller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        #endregion public
    }
}

using Demo.App.Models;
using Demo.App.Services;
using Demo.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Data;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(IContactService service) : ControllerBase
    {
        #region properties

        public IContactService _service { get; set; } = service;

        #endregion properties

        #region public

        //// GET: api/<ContactController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpPost, ActionName("GetContactsForUserID")]
        [Route("/api/Contacts")]
        public ActionResult<IEnumerable<Contact>> GetContactsForUserID([FromBody] dynamic dto)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(dto.ToString());

                List<Expression<Func<Contact, bool>>> filters = new();
                long userID = data.id;
                filters.Add(e=>e.UserID == userID);

                var contactList = this._service.GetContacts(filters);
                return Ok(contactList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost, ActionName("Register")]
        [Route("/api/Register")]
        public ActionResult<Contact> Register([FromBody] UserLoginRegistrationModel dataModel)
        {
            try
            {
                Contact ret = this._service.Register(dataModel, "SYSTEM");
                return Ok(ret);
            }
            catch (DuplicateNameException dupeX)
            {
                return StatusCode(601, new { Message = dupeX.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //// PUT api/<ContactController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ContactController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        #endregion public
    }
}

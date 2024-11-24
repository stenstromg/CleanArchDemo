using Demo.App.Models;
using Demo.App.Services;
using Demo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

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

        //// GET api/<ContactController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        [HttpPost, ActionName("Register")]
        [Route("/api/Register")]
        public Contact Register([FromBody] UserLoginRegistrationModel dataModel)
        {
            Contact ret = this._service.Register(dataModel, "SYSTEM");
            return ret;
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

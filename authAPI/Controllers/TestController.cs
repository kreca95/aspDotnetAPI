using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace authAPI.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        [Authorize]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;

            return Ok("hello " + identity.Name);
        }

        // POST: api/Test
        [Authorize(Roles = "admin")]
        public IHttpActionResult Post([FromBody]string value)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return Ok("hello " + "Role"+ string.Join(",",roles.ToList()));
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}

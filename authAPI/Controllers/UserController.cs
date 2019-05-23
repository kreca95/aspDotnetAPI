using authAPI.Models;
using authAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace authAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly Models.AppContext _context;
        public UserController()
        {
            _context = new Models.AppContext();
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Register([FromBody] UserDTO userDTO)
        {

            var check = _context.Users.Any(x => x.UserName == userDTO.UserName);
            if (!check)
            {
                _context.Users.Add(new User { UserName = userDTO.UserName, Password = userDTO.Password });
                _context.SaveChanges();
            }
            else
            {
                return BadRequest("User already exists");
            }

            return Ok(new { name = userDTO.UserName, pass = userDTO.Password });
        }

        [Authorize]
        [Cache(Duration =300)]
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            IEnumerable<User> users;
            users = _context.Users.ToList();

            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(new { user=user.UserName});
        }
    }
}

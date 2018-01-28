using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sysABC.Infrastructure.DTO;
using sysABC.Infrastructure.Services;

namespace sysABC.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/users
        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return _userService.GetAll();
        }

        // GET api/users/email
        [HttpGet("{email}")]
        public UserDto Get(string email)
        {
            return _userService.Get(email.ToLowerInvariant());
        }

        //// POST api/users
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/users/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/users/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sysABC.Infrastructure.Commands.Users;
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
        public async Task<IEnumerable<UserDto>> GetAsync()
        {
            return await _userService.GetAllAsync();
        }

        // GET api/users/email
        [HttpGet("{email}")]
        public async Task<UserDto> GetAsync(string email)
        {
            return await _userService.GetAsync(email.ToLowerInvariant());
        }

        // POST api/users
        // curl http://localhost:5000/api/users/ -X POST -H "Content-Type: application/json" 
        // -d '{"email": "tk@gmail.com", "password": "pass", "nickname": "bl4des", "firstName": "Tomasz", "lastName": "Kisiel"}'
        [HttpPost]
        public async Task Post([FromBody]CreateUser request)
        {
            await _userService.RegisterAsync(request.Email, request.Password, request.NickName, request.FirstName, request.LastName);
        }

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

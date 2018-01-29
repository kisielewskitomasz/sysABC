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
        public async Task<IActionResult> GetAsync()
        {
            var users = await _userService.GetAllAsync();
            if (users == null)
            {
                return NotFound();
            }

            return (Json(users));
        }

        // GET api/users/email
        [HttpGet("{email}")]
        public async Task<IActionResult> GetAsync(string email)
        {
            var user = await _userService.GetAsync(email.ToLowerInvariant());
            if (user == null)
            {
                return NotFound();
            }

            return (Json(user));
        }

        // POST api/users
        // curl http://localhost:5000/api/users/ -X POST -H "Content-Type: application/json" -d '{"email": "tk@gmail.com", "password": "pass", "nickname": "bl4des", "firstName": "Tomasz", "lastName": "Kisiel"}'

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser request)
        {
            await _userService.RegisterAsync(request.Email, request.Password, request.NickName, request.FirstName, request.LastName);

            return Created($"api/users/{request.Email}", new object());
        }
    }
}

using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sysABC.Infrastructure.Commands.Users;
using sysABC.Infrastructure.Services;

namespace sysABC.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        public UsersController(IUserService userService) : base(userService)
        //public UsersController(IUserService userService, JwtSettings jwtSettings) : base(userService, jwtSettings)
        {
        }

        [HttpGet]
        public async Task<IActionResult> BrowseUsersAsync()
        {
            var users = await UserService.BrowseAsync();
            if (users == null)
            {
                return NotFound();
            }

            return (Json(users));
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserAsync(string email)
        {
            var user = await UserService.GetAsync(email.ToLowerInvariant());
            if (user == null)
            {
                return NotFound();
            }

            return (Json(user));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUserAsync(string email)
        {
            var user = await UserService.GetAsync(email.ToLowerInvariant());
            if (user == null)
            {
                return NotFound();
            }

            await UserService.DeleteAsync(email.ToLowerInvariant());

            return Ok();
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> PostRegisterUserAsync([FromBody]CreateUser request)
        {
            await UserService.RegisterAsync(request.Email, request.Password, request.NickName, request.FirstName, request.LastName);

            return Created($"api/users/{request.Email}", new object());
        }

        [Route("token/admin")]
        [HttpPost]
        public async Task<IActionResult> PostLoginUserAsAdminAsync([FromBody]LoginUser request)
        {
            var loginSuccessful = await UserService.LoginAsync(request.Email, request.Password);
            if (loginSuccessful)
                return new ObjectResult(GenerateToken(request.Email, "admin"));
            return BadRequest();
        }

        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> PostLoginUserAsync([FromBody]LoginUser request)
        {
            var loginSuccessful = await UserService.LoginAsync(request.Email, request.Password);
            if (loginSuccessful)
                return new ObjectResult(GenerateToken(request.Email));
            return BadRequest();
        }

        string GenerateToken(string mail, string role = "user")
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, mail),
                new Claim(ClaimTypes.Role, role),
                //new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                //new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(JwtSettings.ExpiryMinutes)).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(60)).ToUnixTimeSeconds().ToString()),
            };
            var token = new JwtSecurityToken(
            new JwtHeader(new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")),
                                         SecurityAlgorithms.HmacSha256)),
            new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
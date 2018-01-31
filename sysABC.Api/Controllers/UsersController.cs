﻿using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sysABC.Infrastructure.Commands.Users;
using sysABC.Infrastructure.Services;
using sysABC.Infrastructure.Settings;

namespace sysABC.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        public UsersController(IUserService userService, JwtSettings jwtSettings) : base(userService, jwtSettings)
        {
        }

        // GET api/users
        //[Authorize]
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

        // GET api/users/email
        [Authorize]
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

        //[Route("update/password")]
        //[Authorize]
        //[HttpPut]
        //public async Task<IActionResult> PutUpdateUserAsync([FromBody]UpdateUserPassword request)
        //{
        //    await UserService.UpdateAsync(request.Email, request.Password);

        //    return Ok();
        //}

        [Authorize]// admin
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

        // POST api/users
        // curl http://localhost:5000/api/users/add -X POST -H "Content-Type: application/json" -d '{"email": "tk@gmail.com", "password": "pass", "nickname": "bl4des", "firstName": "Tomasz", "lastName": "Kisiel"}'

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> PostRegisterUserAsync([FromBody]CreateUser request)
        {
            await UserService.RegisterAsync(request.Email, request.Password, request.NickName, request.FirstName, request.LastName);

            return Created($"api/users/{request.Email}", new object());
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

        string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now).AddMinutes(60).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                                  new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnoprstuwyz")),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

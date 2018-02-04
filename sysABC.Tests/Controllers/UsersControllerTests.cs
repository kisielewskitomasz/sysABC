using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using sysABC.Api;
using sysABC.Infrastructure.DTO;
using sysABC.Infrastructure.Commands.Users;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;

namespace sysABC.Tests.Controllers
{
    public class UsersControllerTests
    {
        readonly TestServer _server;
        readonly HttpClient _client;

        public UsersControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                          .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
            
        [Fact]
        public async Task given_valid_email_user_should_exist()
        {
            var email = "user@systemabc.com";
            var user = await GetUserAsync(email);
            user.Email.ShouldBeEquivalentTo(email);
        }

        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            var email = "NOTdefaultuser@systemabc.com";
            var response = await _client.GetAsync($"api/users/{email}");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
            var request = new CreateUser
            {
                Email = "testuser@systemabc.com",
                Password = "secret",
                NickName = "test",
                FirstName = "Aaron",
                LastName = "Norris"
            };
            var payload = GetPayload(request);
            var response = await _client.PostAsync("api/users/register", payload);
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().ShouldBeEquivalentTo($"api/users/{request.Email}");

            var user = await GetUserAsync(request.Email);
            user.Email.ShouldBeEquivalentTo(request.Email);

            // return back db
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken("in fact it doesn't metter here", "admin"));
            await _client.DeleteAsync($"api/users/{request.Email}");
            _client.DefaultRequestHeaders.Remove("Authorization");
        }

        [Fact]
        public async Task authoize_valid_admin_token_given_valid_email_user_role_should_be_changed()
        {
            var request = new ChangeRoleUser
            {
                Email = "user@systemabc.com",
                Role = "admin"
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken("in fact it doesn't metter here", "admin"));
            var payload = GetPayload(request);
            var response = await _client.PutAsync($"api/users/", payload);
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            _client.DefaultRequestHeaders.Remove("Authorization");

            var user = await GetUserAsync(request.Email);
            user.Role.ShouldBeEquivalentTo("admin");
        }

        [Fact]
        public async Task authoize_valid_admin_token_given_valid_email_user_should_be_removed()
        {
            string email = "user@systemabc.com";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken(email, "admin"));
            var response = await _client.DeleteAsync($"api/users/{email}");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            _client.DefaultRequestHeaders.Remove("Authorization");

            var user = await GetUserAsync(email);
            user.ShouldBeEquivalentTo(null);

            // return back db
            var request = new CreateUser
            {
                Email = "user@systemabc.com",
                Password = "pass",
                NickName = "usernick",
                FirstName = "UserFirst",
                LastName = "UserLast"
            };
            var payload = GetPayload(request);
            await _client.PostAsync("api/users/register", payload);
        }

        async Task<UserDto> GetUserAsync(string email)
        {
            var response = await _client.GetAsync($"api/users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

        async Task<IEnumerable<UserDto>> GetBrowseAsync()
        {
            var response = await _client.GetAsync($"api/users/");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<UserDto>>(responseString);
        }


        static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        string GenerateToken(string mail, string role = "user")
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, mail),
                new Claim(ClaimTypes.Role, role),
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

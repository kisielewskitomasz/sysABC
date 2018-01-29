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
        public async Task get_all_user()
        {
            var users = await GetAllUsersAsync();
            users.ShouldBeEquivalentTo(users);
        }

        [Fact]
        public async Task given_valid_email_user_should_exist()
        {
            var email = "admin@systemabc.com";
            var user = await GetUserAsync(email);
            user.Email.ShouldBeEquivalentTo(email);
        }

        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            var email = "user@systemabc.com";
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
            var response = await _client.PostAsync("api/users/", payload);
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().ShouldBeEquivalentTo($"api/users/{request.Email}");

            var user = await GetUserAsync(request.Email);
            user.Email.ShouldBeEquivalentTo(request.Email);
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await _client.GetAsync($"api/users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

        private async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var response = await _client.GetAsync($"api/users/");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<UserDto>>(responseString);
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}

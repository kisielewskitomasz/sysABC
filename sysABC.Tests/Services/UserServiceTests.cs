using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using sysABC.Core.Repositories;
using sysABC.Core.Models;
using sysABC.Infrastructure.Services;

namespace sysABC.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userService = new UserService(userRepositoryMock.Object);
            await userService.RegisterAsync("adress@email.com", "plaintextpass", "superruserr", "Brad", "Pitt");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}

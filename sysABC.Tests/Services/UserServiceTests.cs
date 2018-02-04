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
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt()).Returns("hash");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(),It.IsAny<string>())).Returns("salt"); 

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object);

            await userService.RegisterAsync("adress@systemabc.com", "plaintextpass", "superruserr", "Brad", "Pitt");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}

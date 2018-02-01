using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sysABC.Infrastructure.DTO;

namespace sysABC.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string nickName, string firstName, string lastName);
        Task<bool> LoginAsync(string email, string password);
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task<bool> DeleteAsync(string email);
    }
}

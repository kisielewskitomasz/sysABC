using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sysABC.Core.Models;

namespace sysABC.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> BrowseAsync();
        Task AddAsync(User user);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Guid id);
    }
}

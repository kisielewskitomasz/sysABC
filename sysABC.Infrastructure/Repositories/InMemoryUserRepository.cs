using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sysABC.Core.Models;
using sysABC.Core.Repositories;

namespace sysABC.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        static ISet<User> _users = new HashSet<User>();

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task<IEnumerable<User>> BrowseAsync()
            => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            User user = await GetAsync(id);
            //#todo: null checking
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Guid id)
        {
            await Task.CompletedTask;
        }
    }
}

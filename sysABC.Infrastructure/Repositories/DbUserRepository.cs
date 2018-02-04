using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sysABC.Core.Models;
using sysABC.Core.Repositories;
using sysABC.Infrastructure.SQL;

namespace sysABC.Infrastructure.Repositories
{
    public class DbUserRepository : IUserRepository
    {
        readonly SysABCContext _context;

        public DbUserRepository(SysABCContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(Guid id)
        => await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
        => await _context.Users.SingleOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

        public async Task<IEnumerable<User>> BrowseAsync()
        => await _context.Users.ToListAsync();

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, string role)
        {
            var user = await GetAsync(id);
            user.SetRole(role);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}


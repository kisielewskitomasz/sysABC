using System;
using System.Collections.Generic;
using sysABC.Core.Models;

namespace sysABC.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(Guid id);
        User Get(string email);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Remove(Guid id);
        void Update(Guid id);
    }
}

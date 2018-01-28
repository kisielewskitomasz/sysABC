using System;
using System.Collections.Generic;
using System.Linq;
using sysABC.Core.Models;
using sysABC.Core.Repositories;

namespace sysABC.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        static ISet<User> _users = new HashSet<User>()
        {
            new User("admin@systemabc.com", "Admin1234", "dontuseplaintextpass", "admin", "admin", "admin", "Admin"),
            new User("John.Doe@systemabc.com", "JonDoeSecretPass", "dontuseplaintextpass", "John", "Doe", "doedoe"),
            new User("Jack.Daniels@systemabc.com", "NotGoodPassword", "dontuseplaintextpass", "Jack", "Daniels", "jack18"),
        };

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User Get(Guid id)
        {
            return _users.Single(x => x.Id == id);
        }

        public User Get(string email)
        {
            return _users.Single(x => x.Email == email.ToLowerInvariant());
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public void Remove(Guid id)
        {
            User user = Get(id);
            //#todo: null checking
            _users.Remove(user);
        }

        public void Update(Guid id)
        {
        }
    }
}

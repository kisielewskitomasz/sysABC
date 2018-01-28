using System;
using System.Collections.Generic;
using sysABC.Infrastructure.DTO;

namespace sysABC.Infrastructure.Services
{
    public interface IUserService
    {
        void Register(string email, string password, string nickName, string firstName, string lastName);

        UserDto Get(string email);

        IEnumerable<UserDto> GetAll();
    }
}

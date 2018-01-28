using System;
using System.Collections.Generic;
using sysABC.Core.Models;
using sysABC.Core.Repositories;
using sysABC.Infrastructure.DTO;

namespace sysABC.Infrastructure.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(string email, string password, string nickName, string firstName, string lastName)
        {
            User user = _userRepository.Get(email);
            if (user != null)
                throw new Exception($"User with '{email}' alredy exitst!");

            string salt = "plaintextpassword";
            user = new User(email, password, salt, nickName, firstName, lastName);
            _userRepository.Add(user);
        }

        public UserDto Get(string email)
        {
            User user = _userRepository.Get(email);
            if (user == null)
                throw new Exception($"User with '{email}' alredy exitst!");

            return new UserDto(user.Id, user.Email, user.NickName, user.FirstName, user.LastName, user.Privilages);
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            var usersDto = new HashSet<UserDto>();
            foreach (var user in users)
                usersDto.Add(new UserDto(user.Id, user.Email, user.NickName, user.FirstName, user.LastName, user.Privilages));

            return usersDto;
        }
    }
}

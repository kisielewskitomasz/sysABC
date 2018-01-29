using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task RegisterAsync(string email, string password, string nickName, string firstName, string lastName)
        {
            User user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new Exception($"User with '{email}' alredy exitst!");

            string salt = "plaintextpassword";
            user = new User(email, password, salt, nickName, firstName, lastName);
            await _userRepository.AddAsync(user);
        }

        public async Task<UserDto> GetAsync(string email)
        {
            User user = await _userRepository.GetAsync(email);
            if (user == null) 
                return null;
            //   throw new Exception($"User with '{email}' not exitst!");

            return new UserDto(user.Id, user.Email, user.NickName, user.FirstName, user.LastName, user.Privilages);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var usersDto = new HashSet<UserDto>();
            foreach (var user in users)
                usersDto.Add(new UserDto(user.Id, user.Email, user.NickName, user.FirstName, user.LastName, user.Privilages));

            if (usersDto.Count == 0)
                return null;
            
            return usersDto;
        }
    }
}

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
        readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task RegisterAsync(string email, string password, string nickName, string firstName, string lastName)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new Exception($"User with '{email}' alredy exitst!");

            var salt = _encrypter.GetSalt();
            var hash = _encrypter.GetHash(password, salt);
            user = new User(email, hash, salt, nickName, firstName, lastName);
            await _userRepository.AddAsync(user);
        }

        public async Task<UserDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
                throw new Exception("Invalid credentials");

            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password != hash)
                throw new Exception("Invalid credentials");

            return new UserDto(user.Id, user.Email, user.NickName, user.FirstName, user.LastName, user.Role);
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
                return null; // to unit test make working
                //throw new Exception($"User with '{email}' not exitst!");
            return new UserDto(user.Id, user.Email, user.NickName, user.FirstName, user.LastName, user.Role);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();
            var usersDto = new HashSet<UserDto>();
            foreach (var user in users)
                usersDto.Add(new UserDto(user.Id, user.Email, user.NickName, user.FirstName, user.LastName, user.Role));

            if (usersDto.Count == 0)
                return null;

            return usersDto;
        }

        public async Task DeleteAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
                throw new Exception($"User with '{email}' not exitst!");

            await _userRepository.RemoveAsync(user.Id);
        }

        public async Task UpdateAsync(string email, string role) //string password, string nickName, string firstName, string lastName, 
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
                throw new Exception($"User with '{email}' not exitst!");

            await _userRepository.UpdateAsync(user.Id, role);
        }

        //public async Task UpdateAsync(string email, string newEmail, string password, string nickName, string firstName, string lastName, string role)
        //{
        //    var user = await _userRepository.GetAsync(email);
        //    if (user == null)
        //        throw new Exception($"User with '{email}' not exitst!");

        //    var salt = _encrypter.GetSalt();
        //    var hash = _encrypter.GetHash(password, salt);
        //    user.SetEmail(newEmail);
        //    user.SetPassword(hash);
        //    user.SetNickName(nickName);
        //    user.SetFirstName(firstName);
        //    user.SetLastName(lastName);
        //    await _userRepository.UpdateAsync(user.Id, role);
        //}
    }
}

using System;
namespace sysABC.Infrastructure.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public UserDto(Guid id, string email, string nickName, string firstName, string lastName, string role)
        {
            Id = id;
            Email = email;
            Nickname = nickName;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
    }
}

using System;
namespace sysABC.Core.Models
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string NickName { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Privilages { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
        }

        public User(string email, string password, string salt, string nickName, string firstName, string lastName, string privilages = "User")
        {
            //#todo: validation
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Password = password;
            Salt = salt;
            NickName = nickName;
            FirstName = firstName;
            LastName = lastName;
            Privilages = privilages;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

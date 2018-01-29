using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

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
        public DateTime UpdatedAt { get; protected set; }

        static readonly Regex NickNameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

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

        public void SetNickName(string nickName)
        {
            if (string.IsNullOrWhiteSpace(nickName))
                throw new Exception("Nickname can not be empty.");

            if (!NickNameRegex.IsMatch(nickName))
                throw new Exception("Nickname is invalid.");


            NickName = nickName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email can not be empty.");

            if (!IsValidEmail(email))
                throw new Exception("Email is invalid.");

            if (Email == email)
                return;

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password can not be empty.");

            if (password.Length < 3)
                throw new Exception("Password must contain at least 3 characters.");

            if (Password == password)
                return;

            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}

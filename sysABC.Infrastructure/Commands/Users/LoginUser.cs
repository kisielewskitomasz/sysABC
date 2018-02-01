using System;
namespace sysABC.Infrastructure.Commands.Users
{
    public class LoginUser : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginUser()
        {
        }
    }
}

using System;
namespace sysABC.Infrastructure.Commands.Users
{
    public class UpdateUserPassword : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UpdateUserPassword()
        {
        }
    }
}

using System;
namespace sysABC.Infrastructure.Commands.Users
{
    public class ChangeRoleUser
    {
        public string Email { get; set; }
        public string Role { get; set; }

        public ChangeRoleUser()
        {
        }
    }
}

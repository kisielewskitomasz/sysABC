using System;
using System.Threading.Tasks;

namespace sysABC.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        readonly IUserService _userService;

        public DataInitializer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task SeedAsync()
        {
            var user1 = await _userService.GetAsync("admin@systemabc.com");
            if (user1 != null)
                await _userService.RegisterAsync("admin@systemabc.com", "adminpass", "admin", "AdminFirst", "AdminLast");
            var user2 = await _userService.GetAsync("user@systemabc.com");
            if (user2 != null)
                await _userService.RegisterAsync("user@systemabc.com", "userpass", "user1", "John", "Doe");
        }
    }
}

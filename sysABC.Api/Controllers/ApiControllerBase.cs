using System;
using Microsoft.AspNetCore.Mvc;
using sysABC.Infrastructure.Services;
using sysABC.Infrastructure.Settings;

namespace sysABC.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        protected readonly IUserService UserService;
        protected readonly JwtSettings JwtSettings;

        public ApiControllerBase(IUserService userService, JwtSettings jwtSettings)
        {
            UserService = userService;
            JwtSettings = jwtSettings;
        }
    }
}

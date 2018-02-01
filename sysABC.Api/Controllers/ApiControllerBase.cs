using System;
using Microsoft.AspNetCore.Mvc;
using sysABC.Infrastructure.Services;

namespace sysABC.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        protected readonly IUserService UserService;

        public ApiControllerBase(IUserService userService)
        {
            UserService = userService;
        }
    }
}

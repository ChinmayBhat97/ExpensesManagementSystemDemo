using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        [ApiController]
        [Route("api/[controller]")]
        public class AuthenticateController : ControllerBase
        {

            private IUserService userTableService;

            public AuthenticateController(IUserService _userTableService)
            {
                userTableService = _userTableService;
            }

            [HttpPost]
            public AuthenticationViewModel Authenticate(string userName,string password)
            {
                return userTableService.Authenticate(userName, password);
            }
        }
    }
}

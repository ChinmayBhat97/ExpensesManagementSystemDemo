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

            private IUserService userService;
            
            public AuthenticateController(IUserService _userService)
            {
                userService = _userService;
            }

            [HttpPost]
            public AuthenticationViewModel Authenticate(string EmployeeCode, string password)
            {
                return userService.Authenticate(EmployeeCode, password);
            }
        }
    }
}

using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class LoginController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public LoginController(IConfiguration _configuration)
        {

            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }
        private readonly SignInManager<IdentityUser> signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {

            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var signInResult = await signInManager.PasswordSignInAsync(user.EmployeeCode,
                user.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}

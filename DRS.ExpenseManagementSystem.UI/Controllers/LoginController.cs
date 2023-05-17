using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient client;
        private readonly SignInManager<IdentityUser> signInManager;

        public LoginController(IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;

            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
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

            if (signInResult.Succeeded)
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


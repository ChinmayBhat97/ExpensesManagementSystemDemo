using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient client;


        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
            

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
        public async Task<IActionResult> Login(User data)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress + $"Authenticate?userName={HttpUtility.UrlEncode(data.EmployeeCode)}&password={HttpUtility.UrlEncode(data.Password)}", null);

                if (response.IsSuccessStatusCode)
                {
                    var authResponse = JsonConvert.DeserializeObject<Auth>(await response.Content.ReadAsStringAsync());
                    if (authResponse.isAuthenticated)
                    {
                        HttpContext.Session.SetString("UserRole", authResponse.employeeDetails.Designation);
                        HttpContext.Session.SetInt32("UserId", authResponse.userDetails.Id);
                        HttpContext.Session.SetString("UserName", authResponse.employeeDetails.FirstName + " " + authResponse.employeeDetails.LastName);
                        HttpContext.Session.SetString("UserFullName", authResponse.employeeDetails.FirstName + " " + authResponse.employeeDetails.LastName);

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Incorrect Username or Password");
            }

            return View("Index", data);
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("Index");
        }
    }
}


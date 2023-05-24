using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DRS.ExpenseManagementSystem.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{

    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient client;
        private readonly ExpensesManagementSystem_UpdatedContext expensesManagementSystem_UpdatedContext;

        public LoginController(IConfiguration configuration, ExpensesManagementSystem_UpdatedContext _expensesManagementSystem_UpdatedContext)
        {
            this.configuration = configuration;
            this.expensesManagementSystem_UpdatedContext = _expensesManagementSystem_UpdatedContext;

            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(10)
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;

            if (claimsUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string EmployeeCode, string Password)
        {
            var checkUser = expensesManagementSystem_UpdatedContext.Users.Any(u => u.EmployeeCode == EmployeeCode && u.Password == Password);

            if (checkUser)
            {
                var user = expensesManagementSystem_UpdatedContext.Users.Single(u => u.EmployeeCode == EmployeeCode);
                var empID = user.Id;
                var ID_Employee = expensesManagementSystem_UpdatedContext.Employees.Single(x => x.EmpId == empID).Id;
                var designation = expensesManagementSystem_UpdatedContext.Employees.Single(x => x.EmpId == empID).Designation;

                TempData["logged_empID"] = ID_Employee;

                List<Claim> userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, EmployeeCode),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                    
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = false,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["Validate message"] = "Incorrect user credentials! Please check and enter again.";
            return View("Index");
        }




        // [HttpGet("EmployeeCode/{employeeCode},Password/{password}")]
        //[HttpPost]
        //public async Task<IActionResult> Login(string EmployeeCode, string Password)
        //{

        //    var checkUser = expensesManagementSystem_UpdatedContext.Users.Any(u => u.EmployeeCode==EmployeeCode);
        //    var checkPswd = expensesManagementSystem_UpdatedContext.Users.Any(u => u.Password==Password);

        //    if ((checkUser && checkPswd)==true)
        //    {
        //        var empID = expensesManagementSystem_UpdatedContext.Users.Where(v => v.EmployeeCode==EmployeeCode).Select(k => k.Id).SingleOrDefault();
        //        var ID_Employee = expensesManagementSystem_UpdatedContext.Employees.Where(x => x.EmpId==empID).Select(x => x.Id).SingleOrDefault();
        //        var designation = expensesManagementSystem_UpdatedContext.Employees.Where(x => x.EmpId==empID).Select(x => x.Designation).SingleOrDefault();
        //        TempData["logged_empID"] = ID_Employee;
        //        List<Claim> user1 = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, EmployeeCode),
        //            new Claim(ClaimTypes.Role, designation)

        //        };



        //        ClaimsIdentity claimsIdentity = new ClaimsIdentity(user1, CookieAuthenticationDefaults.AuthenticationScheme);

        //        AuthenticationProperties properties = new AuthenticationProperties()
        //        {
        //           AllowRefresh= false,

        //        };

        //         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //         new ClaimsPrincipal(claimsIdentity), properties);

        //        return RedirectToAction("Index", "Home");


        //    }
        //    ViewData["Validate message"]= "Incorrect user credentials!.Kindly check and enter again.";
        //    return View("Index");
        //}


        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }


        }


    }
}

//if (ModelState.IsValid)
//{
//    HttpResponseMessage response = await client.PostAsync(client.BaseAddress + $"Auth?employeeCode={HttpUtility.UrlEncode(EmployeeCode)}&password={HttpUtility.UrlEncode(Password)}",null);

//    if (response.IsSuccessStatusCode)
//    {
//        var authResponse = JsonConvert.DeserializeObject<Auth>(await response.Content.ReadAsStringAsync());
//        if (authResponse.isAuthenticated)
//        {
//            HttpContext.Session.SetString("UserRole", authResponse.employeeDetails.Designation);
//            HttpContext.Session.SetInt32("UserId", authResponse.userDetails.Id);
//            HttpContext.Session.SetString("UserName", authResponse.employeeDetails.FirstName + " " + authResponse.employeeDetails.LastName);
//            HttpContext.Session.SetString("UserFullName", authResponse.employeeDetails.FirstName + " " + authResponse.employeeDetails.LastName);

//            return RedirectToAction("Index", "Home");
//        }
//    }

//    ModelState.AddModelError(string.Empty, "Incorrect Username or Password");
//}


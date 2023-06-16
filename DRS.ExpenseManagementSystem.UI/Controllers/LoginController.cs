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
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;

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
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress + $"Authenticate?EmployeeCode={HttpUtility.UrlEncode(EmployeeCode)}&Password={HttpUtility.UrlEncode(Password)}", null);
              //  HttpResponseMessage response = await client.PostAsync(client.BaseAddress + $"Authenticate?userName={HttpUtility.UrlEncode(data.userName)}&password={HttpUtility.UrlEncode(data.password)}", null);
                if (response.IsSuccessStatusCode)
                {
                    var authResponse = JsonConvert.DeserializeObject<AuthenticationViewModel>(await response.Content.ReadAsStringAsync());
                    if (authResponse.IsAuthenticated==true)
                    {
                        if(authResponse.userDetails.IsActive==true && authResponse.userDetails.IsAccountLocked==false)
                        {
                            TempData["EmployeeCode"]= authResponse.userDetails.EmployeeCode;
                            TempData["EmpID"]= authResponse.userDetails.EmpId;
                            TempData["LoggedDesignation"]= authResponse.userDetails.Designation;
                            TempData["LoggedFirstName"]= authResponse.userDetails.FirstName;
                            TempData["Role"]=authResponse.userDetails.Role.ToString();
                            TempData["Department"]= authResponse.userDetails.deptName;
                            TempData["DepID"]=authResponse.userDetails.DeptId;
                            TempData["FirstName"] = authResponse.userDetails.FirstName;
                            // TempData["projectID"]=authResponse.userDetails.pID.ToString();
                            TempData.Keep();

                            

                            // HttpContext.Session.SetInt32("Roles",authResponse.userDetails.Role);
                           // TempData["Success"]=$"{authResponse.userDetails.EmployeeCode} You have successfully logged in to application.";
                           
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            
                            TempData["Message"]="Your are not active User or you dont have access to this application.";
                            TempData["MessageInst"]="Kindly contact Admin or HR Manager for further information.";
                            return RedirectToAction("Index", "Login");
                        }
                        //HttpContext.Session.SetString("UserRole", authResponse.userDetails.RoleName);
                        //HttpContext.Session.SetInt32("RoleId", authResponse.userDetails.RoleID);
                        //HttpContext.Session.SetInt32("UserId", authResponse.userDetails.UserId);
                        //HttpContext.Session.SetString("UserName", authResponse.userDetails.FirstName + " " + authResponse.userDetails.LastName);
                        //HttpContext.Session.SetString("UserFullName", authResponse.userDetails.FirstName + " " + authResponse.userDetails.LastName);
                    }
                    else
                    {
                        TempData["MessageInfo"] = "Kindly check your credentials and Try again/ Or you are not registered.";
                        //ViewBag.NotFound="Kindly check your credentials and Try again.";
                        return RedirectToAction("Index", "Login");
                    }

                }
                else
                {
                    TempData["Check"]="You are not registered.";
                    return RedirectToAction("Index", "Login");
                }
               

               // ModelState.AddModelError(string.Empty, "Incorrect Username or Password");
            }

            return View("Index");
        }

      

        
        public async Task<IActionResult> Logout()
        {
            
            TempData.Clear();
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Index", "Login");
           
        }


    }
}


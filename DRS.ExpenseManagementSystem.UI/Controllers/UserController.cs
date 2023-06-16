
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using UserViewModel = DRS.ExpenseManagementSystem.UI.Models.UserViewModel;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class UserController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public UserController(IConfiguration _configuration)
        {

            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }


        // [Authorize(Roles = "4")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "User");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<UserViewModel>>(responseContent);
                var filteredModel = model?.Count > 0 ? model.Where(e => e.IsActive == true).ToList() : new();
                return View(filteredModel);
            }
            else
            {
                return View();
            }
        }

        // [Authorize(Roles = "4")]
        [HttpGet("User/Create")]
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage responseCreateUser = await client.GetAsync(client.BaseAddress + $"User");
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            if (responseCreateUser.IsSuccessStatusCode && responseDepartmentList.IsSuccessStatusCode)
            {
                var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
                var departmentSelectList = new List<SelectListItem>();
                foreach (var department in departmentList)
                {
                    departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
                }
                ViewBag.departmentList = departmentSelectList;


                return View();
            }
            return View();
        }

        // [Authorize(Roles = "4")]
        [HttpPost("User/Create")]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {

            var myContent = JsonConvert.SerializeObject(userViewModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"User/", byteContent);
            if (createNewClaim.IsSuccessStatusCode)
            {
                return RedirectToAction("CreateEmployee", "Employee", new { @EmployeeCode = userViewModel.EmployeeCode });
            }
            return BadRequest("Please check the credentials and try again!");


        }

        // [Authorize(Roles = "4")]
        [HttpGet("User/EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id)
        {
            HttpResponseMessage responseEditUser = await client.GetAsync(client.BaseAddress + $"User/{id}");
            var EditUser = JsonConvert.DeserializeObject<UserViewModel>(await responseEditUser.Content.ReadAsStringAsync());
            return View(EditUser);
        }

        //  [Authorize(Roles = "4")]
        [HttpPost("User/EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id, UserViewModel userViewModel)
        {

            var myContent = JsonConvert.SerializeObject(userViewModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"User/{userViewModel.Id}", byteContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return BadRequest("I apologize, but it seems that an user with those credentials already exists in our system. Please try again with different credentials");
        }

        // [Authorize(Roles = "4")]
        [HttpGet("User/DetailsUser/{id}")]
        public async Task<IActionResult> DetailsByUserID(int id)
        {
            HttpResponseMessage responseDetailsUser = await client.GetAsync(client.BaseAddress + $"User/{id}");
            var detailsUser = JsonConvert.DeserializeObject<UserViewModel>(await responseDetailsUser.Content.ReadAsStringAsync());
            return View(detailsUser);
        }

        //  [Authorize(Roles = "4")]
        [HttpGet]
        public async Task<IActionResult> GetByRole(int role)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"User/{role}");
            return View();
        }





    }
}

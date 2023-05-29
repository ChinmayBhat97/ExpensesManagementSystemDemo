
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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


        [Authorize(Roles = "4")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "User");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<UserViewModel>>(responseContent);
                return View(model);
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
            return View();

        }

       // [Authorize(Roles = "4")]
        [HttpPost("User/Create")]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(userViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"User", byteContent);

                return RedirectToAction("Index");
            }
            return View(userViewModel);
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
            if (ModelState.IsValid)
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
            }

            return View(userViewModel);
        }

       // [Authorize(Roles = "4")]
        [HttpGet("User/DetailsUser/{id}")]
        public async Task<IActionResult> DetailsByUserID(int id)
        {
            HttpResponseMessage responseDetailsUser = await client.GetAsync(client.BaseAddress + $"User/{id}");
            var detailsUser = JsonConvert.DeserializeObject<User>(await responseDetailsUser.Content.ReadAsStringAsync());
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

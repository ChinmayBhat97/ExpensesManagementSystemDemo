
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;


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

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseUser = await client.GetAsync(client.BaseAddress + $"User");
            return View();
        }

        [HttpGet("User/Create")]
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage responseCreateUser = await client.GetAsync(client.BaseAddress + $"User");
            return View();

        }


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

        [HttpGet("ExpenseClaim/Edit")]
        public async Task<IActionResult> EditByAdmin()
        {
            HttpResponseMessage responseCreateClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim");
            return View();

        }

        [HttpPost("User/Edit")]
        public async Task<IActionResult> EditByAdmin(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var myContent = JsonConvert.SerializeObject(userViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"User/{userViewModel.Id}", byteContent);
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }
    }
}

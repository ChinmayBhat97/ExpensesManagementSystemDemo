using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class EmployeeController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public EmployeeController(IConfiguration _configuration)
        {

            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "Employee");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(responseContent);
                return View(model);
            }
            else
            {

                return View();
            }
        }

        [HttpGet("Employee/CreateEmployee")]
        public async Task<IActionResult> CreateEmployeeAsync()
        {
            HttpResponseMessage responseCreateEmployee = await client.GetAsync(client.BaseAddress + $"Employee");
            return View();
        }

        [HttpPost("Employee/CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(employeeViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewEmployee = await client.PostAsync(client.BaseAddress + $"Employee", byteContent);

                return RedirectToAction("Index");
            }

            return View(employeeViewModel);
        }

        //[HttpGet("User/EditEmployee/{id}")]
        //public async Task<IActionResult> EditEmployee(int id)
        //{
        //    HttpResponseMessage responseEditEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
        //    return View();
        //}

        [HttpGet("Employee/EditEmployee/{id}")]
        public async Task<IActionResult> EditEmployee(int id)
        {
            HttpResponseMessage responseEditEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
            var EditEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>(await responseEditEmployee.Content.ReadAsStringAsync());
            return View(EditEmployee);
        }

        [HttpPost("Employee/EditEmployee/{id}")]
        public async Task<IActionResult> EditEmployee(int id, EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(employeeViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"Employee/{employeeViewModel.Id}", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(employeeViewModel);
        }

        [HttpGet("Employee/DetailsEmployee/{id}")]
        public async Task<IActionResult> DetailsByEmployeeID(int id)
        {
            HttpResponseMessage responseDetailsEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
            var detailsEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>(await responseDetailsEmployee.Content.ReadAsStringAsync());
            return View(detailsEmployee);
        }
    }
}

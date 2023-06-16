using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using DepartmentViewModel = DRS.ExpenseManagementSystem.UI.Models.DepartmentViewModel;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class DepartmentController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public DepartmentController(IConfiguration _configuration)
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
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "Department");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(responseContent);
                return View(model);
            }
            else
            {

                return View();
            }
        }



        //  [Authorize(Roles = "4")]
        [HttpGet("Department/CreateDepartment")]
        public async Task<IActionResult> CreateDepartmentAsync()
        {
            HttpResponseMessage responseCreateDepartment = await client.GetAsync(client.BaseAddress + $"Department");
            return View();
        }


        // [Authorize(Roles = "4")]
        [HttpPost("Department/CreateDepartment")]
        public async Task<IActionResult> CreateDepartment(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {

                var myContent = JsonConvert.SerializeObject(departmentViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewDepartment = await client.PostAsync(client.BaseAddress + $"Department", byteContent);
                if (createNewDepartment.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return BadRequest("DPlease check the credentials and try again");
            }

            return View(departmentViewModel);
        }

        // [Authorize(Roles = "4")]
        [HttpGet("Department/EditDepartment/{id}")]
        public async Task<IActionResult> EditDepartment(int id)
        {
            HttpResponseMessage responseEditDepartment = await client.GetAsync(client.BaseAddress + $"Department/{id}");
            var EditDepartment = JsonConvert.DeserializeObject<DepartmentViewModel>(await responseEditDepartment.Content.ReadAsStringAsync());
            return View(EditDepartment);
        }

        //  [Authorize(Roles = "4")]
        [HttpPost("Department/EditDepartment/{id}")]
        public async Task<IActionResult> EditDepartment(int id, DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(departmentViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"Department/{departmentViewModel.Id}", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return BadRequest("You dont have the permission to edit this data");
            }

            return View(departmentViewModel);
        }

        // [Authorize(Roles = "4")]
        [HttpGet("Department/DetailsDepartment/{id}")]
        public async Task<IActionResult> DetailsByDepartmentID(int id)
        {
            HttpResponseMessage responseDetailsDepartment = await client.GetAsync(client.BaseAddress + $"Department/{id}");
            var detailsDepartment = JsonConvert.DeserializeObject<Department>(await responseDetailsDepartment.Content.ReadAsStringAsync());
            return View(detailsDepartment);
        }
    }
}

using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                Timeout = TimeSpan.FromMinutes(20)
            };
        }

       // [Authorize(Roles = "4")]
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

       // [Authorize(Roles = "4")]
        [HttpGet("Employee/CreateEmployee")]
        public async Task<IActionResult> CreateEmployeeAsync()
        {
            HttpResponseMessage responseCreateEmployee = await client.GetAsync(client.BaseAddress + $"Employee");
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            if (responseCreateEmployee.IsSuccessStatusCode && responseDepartmentList.IsSuccessStatusCode)
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

      //  [Authorize(Roles = "4")]
        [HttpPost("Employee/CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            employee.CreatedAt=DateTime.Now;
            employee.FirstName= employee.FirstName.ToUpper();
            employee.LastName= employee.LastName.ToUpper();
            if (ModelState.IsValid)
            {
          
                var myContent = JsonConvert.SerializeObject(employee);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
               // HttpResponseMessage createNewEmployee = await client.PostAsync(client.BaseAddress + $"Employee", byteContent);
                  HttpResponseMessage createNewEmp = await client.PostAsync(client.BaseAddress + $"Employee", byteContent);

                return RedirectToAction("Index");
            }

            return View(employee);
        }

        //[HttpGet("User/EditEmployee/{id}")]
        //public async Task<IActionResult> EditEmployee(int id)
        //{
        //    HttpResponseMessage responseEditEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
        //    return View();
        //}

       // [Authorize(Roles = "4")]
        [HttpGet("Employee/EditEmployee/{id}")]
        public async Task<IActionResult> EditEmployee(int id)
        {
            HttpResponseMessage responseEditEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            if (responseEditEmployee.IsSuccessStatusCode && responseDepartmentList.IsSuccessStatusCode)
            {
                var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
                var departmentSelectList = new List<SelectListItem>();
                foreach (var department in departmentList)
                {
                    departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
                }
                ViewBag.departmentList = departmentSelectList;
                var EditEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>(await responseEditEmployee.Content.ReadAsStringAsync());
                return View(EditEmployee);
                
            }
            return View();
        }


       // [Authorize(Roles = "4")]
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

      //  [Authorize(Roles = "4")]
        [HttpGet("Employee/DetailsEmployee/{id}")]
        public async Task<IActionResult> DetailsByEmployeeID(int id)
        {
            HttpResponseMessage responseDetailsEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            if (responseDetailsEmployee.IsSuccessStatusCode && responseDepartmentList.IsSuccessStatusCode)
            {
                var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
                var departmentSelectList = new List<SelectListItem>();
                foreach (var department in departmentList)
                {
                    departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
                }
                ViewBag.departmentList = departmentSelectList;
                var detailsEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>(await responseDetailsEmployee.Content.ReadAsStringAsync());
                return View(detailsEmployee);
            }
            return View();
        }
    }
}

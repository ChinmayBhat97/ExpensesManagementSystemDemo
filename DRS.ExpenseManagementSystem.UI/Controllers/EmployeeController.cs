using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using EmployeeViewModel = DRS.ExpenseManagementSystem.UI.Models.EmployeeViewModel;

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
                var filteredModel = model?.Count > 0 ? model.Where(e => e.Emp.IsActive == true).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

        // [Authorize(Roles = "4")]
        [HttpGet("Employee/CreateEmployee/{EmployeeCode}")]
        public async Task<IActionResult> CreateEmployee(string EmployeeCode)
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

                // Get ID by passing EmployeeCode
                HttpResponseMessage responseUserNew = await client.PostAsync(client.BaseAddress + $"User/{EmployeeCode}", null);
                if (responseUserNew.IsSuccessStatusCode)
                {
                    // var authResponse = JsonConvert.DeserializeObject<AuthenticationViewModel>(await response.Content.ReadAsStringAsync());
                    var userList = JsonConvert.DeserializeObject<AuthenticationViewModel>(await responseUserNew.Content.ReadAsStringAsync());
                    TempData["EmpId"] = userList.userDetails.Id;
                    TempData["EmployeeCode"] = userList.userDetails.EmployeeCode;
                    TempData.Keep();
                    return View();
                }

                //var userSelectList = new List<SelectListItem>();
                //foreach (var user in userList)
                //{
                //    userSelectList.Add(new SelectListItem(user.EmployeeCode, user.Id.ToString()));
                //}
                //ViewBag.userList = userSelectList;

                return View();
            }
            return View();
        }



        //  [Authorize(Roles = "4")]
        [HttpPost("Employee/CreateEmployee/{EmployeeCode}")]
        public async Task<IActionResult> CreateEmployee(string EmployeeCode, Employee employee)
        {
            employee.EmpId = Convert.ToInt32(TempData["EmpId"]);
            employee.CreatedAt = DateTime.Now;
            var myContent = JsonConvert.SerializeObject(employee);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // HttpResponseMessage createNewEmployee = await client.PostAsync(client.BaseAddress + $"Employee", byteContent);
            HttpResponseMessage createNewEmp = await client.PostAsync(client.BaseAddress + $"Employee", byteContent);
            if (createNewEmp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest("I apologize, but it seems that an employee with those credentials already exists in our system. Please try again with different credentials");
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

            // Get Department
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

                // Get employee code
                HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"User");
                var userList = JsonConvert.DeserializeObject<List<User>>(await responseUserList.Content.ReadAsStringAsync());
                var userSelectList = new List<SelectListItem>();
                foreach (var user in userList)
                {
                    userSelectList.Add(new SelectListItem(user.EmployeeCode, user.Id.ToString()));
                }
                ViewBag.userList = userSelectList;
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
                return BadRequest("I apologize, but it seems that an employee with those credentials already exists in our system. Please try again with different credentials");
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



//HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"User/");
//var userList = JsonConvert.DeserializeObject<List<User>>(await responseUserList.Content.ReadAsStringAsync());
//var userSelectList = new List<SelectListItem>();
//foreach (var user in userList)
//{
//    userSelectList.Add(new SelectListItem(user.EmployeeCode, user.Id.ToString()));
//}
//ViewBag.userList = userSelectList;

//return View();
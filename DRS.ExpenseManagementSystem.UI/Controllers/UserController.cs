
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.UI.Models;
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

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"User");
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

        [HttpGet("User/Edit")]
        public async Task<IActionResult> EditByAdmin()
        {
            HttpResponseMessage responseEditClaim = await client.GetAsync(client.BaseAddress + $"User");
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

        public async Task<IActionResult> IndexProject()
        {
            HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"Project");
            return View();
        }

        [HttpGet("User/CreateProject")]
        public async Task<IActionResult> CreateProjectAsync()
        {
            HttpResponseMessage responseCreateProject = await client.GetAsync(client.BaseAddress + $"Project");
            return View();
        }

        [HttpPost("User/CreateProject")]
        public async Task<IActionResult> CreateProject(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(projectViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewProject = await client.PostAsync(client.BaseAddress + $"Project", byteContent);

                return RedirectToAction("Index");
            }

            return View(projectViewModel);
        }

        [HttpGet("User/EditProject/{id}")]
        public async Task<IActionResult> EditProject(int id)
        {
            HttpResponseMessage responseEditProject = await client.GetAsync(client.BaseAddress + $"Project");
            return View();
        }

        [HttpPost("User/EditProject/{id}")]
        public async Task<IActionResult> EditProject(int id, ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(projectViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"Project/{projectViewModel.Id}", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(projectViewModel);
        }


        [HttpGet("User/CreateDepartment")]
        public async Task<IActionResult> CreateDepartmentAsync()
        {
            HttpResponseMessage responseCreateDepartment = await client.GetAsync(client.BaseAddress + $"Department");
            return View();
        }

        [HttpPost("User/CreateDepartment")]
        public async Task<IActionResult> CreateDepartment(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(departmentViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewDepartment = await client.PostAsync(client.BaseAddress + $"Project", byteContent);

                return RedirectToAction("Index");
            }

            return View(departmentViewModel);
        }

        [HttpGet("User/EditDepartment/{id}")]
        public async Task<IActionResult> EditDepartment(int id)
        {
            HttpResponseMessage responseEditDepartment = await client.GetAsync(client.BaseAddress + $"Department");
            return View();
        }

        [HttpPost("User/EditDepartment/{id}")]
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
            }

            return View(departmentViewModel);
        }

        [HttpGet("User/CreateEmployee")]
        public async Task<IActionResult> CreateEmployeeAsync()
        {
            HttpResponseMessage responseCreateEmployee = await client.GetAsync(client.BaseAddress + $"Employee");
            return View();
        }

        [HttpPost("User/CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(employeeViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewDepartment = await client.PostAsync(client.BaseAddress + $"Employee", byteContent);

                return RedirectToAction("Index");
            }

            return View(employeeViewModel);
        }

        [HttpGet("User/EditEmployee/{id}")]
        public async Task<IActionResult> EditEmployee(int id)
        {
            HttpResponseMessage responseEditEmployee = await client.GetAsync(client.BaseAddress + $"Employee");
            return View();
        }

        [HttpPost("User/EditEmployee/{id}")]
        public async Task<IActionResult> EditDepartment(int id, EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(employeeViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"Department/{employeeViewModel.Id}", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(employeeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetByEmployeeId(int empID)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{empID}");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByDeptId(int deptID)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{deptID}");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByFirstName(string fName)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{fName}");
            return View();
        }
    }
}

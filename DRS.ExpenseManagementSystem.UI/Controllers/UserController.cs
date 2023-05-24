
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

        [HttpGet("User/EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id)
        {
            HttpResponseMessage responseEditUser = await client.GetAsync(client.BaseAddress + $"User/{id}");
            var EditUser = JsonConvert.DeserializeObject<UserViewModel>(await responseEditUser.Content.ReadAsStringAsync());
            return View(EditUser);
        }

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

        [HttpGet("User/DetailsUser/{id}")]
        public async Task<IActionResult> DetailsByUserID(int id)
        {
            HttpResponseMessage responseDetailsUser = await client.GetAsync(client.BaseAddress + $"User/{id}");
            var detailsUser = JsonConvert.DeserializeObject<User>(await responseDetailsUser.Content.ReadAsStringAsync());
            return View(detailsUser);
        }

        [HttpGet]
        public async Task<IActionResult> GetByRole(int role)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"User/{role}");
            return View();
        }


        //public async Task<IActionResult> IndexProject()
        //{
        //    HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"Project");
        //    return View();
        //}

        //        [HttpGet]
        //        public async Task<IActionResult> IndexProject()
        //        {
        //            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "Project");
        //            if (responseHomePage.IsSuccessStatusCode)
        //            {
        //                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
        //                var model = JsonConvert.DeserializeObject<List<ProjectViewModel>>(responseContent);
        //                return View(model);
        //            }
        //            else
        //            {

        //                return View();
        //            }
        //        }

        //        [HttpGet("User/CreateProject")]
        //        public async Task<IActionResult> CreateProjectAsync()
        //        {
        //            HttpResponseMessage responseCreateProject = await client.GetAsync(client.BaseAddress + $"Project");
        //            return View();
        //        }

        //        [HttpPost("User/CreateProject")]
        //        public async Task<IActionResult> CreateProject(ProjectViewModel projectViewModel)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                var myContent = JsonConvert.SerializeObject(projectViewModel);
        //                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //                var byteContent = new ByteArrayContent(buffer);
        //                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //                HttpResponseMessage createNewProject = await client.PostAsync(client.BaseAddress + $"Project", byteContent);

        //                return RedirectToAction("IndexProject");
        //                //var myContent = JsonConvert.SerializeObject(departmentViewModel);
        //                //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //                //var byteContent = new ByteArrayContent(buffer);
        //                //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //                //HttpResponseMessage createNewDepartment = await client.PostAsync(client.BaseAddress + $"Department", byteContent);

        //                //return RedirectToAction("IndexDepartment");
        //            }

        //            return View(projectViewModel);
        //        }

        //        // Edited by Chinmay
        //        [HttpGet("User/EditProject/{id}")]
        //        public async Task<IActionResult> EditProject(int id)
        //        {
        //            HttpResponseMessage responseEditProject = await client.GetAsync(client.BaseAddress + $"Project/{id}");
        //            var EditProject = JsonConvert.DeserializeObject<ProjectViewModel>(await responseEditProject.Content.ReadAsStringAsync());
        //            return View(EditProject);
        //        }

        //        [HttpPost("User/EditProject/{id}")]
        //        public async Task<IActionResult> EditProject(int id, ProjectViewModel projectViewModel)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                var myContent = JsonConvert.SerializeObject(projectViewModel);
        //                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //                var byteContent = new ByteArrayContent(buffer);
        //                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"Project/{projectViewModel.Id}", byteContent);
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    return RedirectToAction("IndexProject");
        //                }
        //            }

        //            return View(projectViewModel);
        //        }

        //        [HttpGet("User/DetailsProject/{id}")]
        //        public async Task<IActionResult> DetailsByProjectID(int id)
        //        {
        //            HttpResponseMessage responseDetailsProject = await client.GetAsync(client.BaseAddress + $"Project/{id}");
        //            var detailsProject = JsonConvert.DeserializeObject<Project>(await responseDetailsProject.Content.ReadAsStringAsync());
        //            return View(detailsProject);
        //        }

        //        [HttpGet]
        //        public async Task<IActionResult> IndexDepartment()
        //        {
        //            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "Department");
        //            if (responseHomePage.IsSuccessStatusCode)
        //            {
        //                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
        //                var model = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(responseContent);
        //                return View(model);
        //            }
        //            else
        //            {

        //                return View();
        //            }
        //        }


        //        [HttpGet("User/CreateDepartment")]
        //        public async Task<IActionResult> CreateDepartmentAsync()
        //        {
        //            HttpResponseMessage responseCreateDepartment = await client.GetAsync(client.BaseAddress + $"Department");
        //            return View();
        //        }

        //        [HttpPost("User/CreateDepartment")]
        //        public async Task<IActionResult> CreateDepartment(DepartmentViewModel departmentViewModel)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                var myContent = JsonConvert.SerializeObject(departmentViewModel);
        //                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //                var byteContent = new ByteArrayContent(buffer);
        //                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //                HttpResponseMessage createNewDepartment = await client.PostAsync(client.BaseAddress + $"Department", byteContent);

        //                return RedirectToAction("IndexDepartment");
        //            }

        //            return View(departmentViewModel);
        //        }


        //        [HttpGet("User/EditDepartment/{id}")]
        //        public async Task<IActionResult> EditDepartment(int id)
        //        {
        //            HttpResponseMessage responseEditDepartment = await client.GetAsync(client.BaseAddress + $"Department/{id}");
        //            var EditDepartment = JsonConvert.DeserializeObject<DepartmentViewModel>(await responseEditDepartment.Content.ReadAsStringAsync());
        //            return View(EditDepartment);
        //        }

        //        [HttpPost("User/EditDepartment/{id}")]
        //        public async Task<IActionResult> EditDepartment(int id, DepartmentViewModel departmentViewModel)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                var myContent = JsonConvert.SerializeObject(departmentViewModel);
        //                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //                var byteContent = new ByteArrayContent(buffer);
        //                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"Department/{departmentViewModel.Id}", byteContent);
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    return RedirectToAction("IndexDepartment");
        //                }
        //            }

        //            return View(departmentViewModel);
        //        }

        //        [HttpGet("User/DetailsDepartment/{id}")]
        //        public async Task<IActionResult> DetailsByDepartmentID(int id)
        //        {
        //            HttpResponseMessage responseDetailsDepartment = await client.GetAsync(client.BaseAddress + $"Department/{id}");
        //            var detailsDepartment = JsonConvert.DeserializeObject<Department>(await responseDetailsDepartment.Content.ReadAsStringAsync());
        //            return View(detailsDepartment);
        //        }

        //        //[HttpGet]
        //        //public async Task<IActionResult> IndexEmployee()
        //        //{
        //        //    HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "Employee");
        //        //    if (responseHomePage.IsSuccessStatusCode)
        //        //    {
        //        //        var responseContent = await responseHomePage.Content.ReadAsStringAsync();
        //        //        var model = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(responseContent);
        //        //        return View(model);
        //        //    }
        //        //    else
        //        //    {

        //        //        return View();
        //        //    }
        //        //}

        //        //[HttpGet("User/CreateEmployee")]
        //        //public async Task<IActionResult> CreateEmployeeAsync()
        //        //{
        //        //    HttpResponseMessage responseCreateEmployee = await client.GetAsync(client.BaseAddress + $"Employee");
        //        //    return View();
        //        //}

        //        //[HttpPost("User/CreateEmployee")]
        //        //public async Task<IActionResult> CreateEmployee(EmployeeViewModel employeeViewModel)
        //        //{
        //        //    if (ModelState.IsValid)
        //        //    {
        //        //        var myContent = JsonConvert.SerializeObject(employeeViewModel);
        //        //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //        //        var byteContent = new ByteArrayContent(buffer);
        //        //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        //        HttpResponseMessage createNewEmployee = await client.PostAsync(client.BaseAddress + $"Employee", byteContent);

        //        //        return RedirectToAction("IndexEmployee");
        //        //    }

        //        //    return View(employeeViewModel);
        //        //}

        //        ////[HttpGet("User/EditEmployee/{id}")]
        //        ////public async Task<IActionResult> EditEmployee(int id)
        //        ////{
        //        ////    HttpResponseMessage responseEditEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
        //        ////    return View();
        //        ////}

        //        //[HttpGet("User/EditEmployee/{id}")]
        //        //public async Task<IActionResult> EditEmployee(int id)
        //        //{
        //        //    HttpResponseMessage responseEditEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
        //        //    var EditEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>(await responseEditEmployee.Content.ReadAsStringAsync());
        //        //    return View(EditEmployee);
        //        //}

        //        //[HttpPost("User/EditEmployee/{id}")]
        //        //public async Task<IActionResult> EditEmployee(int id, EmployeeViewModel employeeViewModel)
        //        //{
        //        //    if (ModelState.IsValid)
        //        //    {
        //        //        var myContent = JsonConvert.SerializeObject(employeeViewModel);
        //        //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //        //        var byteContent = new ByteArrayContent(buffer);
        //        //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        //        HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"Employee/{employeeViewModel.Id}", byteContent);
        //        //        if (response.IsSuccessStatusCode)
        //        //        {
        //        //            return RedirectToAction("IndexEmployee");
        //        //        }
        //        //    }

        //        //    return View(employeeViewModel);
        //        //}

        //        //[HttpGet("User/DetailsEmployee/{id}")]
        //        //public async Task<IActionResult> DetailsByEmployeeID(int id)
        //        //{
        //        //    HttpResponseMessage responseDetailsEmployee = await client.GetAsync(client.BaseAddress + $"Employee/{id}");
        //        //    var detailsEmployee = JsonConvert.DeserializeObject<Employee>(await responseDetailsEmployee.Content.ReadAsStringAsync());
        //        //    return View(detailsEmployee);
        //        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByEmployeeId(int empID)
        //{
        //    HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{empID}");
        //    return View();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByDeptId(int deptID)
        //{
        //    HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{deptID}");
        //    return View();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByFirstName(string fName)
        //{
        //    HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{fName}");
        //    return View();
        //}
    }
}

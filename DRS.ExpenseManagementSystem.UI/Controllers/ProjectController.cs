using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ProjectController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public ProjectController(IConfiguration _configuration)
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
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "Project");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ProjectViewModel>>(responseContent);
                return View(model);
            }
            else
            {

                return View();
            }
        }

        // [Authorize(Roles = "4")]
        [HttpGet("Project/CreateProject")]
        public async Task<IActionResult> CreateProjectAsync()
        {
            int role = 2;
            HttpResponseMessage responseCreateProject = await client.GetAsync(client.BaseAddress + $"Project");
            HttpResponseMessage responseUserList = await client.PostAsync(client.BaseAddress + $"Employee/{role}", null);
            var userList = JsonConvert.DeserializeObject<List<Employee>>(await responseUserList.Content.ReadAsStringAsync());
            var userSelectList = new List<SelectListItem>();
           // var filteredUsers = userList.Where(user => user.Emp != null && user.Emp.Role == 2);
            foreach (var user in userList)
            {
                // Retrieve the employee ID from the Employee table using the FK in the User table
                int employeeId = user.Id;

                // Retrieve the employee code from the User table
                string employeeCode = user.Emp.EmployeeCode;

                // Create a SelectListItem with the employee code and ID
                //userSelectList.Add(new SelectListItem(employeeCode, employeeId.ToString()));
                userSelectList.Add(new SelectListItem(employeeCode, employeeId.ToString()));
            }
            ViewBag.userList = userSelectList;
            return View();
        }

        // [Authorize(Roles = "4")]
        [HttpPost("Project/CreateProject")]
        public async Task<IActionResult> CreateProject(Project projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(projectViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewProject = await client.PostAsync(client.BaseAddress + "Project", byteContent);
                if (createNewProject.IsSuccessStatusCode)
                {
                    
                    return RedirectToAction("Index");
                }
                return BadRequest("Project already exists!");
            }

            return View(projectViewModel);
        }

        //  [Authorize(Roles = "4")]
        [HttpGet("Project/EditProject/{id}")]
        public async Task<IActionResult> EditProject(int id)
        {
            int role = 2;
            HttpResponseMessage responseEditProject = await client.GetAsync(client.BaseAddress + $"Project/{id}");
            HttpResponseMessage responseUserList = await client.PostAsync(client.BaseAddress + $"Employee/{role}", null);
            var userList = JsonConvert.DeserializeObject<List<Employee>>(await responseUserList.Content.ReadAsStringAsync());
            var userSelectList = new List<SelectListItem>();
            foreach (var user in userList)
            {
                // Retrieve the employee ID from the Employee table using the FK in the User table
                int employeeId = user.Id;

                // Retrieve the employee code from the User table
                string employeeCode = (user.Emp.EmployeeCode); // .Where(user.Emp.Role==2))

                // Create a SelectListItem with the employee code and ID
                userSelectList.Add(new SelectListItem(employeeCode, employeeId.ToString()));

            }
            ViewBag.userList = userSelectList;
            var EditProject = JsonConvert.DeserializeObject<Project>(await responseEditProject.Content.ReadAsStringAsync());
            return View(EditProject);
        }

        // [Authorize(Roles = "4")]
        [HttpPost("Project/EditProject/{id}")]
        public async Task<IActionResult> EditProject(int id, Project projectViewModel)
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
                return BadRequest("I apologize, but it seems that an project with those credentials already exists in our system. Please try again with different credentials");
            }

            return View(projectViewModel);
        }

        //  [Authorize(Roles = "4")]
        [HttpGet("Project/DetailsProject/{id}")]
        public async Task<IActionResult> DetailsByProjectID(int id)
        {
            //to show employeecode
            HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"Employee");
            var userList = JsonConvert.DeserializeObject<List<Employee>>(await responseUserList.Content.ReadAsStringAsync());
            var userSelectList = new List<SelectListItem>();
            foreach (var user in userList)
            {
                userSelectList.Add(new SelectListItem(user.Emp.EmployeeCode, user.Id.ToString()));
            }
            ViewBag.userList = userSelectList;

            HttpResponseMessage responseDetailsProject = await client.GetAsync(client.BaseAddress + $"Project/{id}");
            var detailsProject = JsonConvert.DeserializeObject<ProjectViewModel>(await responseDetailsProject.Content.ReadAsStringAsync());
            return View(detailsProject);
        }
    }
}


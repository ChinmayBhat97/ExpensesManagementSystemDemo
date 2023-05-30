using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            HttpResponseMessage responseCreateProject = await client.GetAsync(client.BaseAddress + $"Project");
            return View();
        }

       // [Authorize(Roles = "4")]
        [HttpPost("Project/CreateProject")]
        public async Task<IActionResult> CreateProject(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(projectViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewProject = await client.PostAsync(client.BaseAddress + "Project", byteContent);

                return RedirectToAction("Index");
            }

            return View(projectViewModel);
        }

      //  [Authorize(Roles = "4")]
        [HttpGet("Project/EditProject/{id}")]
        public async Task<IActionResult> EditProject(int id)
        {
            HttpResponseMessage responseEditProject = await client.GetAsync(client.BaseAddress + $"Project/{id}");
            var EditProject = JsonConvert.DeserializeObject<ProjectViewModel>(await responseEditProject.Content.ReadAsStringAsync());
            return View(EditProject);
        }

       // [Authorize(Roles = "4")]
        [HttpPost("Project/EditProject/{id}")]
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

      //  [Authorize(Roles = "4")]
        [HttpGet("Project/DetailsProject/{id}")]
        public async Task<IActionResult> DetailsByProjectID(int id)
        {
            HttpResponseMessage responseDetailsProject = await client.GetAsync(client.BaseAddress + $"Project/{id}");
            var detailsProject = JsonConvert.DeserializeObject<Project>(await responseDetailsProject.Content.ReadAsStringAsync());
            return View(detailsProject);
        }
    }
}


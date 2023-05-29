using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Hosting;
using Syncfusion.EJ2.Diagrams;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Security.Claims;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ManagerController : Controller
    {
       private readonly IConfiguration configuration;
        private readonly IHostingEnvironment webHostEnvironment;
        private readonly HttpClient client;

        public ManagerController(IConfiguration _configuration, IHostingEnvironment _webHostEnvironment)
        {
            this.configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }

        //[HttpGet("Manager/Index/{statusId}")]
        //public async Task<IActionResult> Index(int statusId)
        //{
        //    ExpenseClaim expenseClaim = new ExpenseClaim();
        //    expenseClaim.Status = 1;
        //    HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{statusId}");
        //    return View();
        //}

        //[Authorize(Roles = "2")]

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);
                
                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 1).ToList() : new() ;
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }



        //[HttpGet("Manager/EditByManager/{id}")]
        //public async Task<IActionResult> EditByManager(int id)
        //{
        //    HttpResponseMessage responseEditUser = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
        //    var EditByManager = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseEditUser.Content.ReadAsStringAsync());
        //    return View(EditByManager);
        //}

        //[HttpPost("Manager/EditByManager/{id}")]
        //public async Task<IActionResult> EditByManager(int id, ExpenseClaim expenseClaim)
        //{
        //    if (ModelState.IsValid)
        //    {
                
        //        var myContent = JsonConvert.SerializeObject(expenseClaim);
        //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //        var byteContent = new ByteArrayContent(buffer);
        //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"ExpenseClaim/{expenseClaim.Id}", byteContent);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(expenseClaim);
        //}


        [HttpGet("ExpenseClaim/EditByManager/{id}")]
        public async Task<IActionResult> EditByManager(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());
            //var detailsProject = JsonConvert.DeserializeObject<Project>(await responseDetailsClaim.Content.ReadAsStringAsync());
            //if(detailsClaim.ProjectId == detailsProject.Id )

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            detailsClaim.IndividualExpenditures = individualExpenditures;

            //drop down to show department names
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            //drop down to show project names
            HttpResponseMessage responseProjectList = await client.GetAsync(client.BaseAddress + $"Project");
            var projectList = JsonConvert.DeserializeObject<List<Project>>(await responseProjectList.Content.ReadAsStringAsync());
            var projectSelectList = new List<SelectListItem>();
            foreach (var project in projectList)
            {
                projectSelectList.Add(new SelectListItem(project.Title, project.Id.ToString()));
            }
            ViewBag.projectList = projectSelectList;

            //dropdown to show categories
            HttpResponseMessage responseCategoryList = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            var categoryList = JsonConvert.DeserializeObject<List<ExpenseCategory>>(await responseCategoryList.Content.ReadAsStringAsync());
            var categorySelectList = new List<SelectListItem>();
            foreach (var category in categoryList)
            {
                categorySelectList.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
            ViewBag.categoryList = categorySelectList;


            return View(detailsClaim);
        }


        [HttpPost("ExpenseClaim/EditByManager/{id}")]
        public async Task<IActionResult> EditByManager(ExpenseClaimViewModel expenseClaimViewModel)
        {
            //if (ModelState.IsValid)
            //{
            string wwwPath = this.webHostEnvironment.WebRootPath;
            string contentPath = this.webHostEnvironment.ContentRootPath;

            string path = Path.Combine(this.webHostEnvironment.WebRootPath, "ExpenseProof");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile expenseProofs in expenseClaimViewModel.ExpenseProof)
            {
                string fileName = Path.GetFileName(expenseProofs.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    expenseProofs.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }

            }

            expenseClaimViewModel.Status = 1;

            // Save ExpenseClaim
            var expenseClaimContent = JsonConvert.SerializeObject(expenseClaimViewModel);
            var expenseClaimBuffer = System.Text.Encoding.UTF8.GetBytes(expenseClaimContent);
            var expenseClaimByteContent = new ByteArrayContent(expenseClaimBuffer);
            expenseClaimByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            expenseClaimViewModel.IndividualExpenditures.ForEach(n => n.IsApproved = false);

            HttpResponseMessage createNewClaim = await client.PutAsync(client.BaseAddress + $"ExpenseClaim/", expenseClaimByteContent);

            return RedirectToAction("Index");

        }




        [HttpGet("Manager/DetailsManager/{id}")]
        public async Task<IActionResult> DetailsByManager(int id)
        {
            HttpResponseMessage responseDetailsManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsManager = JsonConvert.DeserializeObject<ExpenseClaim>(await responseDetailsManager.Content.ReadAsStringAsync());
            return View(detailsManager);
        }


        [HttpGet]
        public async Task<IActionResult> GetByClaimId(int ID)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{ID}");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetByEmployeeId(int EmpId)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{EmpId}");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{startDate},{endDate}");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByClaimRequestDate(DateTime requestDate)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{requestDate}");
            return View();
        }
    }
}

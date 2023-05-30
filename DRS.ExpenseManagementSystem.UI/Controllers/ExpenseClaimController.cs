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
  
    public class ExpenseClaimController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment webHostEnvironment;
        private readonly HttpClient client;

        public ExpenseClaimController(IConfiguration _configuration, IHostingEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }


        // GET method for index 
        
        [HttpGet("ExpenseClaim/Index")]
        public async Task<IActionResult> Index()
        {
            //int empID = Convert.ToInt32(TempData["logged_empID"]);
            //int EmpID = empID;

           int EmpId = Convert.ToInt32(TempData["EmpID"]);
           TempData.Keep();
            //bool ID = int.TryParse(LoggedID, out int result);

            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + $"ExpenseClaim?EmpId={EmpId}");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);
                return View(model);
            }
            else
            {

                return View();
            }
        }

      
        // GET method to create expense claim
        
        [HttpGet("ExpenseClaim/Create")]
        public async Task<IActionResult> Create()
        {
            //dropdown to show department
            HttpResponseMessage responsedepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responsedepartmentList.Content.ReadAsStringAsync());
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

            var indiExp = new IndividualExpenditureViewModel();
            var expenseClaimViewModel = new ExpenseClaimViewModel();
            expenseClaimViewModel.IndividualExpenditures = new List<IndividualExpenditureViewModel>();
            expenseClaimViewModel.IndividualExpenditures.Add(indiExp);
            return View(expenseClaimViewModel);
        }

       
        [HttpPost("ExpenseClaim/Create")]
        public async Task<IActionResult> Create(ExpenseClaimViewModel expenseClaimViewModel)
        {
            //if (ModelState.IsValid)
            //{
            string wwwPath = this.webHostEnvironment.WebRootPath;

            string path = Path.Combine(wwwPath, "ExpenseProof");
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
            expenseClaimViewModel.IndividualExpenditures.ForEach(n => n.IsApproved = false);

            // Save ExpenseClaim
            var expenseClaimContent = JsonConvert.SerializeObject(expenseClaimViewModel);
            var expenseClaimBuffer = System.Text.Encoding.UTF8.GetBytes(expenseClaimContent);
            var expenseClaimByteContent = new ByteArrayContent(expenseClaimBuffer);
            expenseClaimByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            

            HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"ExpenseClaim/", expenseClaimByteContent);

            return RedirectToAction("Index");

        }


       
        [HttpGet("ExpenseClaim/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var editClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            editClaim.IndividualExpenditures = individualExpenditures;

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


            return View(editClaim);
        }

       
        [HttpPost("ExpenseClaim/Edit/{id}")]
        public async Task<IActionResult> Edit(ExpenseClaimViewModel expenseClaimViewModel)
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


        
        [HttpGet("ExpenseClaim/DetailsByClaimID/{claimId}")]
        public async Task<IActionResult> DetailsByClaimID(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{claimId}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            detailsClaim.IndividualExpenditures = individualExpenditures;
            string wwwPath = this.webHostEnvironment.WebRootPath;
            return View(detailsClaim);
        }


       
    }
}

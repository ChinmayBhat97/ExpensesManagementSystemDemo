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
using System.Data;

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
                Timeout = TimeSpan.FromMinutes(15)
            };
        }


        [HttpGet("ExpenseClaim/Index")]
        public async Task<IActionResult> Index()
        {
            int empId = Convert.ToInt32(TempData["EmpID"]);
            TempData.Keep();

            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/Details/{empId}", HttpCompletionOption.ResponseHeadersRead);
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                //// Filter the claims based on matching EmpId
                //var filteredClaims = model;
                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 1).ToList() : new();
                return View(filteredModel);
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
            expenseClaimViewModel.ClaimRequestDate = DateTime.Now;
            expenseClaimViewModel.IndividualExpenditures.Add(indiExp);
            return View(expenseClaimViewModel);
        }


        [HttpPost("ExpenseClaim/Create")]
        public async Task<IActionResult> Create(ExpenseClaimViewModel expenseClaimViewModel)
        {
            int role = Convert.ToInt32(TempData["Role"]);

            TempData.Keep();

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
            expenseClaimViewModel.ClaimRequestDate = DateTime.Now;

            if (role == 3)
            {
                expenseClaimViewModel.Status = 6;
                expenseClaimViewModel.ManagerRemarks = "Not Applicable";
                expenseClaimViewModel.ManagerApprovedOn = DateTime.Now;
            }

            //expenseClaimViewModel.Status = 1;
            expenseClaimViewModel.EmpId = Convert.ToInt32(TempData["EmpID"]);
            expenseClaimViewModel.DeptId = Convert.ToInt32(TempData["DepID"]);
            expenseClaimViewModel.IndividualExpenditures.ForEach(n => n.IsApproved = false);
            expenseClaimViewModel.IndividualExpenditures.ForEach(n => n.IsDelete = 0);

            // Save ExpenseClaim
            var expenseClaimContent = JsonConvert.SerializeObject(expenseClaimViewModel);
            var expenseClaimBuffer = System.Text.Encoding.UTF8.GetBytes(expenseClaimContent);
            var expenseClaimByteContent = new ByteArrayContent(expenseClaimBuffer);
            expenseClaimByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"ExpenseClaim/", expenseClaimByteContent);

            if (createNewClaim.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest("Please check the credentials and try again");

        }


        [HttpGet("ExpenseClaim/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());
            
            //detailsClaim.Status = 1;
            if (detailsClaim.Status != 1)
            {
                //Return a string message when the status is not 1
                return BadRequest("EXPENSE CLAIM CANNOT BE EDITTED!.");
            }
            else
            {
                
                // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
                HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
                var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());
                detailsClaim.ClaimRequestDate = DateTime.Now;
                detailsClaim.IndividualExpenditures = individualExpenditures;

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

                //dropdown to show department
                HttpResponseMessage responsedepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
                var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responsedepartmentList.Content.ReadAsStringAsync());
                var departmentSelectList = new List<SelectListItem>();
                foreach (var department in departmentList)
                {
                    departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
                }
                ViewBag.departmentList = departmentSelectList;


                return View(detailsClaim);
            }

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
            if (expenseClaimViewModel.ExpenseProof != null && expenseClaimViewModel.ExpenseProof.Count > 0)
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
            expenseClaimViewModel.IndividualExpenditures.ForEach(x => x.ClaimId = expenseClaimViewModel.Id);
            expenseClaimViewModel.EmpId = Convert.ToInt32(TempData["EmpID"]);
            // Save ExpenseClaim
            var expenseClaimContent = JsonConvert.SerializeObject(expenseClaimViewModel);
            var expenseClaimBuffer = System.Text.Encoding.UTF8.GetBytes(expenseClaimContent);
            var expenseClaimByteContent = new ByteArrayContent(expenseClaimBuffer);
            expenseClaimByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await client.PatchAsync(client.BaseAddress + $"ExpenseClaim/", expenseClaimByteContent);

            //Save Individual Expenditure
            var expenditureContent = JsonConvert.SerializeObject(expenseClaimViewModel.IndividualExpenditures);
            var expenditureBuffer = System.Text.Encoding.UTF8.GetBytes(expenditureContent);
            var expenditureByteContent = new ByteArrayContent(expenditureBuffer);
            expenditureByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage editClaim = await client.PutAsync(client.BaseAddress + $"IndividualExpenditure/", expenditureByteContent);

            //if (editClaim.IsSuccessStatusCode)
            //{
                return RedirectToAction("Index");
            //}
            //return BadRequest("Please check the credentials and try again");

        }



        [HttpGet("ExpenseClaim/DetailsByClaimID/{claimId}")]
        public async Task<IActionResult> DetailsByClaimID(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{claimId}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

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

            //dropdown to show department
            HttpResponseMessage responsedepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responsedepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            //drop down to show status
            HttpResponseMessage responseStatusList = await client.GetAsync(client.BaseAddress + $"ClaimStatus");
            var statusList = JsonConvert.DeserializeObject<List<ClaimStatus>>(await responseStatusList.Content.ReadAsStringAsync());
            var statusSelectList = new List<SelectListItem>();
            foreach (var status in statusList)
            {
                statusSelectList.Add(new SelectListItem(status.Name, status.Id.ToString()));
            }
            ViewBag.statusList = statusSelectList;

            detailsClaim.IndividualExpenditures = individualExpenditures;
            string wwwPath = this.webHostEnvironment.WebRootPath;
            return View(detailsClaim);
        }

        // Get all that are approved

        [HttpGet]
        public async Task<IActionResult> IndexApproved()
        {
            int empId = Convert.ToInt32(TempData["EmpID"]);
            TempData.Keep();
            //int EmpId = Convert.ToInt32(TempData["EmpID"]);

            //  HttpResponseMessage responseApproved = await client.PostAsync(client.BaseAddress + $"ExpenseClaim/{EmpId}", null) ;
            HttpResponseMessage responseApproved = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/Details/{empId}");
            if (responseApproved.IsSuccessStatusCode)
            {
                var responseContent = await responseApproved.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 2 || e.Status == 3 /*&& (e.EmpId==EmpId)*/).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }


        [HttpGet("ExpenseClaim/DetailsApproved/{claimId}")]
        public async Task<IActionResult> DetailsApproved(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{claimId}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

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

            //dropdown to show department
            HttpResponseMessage responsedepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responsedepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            detailsClaim.IndividualExpenditures = individualExpenditures;
            string wwwPath = this.webHostEnvironment.WebRootPath;
            return View(detailsClaim);
        }

        // Get all that are rejected

        [HttpGet]
        public async Task<IActionResult> IndexRejected()
        {
            int empId = Convert.ToInt32(TempData["EmpID"]);
            TempData.Keep();

            // HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            HttpResponseMessage responseRejected = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/Details/{empId}");
            if (responseRejected.IsSuccessStatusCode)
            {
                var responseContent = await responseRejected.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 4 || e.Status == 5).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

        [HttpGet("ExpenseClaim/DetailsRejected/{claimId}")]
        public async Task<IActionResult> DetailsRejected(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{claimId}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

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

            //dropdown to show department
            HttpResponseMessage responsedepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responsedepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            detailsClaim.IndividualExpenditures = individualExpenditures;
            string wwwPath = this.webHostEnvironment.WebRootPath;
            return View(detailsClaim);
        }
    }
}

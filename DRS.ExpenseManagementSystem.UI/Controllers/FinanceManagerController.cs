﻿
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
   // [Authorize(Roles = "3")]
    public class FinanceManagerController : Controller
    {
        readonly IConfiguration configuration;
        private readonly IHostingEnvironment webHostEnvironment;
        readonly HttpClient client;

        public FinanceManagerController(IConfiguration _configuration, IHostingEnvironment _webHostEnvironment)
        {

            this.configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    ExpenseClaim expenseClaim = new ExpenseClaim();
        //    expenseClaim.Status = 5;
        //    HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{expenseClaim.Status}");
        //    return View(model);
        //}

      // [Authorize(Roles = "3")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 2 || e.Status==6).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

      //  [Authorize(Roles = "3")]
        [HttpGet("FinanceManager/DetailsByClaimID/{claimId}")]
        public async Task<IActionResult> DetailsByClaimID(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaim>(await responseDetailsClaim.Content.ReadAsStringAsync());
            return View(detailsClaim);
        }

       // [Authorize(Roles = "3")]
        [HttpGet("ExpenseClaim/EditByFinanceManager/{id}")]
        public async Task<IActionResult> EditbyFinanceManager(int id)
        {
            int role = Convert.ToInt32(TempData["Role"]);
            var firstName = TempData["FirstName"];
            TempData.Keep();

            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            detailsClaim.FinanceManagerApprovedOn = DateTime.Now;
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
            //drop down to show department names
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            //dropdown to show categories
            HttpResponseMessage responseCategoryList = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            var categoryList = JsonConvert.DeserializeObject<List<ExpenseCategory>>(await responseCategoryList.Content.ReadAsStringAsync());
            var categorySelectList = new List<SelectListItem>();
            foreach (var category in categoryList)
            {
                categorySelectList.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
            ViewBag.categoryList = categorySelectList;

            //drop down to show status
            HttpResponseMessage responseStatusList = await client.PostAsync(client.BaseAddress + $"ClaimStatus/{role}",null);
            var statusList = JsonConvert.DeserializeObject<List<ClaimStatus>>(await responseStatusList.Content.ReadAsStringAsync());
            var statusSelectList = new List<SelectListItem>();
            foreach (var status in statusList)
            {
                statusSelectList.Add(new SelectListItem(status.Name, status.Id.ToString()));
            }
            ViewBag.statusList = statusSelectList;

            //to show employeecode
            HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"Employee");
            var userList = JsonConvert.DeserializeObject<List<Employee>>(await responseUserList.Content.ReadAsStringAsync());
            var userSelectList = new List<SelectListItem>();
            foreach (var user in userList)
            {
                userSelectList.Add(new SelectListItem(user.Emp.EmployeeCode, user.Id.ToString()));
            }
            ViewBag.userList = userSelectList;


            return View(detailsClaim);
        }

      //  [Authorize(Roles = "3")]
        [HttpPost("ExpenseClaim/EditByFinanceManager/{id}")]
        public async Task<IActionResult> EditByFinanceManager(ExpenseClaimViewModel expenseClaimViewModel)
        {
            expenseClaimViewModel.IndividualExpenditures.ForEach(x => x.ClaimId = expenseClaimViewModel.Id);
            var firstName = TempData["FirstName"];

           

            // Save ExpenseClaim
            var expenseClaimContent = JsonConvert.SerializeObject(expenseClaimViewModel);
            var expenseClaimBuffer = System.Text.Encoding.UTF8.GetBytes(expenseClaimContent);
            var expenseClaimByteContent = new ByteArrayContent(expenseClaimBuffer);
            expenseClaimByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            await client.PutAsync(client.BaseAddress + $"ExpenseClaim/", expenseClaimByteContent);

            //Save Individual Expenditure
            var expenditureContent = JsonConvert.SerializeObject(expenseClaimViewModel.IndividualExpenditures);
            var expenditureBuffer = System.Text.Encoding.UTF8.GetBytes(expenditureContent);
            var expenditureByteContent = new ByteArrayContent(expenditureBuffer);
            expenditureByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage editClaim = await client.PutAsync(client.BaseAddress + $"IndividualExpenditure/", expenditureByteContent);
            return RedirectToAction("Index");

        }

        // Get all that are approved

        [HttpGet]
        public async Task<IActionResult> IndexApprovedByFManager()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e =>  e.Status ==3).ToList() : new();

                //var filteredModel = model?.Count > 0 ? model.Where(e => e.Status ==3 || e.Status == 11).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }



        [HttpGet("ExpenseClaim/DetailsApprovedFM/{id}")]
        public async Task<IActionResult> DetailsApprovedFM(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            detailsClaim.FinanceManagerApprovedOn = DateTime.Now;
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
            //drop down to show department names
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            //dropdown to show categories
            HttpResponseMessage responseCategoryList = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            var categoryList = JsonConvert.DeserializeObject<List<ExpenseCategory>>(await responseCategoryList.Content.ReadAsStringAsync());
            var categorySelectList = new List<SelectListItem>();
            foreach (var category in categoryList)
            {
                categorySelectList.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
            ViewBag.categoryList = categorySelectList;

            //drop down to show status
            HttpResponseMessage responseStatusList = await client.GetAsync(client.BaseAddress + $"ClaimStatus");
            var statusList = JsonConvert.DeserializeObject<List<ClaimStatus>>(await responseStatusList.Content.ReadAsStringAsync());
            var statusSelectList = new List<SelectListItem>();
            foreach (var status in statusList)
            {
                statusSelectList.Add(new SelectListItem(status.Name, status.Id.ToString()));
            }
            ViewBag.statusList = statusSelectList;

            //to show employeecode
            HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"Employee");
            var userList = JsonConvert.DeserializeObject<List<Employee>>(await responseUserList.Content.ReadAsStringAsync());
            var userSelectList = new List<SelectListItem>();
            foreach (var user in userList)
            {
                userSelectList.Add(new SelectListItem(user.Emp.EmployeeCode, user.Id.ToString()));
            }
            ViewBag.userList = userSelectList;


            return View(detailsClaim);
        }


        //Get all partial approved by fmanager

        [HttpGet]
        public async Task<IActionResult> IndexPApprovedByFManager()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 7).ToList() : new();

                //var filteredModel = model?.Count > 0 ? model.Where(e => e.Status ==3 || e.Status == 11).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

        [HttpGet("ExpenseClaim/DetailsPartialApprovedFM/{id}")]
        public async Task<IActionResult> DetailsPartialApprovedFM(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            detailsClaim.FinanceManagerApprovedOn = DateTime.Now;
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
            //drop down to show department names
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            //dropdown to show categories
            HttpResponseMessage responseCategoryList = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            var categoryList = JsonConvert.DeserializeObject<List<ExpenseCategory>>(await responseCategoryList.Content.ReadAsStringAsync());
            var categorySelectList = new List<SelectListItem>();
            foreach (var category in categoryList)
            {
                categorySelectList.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
            ViewBag.categoryList = categorySelectList;

            //drop down to show status
            HttpResponseMessage responseStatusList = await client.GetAsync(client.BaseAddress + $"ClaimStatus");
            var statusList = JsonConvert.DeserializeObject<List<ClaimStatus>>(await responseStatusList.Content.ReadAsStringAsync());
            var statusSelectList = new List<SelectListItem>();
            foreach (var status in statusList)
            {
                statusSelectList.Add(new SelectListItem(status.Name, status.Id.ToString()));
            }
            ViewBag.statusList = statusSelectList;

            //to show employeecode
            HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"Employee");
            var userList = JsonConvert.DeserializeObject<List<Employee>>(await responseUserList.Content.ReadAsStringAsync());
            var userSelectList = new List<SelectListItem>();
            foreach (var user in userList)
            {
                userSelectList.Add(new SelectListItem(user.Emp.EmployeeCode, user.Id.ToString()));
            }
            ViewBag.userList = userSelectList;


            return View(detailsClaim);
        }





        // Get all that are rejected

        [HttpGet]
        public async Task<IActionResult> IndexRejectedByFManager()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 5).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

        [HttpGet("ExpenseClaim/DetailsRejectedFM/{id}")]
        public async Task<IActionResult> DetailsRejectedFM(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            detailsClaim.FinanceManagerApprovedOn = DateTime.Now;
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
            //drop down to show department names
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            //dropdown to show categories
            HttpResponseMessage responseCategoryList = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            var categoryList = JsonConvert.DeserializeObject<List<ExpenseCategory>>(await responseCategoryList.Content.ReadAsStringAsync());
            var categorySelectList = new List<SelectListItem>();
            foreach (var category in categoryList)
            {
                categorySelectList.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
            ViewBag.categoryList = categorySelectList;

            //drop down to show status
            HttpResponseMessage responseStatusList = await client.GetAsync(client.BaseAddress + $"ClaimStatus");
            var statusList = JsonConvert.DeserializeObject<List<ClaimStatus>>(await responseStatusList.Content.ReadAsStringAsync());
            var statusSelectList = new List<SelectListItem>();
            foreach (var status in statusList)
            {
                statusSelectList.Add(new SelectListItem(status.Name, status.Id.ToString()));
            }
            ViewBag.statusList = statusSelectList;

            //to show employeecode
            HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"Employee");
            var userList = JsonConvert.DeserializeObject<List<Employee>>(await responseUserList.Content.ReadAsStringAsync());
            var userSelectList = new List<SelectListItem>();
            foreach (var user in userList)
            {
                userSelectList.Add(new SelectListItem(user.Emp.EmployeeCode, user.Id.ToString()));
            }
            ViewBag.userList = userSelectList;


            return View(detailsClaim);
        }

        // [Authorize(Roles = "3")]
        [HttpGet]
        public async Task<IActionResult> GetByClaimId(int ID)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{ID}");
            return View();
        }

       // [Authorize(Roles = "3")]
        [HttpGet]
        public async Task<IActionResult> GetByEmployeeId(int EmpId)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{EmpId}");
            return View();
        }

       // [Authorize(Roles = "3")]
        [HttpGet]
        public async Task<IActionResult> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{startDate},{endDate}");
            return View();
        }

       // [Authorize(Roles = "3")]
        [HttpGet]
        public async Task<IActionResult> GetByClaimRequestDate(DateTime requestDate)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{requestDate}");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SelfApprovedByFManager()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 8).ToList() : new();

                //var filteredModel = model?.Count > 0 ? model.Where(e => e.Status ==3 || e.Status == 11).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

        [HttpGet("ExpenseClaim/SelfApprovedFM/{id}")]
        public async Task<IActionResult> SelfApprovedFM(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            detailsClaim.FinanceManagerApprovedOn = DateTime.Now;
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
            //drop down to show department names
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            //dropdown to show categories
            HttpResponseMessage responseCategoryList = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            var categoryList = JsonConvert.DeserializeObject<List<ExpenseCategory>>(await responseCategoryList.Content.ReadAsStringAsync());
            var categorySelectList = new List<SelectListItem>();
            foreach (var category in categoryList)
            {
                categorySelectList.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
            ViewBag.categoryList = categorySelectList;

            //drop down to show status
            HttpResponseMessage responseStatusList = await client.GetAsync(client.BaseAddress + $"ClaimStatus");
            var statusList = JsonConvert.DeserializeObject<List<ClaimStatus>>(await responseStatusList.Content.ReadAsStringAsync());
            var statusSelectList = new List<SelectListItem>();
            foreach (var status in statusList)
            {
                statusSelectList.Add(new SelectListItem(status.Name, status.Id.ToString()));
            }
            ViewBag.statusList = statusSelectList;

            //to show employeecode
            HttpResponseMessage responseUserList = await client.GetAsync(client.BaseAddress + $"Employee");
            var userList = JsonConvert.DeserializeObject<List<Employee>>(await responseUserList.Content.ReadAsStringAsync());
            var userSelectList = new List<SelectListItem>();
            foreach (var user in userList)
            {
                userSelectList.Add(new SelectListItem(user.Emp.EmployeeCode, user.Id.ToString()));
            }
            ViewBag.userList = userSelectList;


            return View(detailsClaim);
        }

    }
}

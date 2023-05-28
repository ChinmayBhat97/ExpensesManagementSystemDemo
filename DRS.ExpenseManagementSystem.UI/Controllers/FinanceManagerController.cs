
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
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

        //[Authorize(Roles = "3")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 2).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

        [HttpGet("FinanceManager/DetailsByClaimID/{claimId}")]
        public async Task<IActionResult> DetailsByClaimID(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaim>(await responseDetailsClaim.Content.ReadAsStringAsync());
            return View(detailsClaim);
        }

        [HttpGet("ExpenseClaim/EditByFinanceManager/{id}")]
        public async Task<IActionResult> EditbyFinanceManager(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

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


            return View(detailsClaim);
        }


        [HttpPost("ExpenseClaim/EditByFinanceManager/{id}")]
        public async Task<IActionResult> EditByFinanceManager(ExpenseClaimViewModel expenseClaimViewModel)
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



        [HttpGet]
        public async Task<IActionResult> GetByClaimId(int ID)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{ID}");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByEmployeeId(int EmpId)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{EmpId}");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{startDate},{endDate}");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByClaimRequestDate(DateTime requestDate)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{requestDate}");
            return View();
        }

       

    }
}

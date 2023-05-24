using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ExpenseClaimController : Controller
    {
       private readonly IConfiguration configuration;

        private readonly HttpClient client;

        public ExpenseClaimController(IConfiguration _configuration)
        {
           
            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int empID = Convert.ToInt32(TempData["logged_empID"]);
            int EmpID = empID;
           
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
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

        [HttpGet("ExpenseClaim/Create")]
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage responseCreateClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim");
            return View();

        }

        [HttpPost("ExpenseClaim/Create")]
        public async Task<IActionResult> Create(ExpenseClaimViewModel expenseClaimViewModel)
        {
            if (ModelState.IsValid)
            {
                int EmpID = Convert.ToInt32(TempData["logged_empID"]);
                var myContent = JsonConvert.SerializeObject(expenseClaimViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"ExpenseClaim",byteContent);
                return RedirectToAction("Index");
            }
            return View(expenseClaimViewModel);
        }

        [HttpGet("ExpenseClaim/EditByClaimant/{id}")]
        public async Task<IActionResult> EditByClaimant(int id)
        {
            HttpResponseMessage responseEditClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var EditClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseEditClaim.Content.ReadAsStringAsync());
            return View(EditClaim);
        }

        //[HttpGet("Department/EditDepartment/{id}")]
        //public async Task<IActionResult> EditDepartment(int id)
        //{
        //    HttpResponseMessage responseEditDepartment = await client.GetAsync(client.BaseAddress + $"Department/{id}");
        //    var EditDepartment = JsonConvert.DeserializeObject<DepartmentViewModel>(await responseEditDepartment.Content.ReadAsStringAsync());
        //    return View(EditDepartment);
        //}

        [HttpPost("ExpenseClaim/Edit")]
        public async Task<IActionResult> EditByClaimant(ExpenseClaimViewModel expenseClaimViewModel)
        {
            if (ModelState.IsValid)
            {
                expenseClaimViewModel.ClaimRequestDate = DateTime.Now;
                expenseClaimViewModel.Status = 1;
                var myContent = JsonConvert.SerializeObject(expenseClaimViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"ExpenseClaim/{expenseClaimViewModel.Id}", byteContent);
                return RedirectToAction("Index");
            }
            return View(expenseClaimViewModel);
        }

        [HttpGet("ExpenseClaim/DetailsByClaimID/{claimId}")]
        public async Task<IActionResult> DetailsByClaimID(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
           var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaim>(await responseDetailsClaim.Content.ReadAsStringAsync());
            return View(detailsClaim);
        }

        [HttpGet("IndividualExepnditure/Create")]
        public async Task<IActionResult> AddExpenditures(int Id)
        {
            ViewBag.ClaimId = Id;
            HttpResponseMessage responseCreateClaim = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure");
            return View();

        }

        [HttpPost("IndividualExepnditure/Create")]
        public async Task<IActionResult> AddExpenditures(IndividualExpenditure individualExpenditure)
        {

            int EmpID = Convert.ToInt32(TempData["logged_empID"]);
            //string wwwRootPath = _hostEnvironment.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            //string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            //imageModel.ImageName=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //string path = Path.Combine(wwwRootPath + "/Image/", fileName);
            //using (var fileStream = new FileStream(path, FileMode.Create))
            //{
            //    await imageModel.ImageFile.CopyToAsync(fileStream);
            //}
            var myContent = JsonConvert.SerializeObject(individualExpenditure);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"IndividualExpenditure", byteContent);
            return RedirectToAction("Index");

            // return View(individualExpenditure);
        }
    }
}

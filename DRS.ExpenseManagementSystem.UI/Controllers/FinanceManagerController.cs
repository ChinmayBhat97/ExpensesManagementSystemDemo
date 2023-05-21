
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class FinanceManagerController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public FinanceManagerController(IConfiguration _configuration)
        {

            this.configuration = _configuration;
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

        [HttpGet("FinanceManager/Index/{statusId}")]
        public async Task<IActionResult> Index(int statusId)
        {
           // HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{statusId}");
            //if (responseHomePage.IsSuccessStatusCode)
            //{
            //    var responseContent = await responseHomePage.Content.ReadAsStringAsync();
            //    var model = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseContent.Content.ReadAsStringAsync());
            //    return View(model);

                HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{statusId}");
                var EditClaim = JsonConvert.DeserializeObject<ExpenseClaim>(await responseHomePage.Content.ReadAsStringAsync());
                return View(EditClaim);
            //}
            //else
            //{
            //    // Handle the case where the HTTP request fails
            //    // Return an appropriate response or redirect
            //    // For now, let's return a default view with no data
            //    return View();
            //}
        }

        [HttpGet("FinanceManager/DetailsByClaimID/{claimId}")]
        public async Task<IActionResult> DetailsByClaimID(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaim>(await responseDetailsClaim.Content.ReadAsStringAsync());
            return View(detailsClaim);
        }



        [HttpGet("FinanceManager/Edit/{id}")]
        public async Task<IActionResult> EditByFinanceManager(int id)
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            return View("Edit");

        }

        [HttpPost("FinanceManager/Edit")]
        public async Task<IActionResult> EditByFinanceManager(ExpenseClaimViewModel expenseClaimViewModel)
        {
            if (ModelState.IsValid)
            {
                expenseClaimViewModel.FinanceManagerApprovedOn = DateTime.Now;
                var myContent = JsonConvert.SerializeObject(expenseClaimViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"ExpenseClaim/{expenseClaimViewModel.Id}", byteContent);
                return RedirectToAction("Index");
            }
            return View(expenseClaimViewModel);
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

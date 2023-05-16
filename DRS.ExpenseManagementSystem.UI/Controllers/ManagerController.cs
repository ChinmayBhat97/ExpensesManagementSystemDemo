using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ManagerController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public ManagerController(IConfiguration _configuration)
        {

            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim");
            return View();
        }

        [HttpGet("FinanceManager/Edit")]
        public async Task<IActionResult> EditByManager()
        {
            HttpResponseMessage responseFinanceManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim");
            return View();

        }

        [HttpPost("FinanceManager/Edit")]
        public async Task<IActionResult> EditByManager(ExpenseClaimViewModel expenseClaimViewModel)
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
    }
}

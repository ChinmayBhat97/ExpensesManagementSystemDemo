using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + $"ExpenseClaim");
            return View();
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
                expenseClaimViewModel.ClaimRequestDate = DateTime.Now;
                expenseClaimViewModel.Status = 1;
                var myContent = JsonConvert.SerializeObject(expenseClaimViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"ExpenseClaim",byteContent);

                return RedirectToAction("Index");
            }
           
            return View(expenseClaimViewModel);

        }

        [HttpGet("ExpenseClaim/Edit")]
        public async Task<IActionResult> EditByClaimant()
        {
            HttpResponseMessage responseCreateClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim");
            return View();

        }

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

    }
}

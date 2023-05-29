using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public ExpenseCategoryController(IConfiguration _configuration)
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
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseCategory");
            if (responseHomePage.IsSuccessStatusCode)
            {

                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseCategoryViewModel>>(responseContent);
                return View(model);
            }
            else
            {

                return View();
            }
        }



      //  [Authorize(Roles = "4")]
        [HttpGet("ExpenseCategory/CreateExpenseCategory")]
        public async Task<IActionResult> CreateExpenseCategoryAsync()
        {
            HttpResponseMessage responseCreateExpenseCategory = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            return View();
        }


      //  [Authorize(Roles = "4")]
        [HttpPost("ExpenseCategory/CreateExpenseCategory")]
        public async Task<IActionResult> CreateExpenseCategory(ExpenseCategory expenseCategory, string? title)
        {
            if (ModelState.IsValid)
            {
                title= expenseCategory.Name;
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress + $"ExpenseCategory?title={HttpUtility.UrlEncode(title)}", null);

                var myContent = JsonConvert.SerializeObject(expenseCategory);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage createNewExpenseCategory = await client.PostAsync(client.BaseAddress + $"ExpenseCategory", byteContent);

                return RedirectToAction("Index");
            }
            return View(expenseCategory);
        }

      //  [Authorize(Roles = "4")]
        [HttpGet("ExpenseCategory/EditExpenseCategory/{id}")]
        public async Task<IActionResult> EditExpenseCategory(int id)
        {
            HttpResponseMessage responseEditExpenseCategory = await client.GetAsync(client.BaseAddress + $"ExpenseCategory/{id}");
            var EditExpenseCategory = JsonConvert.DeserializeObject<ExpenseCategoryViewModel>(await responseEditExpenseCategory.Content.ReadAsStringAsync());
            return View(EditExpenseCategory);
        }



      //  [Authorize(Roles = "4")]
        [HttpPost("ExpenseCategory/EditExpenseCategory/{id}")]
        public async Task<IActionResult> EditExpenseCategory(int id, ExpenseCategoryViewModel expenseCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(expenseCategoryViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"ExpenseCategory/{expenseCategoryViewModel.Id}", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(expenseCategoryViewModel);
        }

      //  [Authorize(Roles = "4")]
        [HttpGet("ExpenseCategory/DetailsExpenseCategory/{id}")]
        public async Task<IActionResult> DetailsByExpenseCategoryID(int id)
        {
            HttpResponseMessage responseDetailsExpenseCategory = await client.GetAsync(client.BaseAddress + $"ExpenseCategory/{id}");
            var detailsExpenseCategory = JsonConvert.DeserializeObject<ExpenseCategoryViewModel>(await responseDetailsExpenseCategory.Content.ReadAsStringAsync());
            return View(detailsExpenseCategory);
        }

      // [Authorize(Roles = "4")]
        [HttpGet("ExpenseCategory/DetailsByExpenseCategoryTitle")]
        public async Task<IActionResult> DetailsByExpenseCategoryTitle()
        {
            HttpResponseMessage responseCreateExpenseCategory = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            return View();
        }


       
    }
}

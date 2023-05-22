using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using ExpenseClaimViewModel = DRS.ExpenseManagementSystem.UI.Models.ExpenseClaimViewModel;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ManagerController : Controller
    {
       private readonly IConfiguration configuration;
       private readonly HttpClient client;

        public ManagerController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
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



        [HttpGet("Manager/EditByManager/{id}")]
        public async Task<IActionResult> EditByManager(int id)
        {
            HttpResponseMessage responseEditUser = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var EditByManager = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseEditUser.Content.ReadAsStringAsync());
            return View(EditByManager);
        }

        [HttpPost("Manager/EditByManager/{id}")]
        public async Task<IActionResult> EditByManager(int id, ExpenseClaimViewModel expenseClaimViewModel)
        {
            if (ModelState.IsValid)
            {
                expenseClaimViewModel.FinanceManagerApprovedOn = DateTime.Now;
                var myContent = JsonConvert.SerializeObject(expenseClaimViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"ExpenseClaim/{expenseClaimViewModel.Id}", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(expenseClaimViewModel);
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

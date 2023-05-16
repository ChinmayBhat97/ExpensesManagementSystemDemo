using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class FinanceManagerController : Controller
    {
        //readonly IConfiguration configuration;
        //readonly HttpClient client;

        //public FinanceManagerController(IConfiguration _configuration)
        //{
            
        //    this.configuration = _configuration;
        //    this.client = new HttpClient
        //    {
        //        BaseAddress = new Uri(configuration["BaseUrl"]),
        //        Timeout = TimeSpan.FromMinutes(5)
        //    };
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}

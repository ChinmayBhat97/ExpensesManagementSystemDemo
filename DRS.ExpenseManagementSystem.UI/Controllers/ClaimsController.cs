using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ClaimsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

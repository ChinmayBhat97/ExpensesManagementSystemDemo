using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

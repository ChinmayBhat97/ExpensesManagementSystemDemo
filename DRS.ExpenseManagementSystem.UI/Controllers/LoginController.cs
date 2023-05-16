using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class LoginController : Controller
    {
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("Index");
        }
    }
}

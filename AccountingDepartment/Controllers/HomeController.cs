using Microsoft.AspNetCore.Mvc;

namespace AccountingDepartment.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

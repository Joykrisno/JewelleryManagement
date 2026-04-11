using Microsoft.AspNetCore.Mvc;

namespace JewelleryManagement.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

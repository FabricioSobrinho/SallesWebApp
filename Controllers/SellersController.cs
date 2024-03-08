using Microsoft.AspNetCore.Mvc;

namespace SallesWebApp.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

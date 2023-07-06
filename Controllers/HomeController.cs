using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static ThemisworkshopContext _temp;

        public HomeController(ILogger<HomeController> logger,ThemisworkshopContext context)
        {
            _logger = logger;
            _temp = context;
        }

        public IActionResult Index(int? id)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
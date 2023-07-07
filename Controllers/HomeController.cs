using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static ThemisworkshopContext _temp;
        public static Usuario? usuario;

        public HomeController(ILogger<HomeController> logger,ThemisworkshopContext context)
        {
            _logger = logger;
            _temp = context;
        }

        public IActionResult Index(int? id)
        {
            if (HttpContext.Session.GetString("usuario") != null)
            {
                usuario = _temp.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).First();
                return View();
            }
            else 
            {
               return Redirect("/Sesion/IniciarSesion");
            }
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
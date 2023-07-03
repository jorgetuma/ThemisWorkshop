using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class SesionController : Controller
    {
        [HttpGet]
        public ActionResult IniciarSesion()
        {
            SesionViewModel model = new SesionViewModel(" "," ");
            return View("IniciarSesion",model);
        }
    }
}

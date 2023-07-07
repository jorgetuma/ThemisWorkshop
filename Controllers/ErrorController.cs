using Microsoft.AspNetCore.Mvc;

namespace ThemisWorkshop.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("/403")]
        public ActionResult AccesoDenegado()
        { 
            return View("403");
        }

        [HttpGet]
        [Route("/404")]
        public ActionResult PaginaNoEncontrada()
        {
            return View("404");
        }

        [HttpGet]
        [Route("/405")]
        public ActionResult MetodoNoPermitido()
        {
            return View("405");
        }
    }
}

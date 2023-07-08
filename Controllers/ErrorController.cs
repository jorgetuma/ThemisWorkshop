using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ThemisworkshopContext _context;
        public static Usuario? usuario;

        public ErrorController(ThemisworkshopContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/403")]
        public ActionResult AccesoDenegado()
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            return View("403");
        }

        [HttpGet]
        [Route("/404")]
        public ActionResult PaginaNoEncontrada()
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            return View("404");
        }

        [HttpGet]
        [Route("/405")]
        public ActionResult MetodoNoPermitido()
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            return View("405");
        }
    }
}

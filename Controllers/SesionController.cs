using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class SesionController : Controller
    {
        private readonly ThemisworkshopContext _context;
        public static ThemisworkshopContext _temp;
        private Usuario? user;

        public SesionController(ThemisworkshopContext context) 
        { 
            _context = context;
            _temp = context;
        }

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            SesionViewModel model = new SesionViewModel(" "," ",false);
            return View("IniciarSesion",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth()
        { 
         string userName = Request.Form["user"].ToString();
         string password = Request.Form["pass"].ToString();
            if (Autenticar(userName, password) && ModelState.IsValid)
            {
                HttpContext.Session.SetString("usuario", userName);
                return Redirect("/Home");
            }
            else
            {
                SesionViewModel model = new SesionViewModel(userName,password,true);
                return View("IniciarSesion",model);
            }
        }

        [HttpGet]
        public ActionResult CerrarSesion() 
        {
            HttpContext.Session.Remove("usuario");
            return RedirectToAction("IniciarSesion");
        }

        private bool Autenticar(string userName, string password) 
        {
             user = _context.Usuario.Where(e => e.UserName == userName && e.Password == password && e.Eliminado == false).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}

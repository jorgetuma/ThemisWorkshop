using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            if ( HttpContext.Session.GetString("usuario") == null && ExisteCookieUsuario())
            {
                CargarCookie();
                return Redirect("/Home");
            }
            SesionViewModel model = new SesionViewModel(" "," ",false);
            return View("IniciarSesion",model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth()
        { 
         string userName = Request.Form["user"].ToString();
         string password = Request.Form["pass"].ToString();
            string recordar = Request.Form["recordarUsuario"].ToString();
            if (Autenticar(userName, password) && ModelState.IsValid)
            {
                HttpContext.Session.SetString("usuario", userName);
                if (recordar.Equals("on")) 
                {
                    CrearCookie(user.IdUsuario);
                }
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
            SesionViewModel model = new SesionViewModel("","",false);
            return View("IniciarSesion",model);
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

        private void CrearCookie(int id)
        {
            var cookie = new Cookie("Themisworkshop",id.ToString());
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            cookieOptions.Secure = true;
            cookieOptions.IsEssential = true;
            Response.Cookies.Append(cookie.Name,cookie.Value,cookieOptions);
        }


        public void CargarCookie()
        {
            user = _context.Usuario.Where(e => e.IdUsuario == int.Parse(Request.Cookies["Themisworkshop"].ToString()) && e.Eliminado == false).FirstOrDefault();
            if (user != null)
            {
                HttpContext.Session.SetString("usuario", user.UserName);
            }
        }

        public bool ExisteCookieUsuario()
        {
            if (Request.Cookies["Themisworkshop"] == null) 
            { 
                return false;
            }
            return true;
        }
    }
}

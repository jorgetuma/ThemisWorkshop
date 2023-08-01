using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ThemisworkshopContext _context;
        public static ThemisworkshopContext? _temp; /*Para uso exclusivo en el frontend*/
        public static Usuario? usuario;

        public CategoriaController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = context;
        }

        [HttpGet]
        [Route("/Categoria/ListarCategorias/{pag}")]
        public ActionResult ListarCategorias(int pag)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Secretario) || usuario.Rol == ((int)Rolesapp.Abogado))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            if (pag <= 0)
            {
                pag = 1;
            }
            List<Categoria> categorias = LoadCategorias(pag);
            return View("ListarCategorias",categorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategoria()
        {
            string nombre = Request.Form["categoria"].ToString();
            var categoria = new Categoria(nombre);

            _context.Categoria.Add(categoria);
            _context.SaveChanges();
            return Redirect("/Categoria/ListarCategorias/1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModCategoria() 
        { 
            int id = int.Parse(Request.Form["id"].ToString());
            Categoria? categoria = _context.Categoria.Where(e => e.IdCategoria == id).FirstOrDefault();
            if (categoria != null)
            {
                string nombre = Request.Form["categoria"].ToString();

                categoria.Nombre = nombre;
                _context.Categoria.Update(categoria);
                _context.SaveChanges();
                return Redirect("/Categoria/ListarCategorias/1");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("/Categoria/EliminarCategoria/{id}")]
        public ActionResult EliminarCategoria(int id)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Secretario) || usuario.Rol == ((int)Rolesapp.Abogado))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            Categoria? categoria = _context.Categoria.Where(e => e.IdCategoria == id).FirstOrDefault();
            if (categoria != null)
            {
                categoria.Eliminado = true;
                _context.Categoria.Update(categoria);
                _context.SaveChanges();
                return Redirect("/Categoria/ListarCategorias/1");
            }
            else 
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Error() 
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Secretario) || usuario.Rol == ((int)Rolesapp.Abogado))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            return View("Error");
        }

        private int CantidadCategorias()
        {
            return _context.Categoria.Count(e => e.Eliminado == false);
        }

        private List<Categoria> LoadCategorias(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadCategorias();

            if (indIni >= cantidad)
            {
                return new List<Categoria>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Categoria> categorias = _context.Categoria
                .Where(e => e.Eliminado == false)
                .OrderBy(e => e.IdCategoria)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return categorias;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return (int)Math.Ceiling((double)_temp.Categoria.Count(e => e.Eliminado == false) / cantidadPorpagina);
        }
    }
}

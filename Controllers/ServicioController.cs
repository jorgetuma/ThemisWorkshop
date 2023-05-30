using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ServicioController : Controller
    {
        private readonly ThemisworkshopContext _context;
        static ThemisworkshopContext? _temp; /*Para uso exclusivo en el frontend*/

        public ServicioController(ThemisworkshopContext context) 
        { 
            _context = context;
            _temp = context;
        }
        
        [HttpGet]
        [Route("/Servicio/ListarServicios/{pag}")]
        public ActionResult ListarServicios(int pag)
        {
            if (pag <= 0)
            { 
                pag = 1;
            }
            List<Servicio> servicios = LoadServicios(pag);
            return View("ListarServicios",servicios);
        }

        [HttpGet]
        public ActionResult AgregarServicio() 
        {
            return View("AgregarServicio");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddServicio()
        {
            string nombre = Request.Form["nombre"].ToString();
            string descripcion = Request.Form["descripcion"].ToString();
            decimal precioFijo = decimal.Parse(Request.Form["costo"].ToString());

            var servicio = new Servicio(nombre, descripcion, precioFijo);

            _context.Servicio.Add(servicio);
            _context.SaveChanges();

            return RedirectToAction("AgregarServicio");
        }

        [HttpGet]
        [Route("/Servicio/ModificarServicio/{id}")]
        public ActionResult ModificarServicio(int id)
        {
            Servicio? servicio = _context.Servicio.Find(id);

            if (servicio != null)
            {
                return View("AgregarServicio", servicio);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModServicio()
        {
            int id = int.Parse(Request.Form["id"].ToString());
            Servicio? servicio = _context.Servicio.Find(id);

            if (servicio != null)
            {
                string nombre = Request.Form["nombre"].ToString();
                string descripcion = Request.Form["descripcion"].ToString();
                decimal precioFijo = decimal.Parse(Request.Form["costo"].ToString());

                servicio.Nombre = nombre;
                servicio.Descripcion = descripcion;
                servicio.Preciofijo = precioFijo;

                _context.Servicio.Update(servicio);
                _context.SaveChanges();

                return Redirect("/Servicio/ListarServicios/1");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("/Servicio/EliminarServicio/{id}")]
        public ActionResult EliminarServicio(int id)
        {
            Servicio? servicio = _context.Servicio.Find(id);

            if (servicio != null)
            {
                servicio.Eliminado = true;
                _context.Servicio.Update(servicio);
                _context.SaveChanges();

                return Redirect("/Servicio/ListarServicios/1");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View("Error");
        }

        private int CantidadServicios()
        {
            return _context.Servicio.Count(e => e.Eliminado == false);
        }

        private List<Servicio> LoadServicios(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadServicios();

            if (indIni >= cantidad)
            {
                return new List<Servicio>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Servicio> servicios = _context.Servicio
                .Where(e => e.Eliminado == false)
                .OrderBy(e => e.IdServicio)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return servicios;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return _temp.Servicio.Count(e => e.Eliminado == false) / cantidadPorpagina;
        }

        public static List<Servicio> ObtenerServicios() 
        { 
            return _temp.Servicio.Where(e => e.Eliminado == false).ToList();
        }
    }
}

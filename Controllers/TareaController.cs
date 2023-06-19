using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class TareaController : Controller
    {
        private readonly ThemisworkshopContext _context;

        public static ThemisworkshopContext _temp;

        Usuario? usuario; // se va al implementar sesiones

        public TareaController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = _context;
        }

        [HttpGet]
        [Route("/Tarea/ListarTareas/{pag}")]
        public ActionResult ListarTareas(int pag) 
        {
            if (pag <= 0) 
            { 
                pag = 1;
            }
            List<Tarea> tareas = LoadTareas(pag);
            return View("ListarTareas",tareas);
        }

        [HttpGet]
        public ActionResult AgregarTarea()
        {
            usuario = _context.Usuario.Find(1);
            TareaViewModel model = new TareaViewModel(null,usuario,false);
            return View("AgregarTarea",model);
        }

        [HttpGet]
        [Route("/Tarea/ModificarTarea/{id}")]
        public ActionResult ModificarTarea(int id) 
        {
            Tarea? tarea = _context.Tarea.Find(id);
            usuario = _context.Usuario.Find(1);
            if (tarea != null)
            {
                TareaViewModel model = new TareaViewModel(tarea,usuario,true);
                return View("AgregarTarea",model);
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

        private int CantidadTareas()
        {
            return _context.Tarea.Count(e => e.Realizado == false);
        }

        private List<Tarea> LoadTareas(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadTareas();

            if (indIni >= cantidad)
            {
                return new List<Tarea>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Tarea> tareas = _context.Tarea
                .Where(e => e.Realizado == false)
                .OrderBy(e => e.IdTarea)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return tareas;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return _temp.Tarea.Count(e => e.Realizado == false) / cantidadPorpagina;
        }
    }
}

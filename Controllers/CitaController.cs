using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class CitaController : Controller
    {
        private readonly ThemisworkshopContext _context;

        public static ThemisworkshopContext _temp;

        public CitaController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = context;
        }

        [HttpGet]
        [Route("Cita/ListarCitas/{pag}")]
        public ActionResult ListarCitas(int pag)
        {
            if (pag <= 0)
            {
                pag = 1;
            }
            List<Cita> citas = LoadCitas(pag);
            return View("ListarCitas", citas);
        }

        [HttpGet]
        public ActionResult AgregarCita()
        {
            return View("AgregarCita");
        }

        [HttpGet]
        [Route("/Cita/ReagendarCita/{id}")]
        public ActionResult ReagendarCita(int id) {
            Cita? cita = _context.Cita.Find(id);
            if (cita != null)
            {
                return View("AgregarCita", cita);
            }
            else 
            { 
              return  RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Error() 
        {
            return View("Error");
        }

        [HttpGet]
        [Route("/Calendario")]
        public ActionResult Calendario()
        {
            List<Cita> citas = _context.Cita.ToList(); 
            return View("Calendario",citas);
        }

        private int CantidadCitas()
        {
            return _context.Clientes.Count();
        }

        private List<Cita> LoadCitas(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadCitas();

            if (indIni >= cantidad)
            {
                return new List<Cita>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Cita> citas = _context.Cita
                .OrderBy(e => e.IdCita)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return citas;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return _temp.Cita.Count() / cantidadPorpagina;
        }
    }
}

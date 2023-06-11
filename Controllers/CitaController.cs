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
        [Route("Cita/AgregarCita/{idCliente}")]
        public ActionResult AgregarCita(int idCliente)
        {
            Cliente? cliente = _context.Clientes.Find(idCliente);
            if (cliente != null)
            {
                CitaViewModel model = new CitaViewModel(null, cliente, false);
                return View("AgregarCita", model);
            }
            else 
            {
                return Redirect("/Cliente/Error");
            }
            }

            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCita()
        {
            int idcliente = int.Parse(Request.Form["idcliente"].ToString());
            string asunto = Request.Form["asunto"].ToString();
            string descripcion = Request.Form["descripcion"].ToString();
            string lugar = Request.Form["lugar"].ToString();
            DateTime fecha = DateTime.Parse(Request.Form["fecha"].ToString());
            DateTime fechautc = DateTime.SpecifyKind(fecha, DateTimeKind.Utc);
            fechautc = fechautc.AddDays(1);
            TimeOnly horaini = TimeOnly.Parse(Request.Form["horainicial"].ToString());
            TimeOnly horafin = TimeOnly.Parse("00:00");

            var cita = new Cita(idcliente, 1, asunto, lugar, descripcion, fechautc, horaini, horafin);
            _context.Cita.Add(cita);
            _context.SaveChanges();
            return Redirect("/Cita/ListarCitas/1");
        }

        [HttpGet]
        [Route("/Cita/ReagendarCita/{id}")]
        public ActionResult ReagendarCita(int id) {
            Cita? cita = _context.Cita.Find(id);
            if (cita != null)
            {
                Cliente? cliente = _context.Clientes.Find(cita.IdCliente);
                CitaViewModel model = new CitaViewModel(cita, cliente, true);
                return View("AgregarCita", model);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModCita()
        {
            int id = int.Parse(Request.Form["id"].ToString());
            Cita? cita = _context.Cita.Find(id);
            if (cita != null)
            {
                string asunto = Request.Form["asunto"].ToString();
                string descripcion = Request.Form["descripcion"].ToString();
                string lugar = Request.Form["lugar"].ToString();
                DateTime fecha = DateTime.Parse(Request.Form["fecha"].ToString());
                DateTime fechautc = DateTime.SpecifyKind(fecha, DateTimeKind.Utc);
                fechautc = fechautc.AddDays(1);
                TimeOnly horaini = TimeOnly.Parse(Request.Form["horainicial"].ToString());

                cita.Asunto = asunto;
                cita.Descripcion = descripcion;
                cita.Lugar = lugar;
                cita.Fecha = fechautc;
                cita.HoraInicial = horaini;

                _context.Cita.Update(cita);
                _context.SaveChanges();

                return Redirect("/Cita/ListarCitas/1");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("Cita/EliminarCita/{id}")]
        public ActionResult EliminarCita(int id)
        { 
            Cita? cita = _context.Cita.Find(id);
            if (cita != null)
            {
                cita.Realizado = true;
                cita.Fecha = DateTime.SpecifyKind(cita.Fecha,DateTimeKind.Utc);
                _context.Cita.Update(cita);
                _context.SaveChanges();
                return Redirect("/Cita/ListarCitas/1");
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
                .Where(e => e.Realizado == false)
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

using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ExpedienteController : Controller
    {
        private readonly ThemisworkshopContext _context;
        static ThemisworkshopContext? _temp; /*Para uso exclusivo en el frontend*/

        public ExpedienteController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = context;
        }

        [HttpGet]
        [Route("/Expediente/ListarExpedientes/{pag}")]
        public ActionResult ListarExpedientes(int pag)
        {
            if (pag <= 0)
            {
                pag = 1;
            }
            List<Expediente> expedientes = LoadExpedientes(pag);
            return View("ListarExpedientes", expedientes);
        }

        [HttpGet]
        [Route("/Expediente/AgregarExpediente/{idCliente}")]
        [Route("/Expediente/AgregarExpediente")]
        public ActionResult AgregarExpediente(int idCliente)
        {
            Cliente? cliente = _context.Clientes.Find(idCliente);
            ExpedienteViewModel model = new ExpedienteViewModel(null,cliente,false);
            return View("AgregarExpediente", model);
        }

        /*[HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult AddExpediente()
         {
             string nombre = Request.Form["nombre"].ToString();
             string descripcion = Request.Form["descripcion"].ToString();
             decimal precioFijo = decimal.Parse(Request.Form["costo"].ToString());

             var expediente = new Expediente();

             _context.Expediente.Add(expediente);
             _context.SaveChanges();

             return RedirectToAction("AgregarExpediente");
         }*/

        [HttpGet]
        [Route("/Expediente/ModificarExpediente/{id}")]
        public ActionResult ModificarExpediente(int id)
        {
            Expediente? expediente = _context.Expediente.Find(id);
            if (expediente != null)
            {
                ExpedienteViewModel model = new ExpedienteViewModel(expediente,null, true);
                return View("AgregarExpediente", model);
            }
            else 
            {     
           return RedirectToAction("Error"); 
            }
        }

        [HttpGet]
        [Route("/Expediente/EliminarExpediente/{id}")]
        public ActionResult EliminarExpediente(int id)
        {
            Expediente? expediente = _context.Expediente.Find(id);

            if (expediente != null)
            {
                expediente.Activo = false;
                _context.Expediente.Update(expediente);
                _context.SaveChanges();

                return Redirect("/Expediente/ListarExpedientes/1");
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

        private int CantidadExpedientes()
        {
            return _context.Expediente.Count(e => e.Activo == true);
        }

        private List<Expediente> LoadExpedientes(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadExpedientes();

            if (indIni >= cantidad)
            {
                return new List<Expediente>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Expediente> expedientes = _context.Expediente
                .Where(e => e.Activo == true)
                .OrderBy(e => e.IdExpediente)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return expedientes;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return _temp.Expediente.Count(e => e.Activo == true) / cantidadPorpagina;
        }
    }
}

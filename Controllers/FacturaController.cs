using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class FacturaController : Controller
    {
        private ThemisworkshopContext _context;

        public static ThemisworkshopContext _temp;

        public FacturaController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = context;
        }

        [HttpGet]
        [Route("/Factura/ListarFacturas/{pag}")]
        public ActionResult ListarFacturas(int pag)
        {
            if (pag <= 0)
            {
                pag = 1;
            }
            List<Factura> facturas = LoadFacturas(pag);
            return View("ListarFacturas",facturas);
        }

        [HttpGet]
        [Route("/Factura/GenerarFactura/{idServicio}&{idConsulta}&{idExpediente}")]
        public ActionResult GenerarFactura(int idServicio, int idConsulta,int idExpediente)
        {
            FacturaViewModel model = new FacturaViewModel(idServicio,idConsulta,idExpediente,null);
            return View("GenerarFactura",model);
        }

        [HttpGet]
        [Route("/Factura/EliminarFactura/{id}")]
        public ActionResult EliminarFactura(int id) 
        {
            Factura? factura = _context.Factura.Find(id);
            if (factura != null)
            {
                factura.Eliminado = true;
                factura.FechaEmision = DateTime.SpecifyKind(factura.FechaEmision,DateTimeKind.Utc);
                factura.FechaLimite = DateTime.SpecifyKind(factura.FechaLimite, DateTimeKind.Utc);
                _context.Factura.Update(factura);
                _context.SaveChanges();
                return RedirectToAction("Factura/ListarFacturas/1");
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

        private int CantidadFacturas()
        {
            return _context.Factura.Count(e => e.Eliminado == false);
        }

        private List<Factura> LoadFacturas(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadFacturas();

            if (indIni >= cantidad)
            {
                return new List<Factura>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Factura> facturas = _context.Factura
                .Where(e => e.Eliminado == false)
                .OrderBy(e => e.IdFactura)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return facturas;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return _temp.Factura.Count(e => e.Eliminado == false) / cantidadPorpagina;
        }
    }
}

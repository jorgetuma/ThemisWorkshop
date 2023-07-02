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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFactura() 
        {
            int idServicio = int.Parse(Request.Form["idServicio"].ToString());
            int idConsulta = int.Parse(Request.Form["idConsulta"].ToString());
            int idExpediente = int.Parse(Request.Form["idExpediente"].ToString());
            int idCliente = int.Parse(Request.Form["IdCliente"].ToString());
            string titulo = Request.Form["titulo"].ToString();
            Cliente? cliente = _context.Clientes.FirstOrDefault(c => c.IdClientes == idCliente);
            Servicio? servicio = _context.Servicio.Find(idServicio);
            DateTime  fechaEmision = DateTime.Today;
            DateTime fechaEmisionUtc = DateTime.SpecifyKind(fechaEmision,DateTimeKind.Utc);
            fechaEmisionUtc = fechaEmisionUtc.AddDays(1);
            DateTime fechaLimite = DateTime.Parse(Request.Form["fechalimite"].ToString());
            DateTime fechaLimiteUtc = DateTime.SpecifyKind(fechaLimite, DateTimeKind.Utc);
            fechaLimiteUtc = fechaLimiteUtc.AddDays(1);
            bool esCredito = bool.Parse(Request.Form["esCredito"].ToString());
            decimal costo = 0;
            decimal montoVariable = decimal.Parse(Request.Form["montovariable"].ToString());
            decimal montoPorPagar = 0;

            if (idExpediente != -1)
            {
                costo = servicio.Preciofijo + montoVariable;
                if (esCredito)
                {
                    montoPorPagar = servicio.Preciofijo + montoVariable;
                    cliente.Credito += montoPorPagar;
                }
                Detalleservicio? ds = _context.Detalleservicio.Where(e => e.IdServicio == idServicio && e.IdExpediente == idExpediente).First();
                if (ds.Facturado == true) { 
                return RedirectToAction("ServicioFacturado");
                }
                ds.Facturado = true;
            }

            if (idConsulta != -1) {
                Consulta? consulta = _context.Consulta.Find(idConsulta);
                costo = consulta.Precio + montoVariable;
                if (esCredito)
                {
                    montoPorPagar = consulta.Precio + montoVariable;
                    cliente.Credito += montoPorPagar;
                }
                if (consulta.Facturado == true)
                {
                    return RedirectToAction("ServicioFacturado");
                }
                consulta.Facturado = true;
            }

            Factura factura = new Factura(idServicio, idCliente, costo, montoVariable, fechaEmisionUtc, fechaLimiteUtc, esCredito,titulo);
            factura.MontoPorPagar = montoPorPagar;

            _context.Factura.Add(factura);
            _context.SaveChanges();
            return Redirect("/Factura/ListarFacturas/1"); 
        }

        [HttpGet]
        [Route("/Factura/SaldarFactura/{id}")]
        public ActionResult SaldarFactura(int id) 
        { 
            Factura? factura = _context.Factura.Find(id);
            if (factura != null)
            {
                FacturaViewModel model = new FacturaViewModel(-1,-1,-1,factura);
                return View("SaldarFactura",model);
            }
            else 
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult modFactura() 
        {
            int id = int.Parse(Request.Form["idFactura"].ToString());
            Factura? factura = _context.Factura.Find(id);
            if (factura != null)
            {
                decimal abono = decimal.Parse(Request.Form["abono"].ToString());
                Cliente? cliente = _context.Clientes.Find(factura.IdCliente);
                factura.MontoPorPagar -= abono;
                cliente.Credito -= abono;

                factura.FechaEmision = DateTime.SpecifyKind(factura.FechaEmision,DateTimeKind.Utc);
                factura.FechaLimite = DateTime.SpecifyKind(factura.FechaLimite,DateTimeKind.Utc);
                _context.Factura.Update(factura);
                _context.SaveChanges();
                return Redirect("/Factura/ListarFacturas/1");
            }
            else
            { 
             return RedirectToAction("Error");
            }

        } 

        [HttpGet]
        [Route("/Factura/EliminarFactura/{id}")]
        public ActionResult EliminarFactura(int id) 
        {
            Factura? factura = _context.Factura.Find(id);
            if (factura != null)
            {
                if (factura.MontoPorPagar <= 0)
                {
                    factura.Eliminado = true;
                    factura.FechaEmision = DateTime.SpecifyKind(factura.FechaEmision, DateTimeKind.Utc);
                    factura.FechaLimite = DateTime.SpecifyKind(factura.FechaLimite, DateTimeKind.Utc);
                    _context.Factura.Update(factura);
                    _context.SaveChanges();
                    return Redirect("/Factura/ListarFacturas/1");
                }else {
                    return RedirectToAction("FacturaSinPagar");   
                 }
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
        public ActionResult ServicioFacturado() 
        {
            return View("ServicioFacturado");
        }

        [HttpGet]
        public ActionResult FacturaSinPagar() 
        {
            return View("FacturaSinPagar");
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

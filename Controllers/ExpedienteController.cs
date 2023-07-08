using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ExpedienteController : Controller
    {
        private readonly ThemisworkshopContext _context;
        public static ThemisworkshopContext? _temp; /*Para uso exclusivo en el frontend*/
        public static Usuario? usuario;

        public ExpedienteController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = context;
        }

        [HttpGet]
        [Route("/Expediente/ListarExpedientes/{pag}")]
        public ActionResult ListarExpedientes(int pag)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (pag <= 0)
            {
                pag = 1;
            }
            List<Expediente> expedientes = LoadExpedientes(pag);
            return View("ListarExpedientes", expedientes);
        }

        [HttpGet]
        [Route("/Expediente/AgregarExpediente/{idCliente}")]
        public ActionResult AgregarExpediente(int idCliente)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            Cliente? cliente = _context.Clientes.Find(idCliente);
            if (cliente != null)
            {
                ExpedienteViewModel model = new ExpedienteViewModel(null, cliente, false);
                model.Servicios = _context.Servicio.Where(e => e.Eliminado == false).ToList();
                return View("AgregarExpediente", model);
            }
            else 
            {
                return Redirect("/Cliente/Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult AddExpediente()
         {
            int idCliente = int.Parse(Request.Form["idCliente"].ToString()); 
            string titulo = Request.Form["titulo"].ToString();
            string descripcion = Request.Form["descripcion"].ToString();
            string ids = Request.Form["id"].ToString();
            List<string> servicios = new List<string>();
            DateTime fecha = DateTime.Today;
            DateTime fechaUtc = DateTime.SpecifyKind(fecha, DateTimeKind.Utc);
            fechaUtc = fechaUtc.AddDays(1);
            if (ids.Count() > 1)
            {
                servicios = Request.Form["id"].ToString().Split('-').ToList();
            }
            else
            {
                servicios.Add(ids);
            }

            var expediente = new Expediente(idCliente,usuario.IdUsuario,titulo,descripcion,fechaUtc);


             _context.Expediente.Add(expediente);
            _context.SaveChanges();
            for (int i = 0; i < servicios.Count(); i++)
            {
                int id = int.Parse(servicios.ElementAt(i).ToString());
                var ds = new Detalleservicio(id, expediente.IdExpediente);
                _context.Detalleservicio.Add(ds);
            }
            _context.SaveChanges();

             return Redirect("/Expediente/ListarExpedientes/1");
         }

        [HttpGet]
        [Route("/Expediente/ModificarExpediente/{id}")]
        public ActionResult ModificarExpediente(int id)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            Expediente? expediente = _context.Expediente.Find(id);
            if (expediente != null)
            {
                Cliente cliente = _context.Clientes.Find(expediente.IdCliente);
                ExpedienteViewModel model = new ExpedienteViewModel(expediente,cliente, true);
                model.Servicios = _context.Servicio.Where(e => e.Eliminado == false).ToList();
                return View("AgregarExpediente", model);
            }
            else 
            {     
           return RedirectToAction("Error"); 
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModExpediente() 
        {
            int id = int.Parse(Request.Form["idExpediente"].ToString());
            Expediente? expediente = _context.Expediente.Find(id);
            if (expediente != null)
            {
                string titulo = Request.Form["titulo"].ToString();
                string descripcion = Request.Form["descripcion"].ToString();
                int estado = int.Parse(Request.Form["estado"].ToString());
                string ids = Request.Form["id-originales"].ToString();
                string idsMod = Request.Form["id"].ToString();
                List<Detalleservicio> servicios = _context.Detalleservicio.Where(e => e.IdExpediente == expediente.IdExpediente).ToList();
                List<string> serviciosMod = new List<string>();

                if (!idsMod.Equals("empty"))
                {
                    serviciosMod = Request.Form["id"].ToString().Split('-').ToList();
                }

                switch (estado) { 
                case 1:
                expediente.Activo = true;
                break;
                case 0:
                expediente.Activo = false;
                break;
                }
                
                expediente.Asunto = titulo;
                expediente.Descripcion = descripcion;
                expediente.FechaApertura = DateTime.SpecifyKind(expediente.FechaApertura, DateTimeKind.Utc);
                expediente.FechaApertura = expediente.FechaApertura.AddDays(1);
                _context.Expediente.Update(expediente);
                _context.SaveChanges();
   
                if (!idsMod.Equals("empty"))
                {
                    List<int> serFacturado = new List<int>();
                    for (int i = 0; i < servicios.Count(); i++)
                    {
                        Detalleservicio ds = servicios.ElementAt(i);
                        if (ds.Facturado == false)
                        {
                            _context.Detalleservicio.Remove(ds);
                            _context.SaveChanges();
                        }
                        else {
                            serFacturado.Add(servicios.ElementAt(i).IdServicio);
                        }
                    }

                    for (int i = 0; i < serviciosMod.Count(); i++)
                    {
                        int idServ = int.Parse(serviciosMod.ElementAt(i).ToString());
                        if (!serFacturado.Contains(idServ))
                        {
                            Detalleservicio ds = new Detalleservicio(idServ, expediente.IdExpediente);
                            _context.Detalleservicio.Add(ds);
                            _context.SaveChanges();
                        }
                    }
                }

                return Redirect("/Expediente/ListarExpedientes/1");
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
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            Expediente? expediente = _context.Expediente.Find(id);

            if (expediente != null)
            {
                expediente.Activo = false;
                expediente.FechaApertura = DateTime.SpecifyKind(expediente.FechaApertura, DateTimeKind.Utc);
                expediente.FechaApertura = expediente.FechaApertura.AddDays(1);
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
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            return View("Error");
        }

        private int CantidadExpedientes()
        {
            return _context.Expediente.Count(e => e.IdUsuario == usuario.IdUsuario);
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
                .Where(e => e.IdUsuario == usuario.IdUsuario)
                .OrderBy(e => e.IdExpediente)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return expedientes;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return (int)Math.Ceiling((double)_temp.Expediente.Count(e => e.IdUsuario == usuario.IdUsuario) / cantidadPorpagina);
        }
    }
}

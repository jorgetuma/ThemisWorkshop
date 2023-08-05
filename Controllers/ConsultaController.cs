using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ConsultaController : Controller
    {
        private ThemisworkshopContext _context;
        public static ThemisworkshopContext _temp;
        public static Usuario? usuario;

        public ConsultaController(ThemisworkshopContext context) 
        {
            _context = context;
            _temp = context;
        }
        
        [HttpGet]
        [Route("/Consulta/ListarConsultas/{pag}")]
        public ActionResult ListarConsultas(int pag)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            if (pag <= 0) 
            {
                pag = 1;
            }
            List<Consulta> consultas = LoadConsultas(pag);
            return View("ListarConsultas",consultas);
        }

        [HttpGet]
        [Route("/Consulta/AgregarConsulta/{idCita}")]
        public ActionResult AgregarConsulta(int idCita) 
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            Cita? cita  = _context.Cita.Find(idCita);
            if (cita != null)
            {
                Cliente? cliente = _context.Clientes.Find(cita.IdCliente);
                ConsultaViewModel model = new ConsultaViewModel(null, cita, null, cliente, false);
                return View("AgregarConsulta", model);
            }
            else
            { 
                return Redirect("/Cita/Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddConsulta() 
        { 
            int idCita = int.Parse(Request.Form["idCita"].ToString());
            int idExpediente = int.Parse(Request.Form["idExpediente"].ToString());
            TimeOnly horaini = TimeOnly.Parse(Request.Form["horainicial"].ToString());
            TimeOnly horafin = TimeOnly.Parse(Request.Form["horafinal"].ToString());
            decimal costo = decimal.Parse(Request.Form["costo"].ToString());

            var cita = _context.Cita.Find(idCita);
            cita.Realizado = true;
            cita.Fecha = DateTime.SpecifyKind(cita.Fecha,DateTimeKind.Utc);
            cita.Fecha = cita.Fecha.AddDays(1);
            _context.Cita.Update(cita);
            var consulta = new Consulta(idCita,idExpediente,usuario.IdUsuario,horaini,horafin,costo);
            _context.Consulta.Add(consulta);
            _context.SaveChanges();
            return Redirect("/Consulta/ListarConsultas/1");
        }

        [HttpGet]
        [Route("/Consulta/EliminarConsulta/{id}")]
        public ActionResult EliminarConsulta(int id) {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            Consulta? consulta = _context.Consulta.Find(id);
            if (consulta != null)
            {
                Cita? cita = _context.Cita.Find(consulta.IdCita);
                consulta.Eliminado = true;
                _context.Consulta.Update(consulta);

                cita.Realizado = false;
                cita.Fecha = DateTime.SpecifyKind(cita.Fecha, DateTimeKind.Utc);
                cita.Fecha = cita.Fecha.AddDays(1);
                _context.Cita.Update(cita);
                _context.SaveChanges();
                return Redirect("/Consulta/ListarConsultas/1");
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
            if (usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            return View("Error");
        }

        private int CantidadConsulta()
        {
            return _context.Consulta.Count(e => e.Eliminado == false && e.IdUsuario == usuario.IdUsuario);
        }

        private List<Consulta> LoadConsultas(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadConsulta();

            if (indIni >= cantidad)
            {
                return new List<Consulta>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Consulta> consultas = _context.Consulta
                .Where(e => e.Eliminado == false && e.IdUsuario == usuario.IdUsuario)
                .OrderBy(e => e.IdConsulta)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return consultas;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return (int)Math.Ceiling((double)_temp.Consulta.Count(e => e.Eliminado == false && e.IdUsuario == usuario.IdUsuario) / cantidadPorpagina);
        }
    }
}

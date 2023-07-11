using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ThemisworkshopContext _context;
        public static ThemisworkshopContext _temp; /*Para uso exclusivo en el frontend*/
        public static Usuario? usuario;

        public EmpleadoController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = context;
        }

        [HttpGet]
        [Route("Empleado/ListarEmpleados/{pag}")]
        public ActionResult ListarEmpleados(int pag)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Abogado) || usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            if (pag <= 0) 
            {
                pag = 1;
            }
            List<Usuario> usuarios = LoadEmpleados(pag);
            return View("ListarEmpleados", usuarios);
        }

        [HttpGet]
        [Route("Empleado/AgregarEmpleado")]
        public ActionResult AgregarEmpleado()
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Abogado) || usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            return View("AgregarEmpleado");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmpleado()
        {
            String userName = Request.Form["usuario"].ToString();
            String password = Request.Form["password"].ToString();
            String nombre = Request.Form["nombre"].ToString();
            String apellido = Request.Form["apellido"].ToString();
            String cedula = Request.Form["docIdentidad"].ToString();
            String sexo = Request.Form["sexo"].ToString();
            String estadoCivil = Request.Form["estadoCivil"].ToString();
            String pais = Request.Form["nacionalidad"].ToString();
            String correo = Request.Form["correo"].ToString();
            String telefono = Request.Form["telefono"].ToString();
            String direccion = Request.Form["direccion"].ToString();
            DateTime fechanacimiento = DateTime.Parse(Request.Form["fecha"].ToString());
            DateTime fechaUtc = DateTime.SpecifyKind(fechanacimiento, DateTimeKind.Utc);
            fechaUtc = fechaUtc.AddDays(1);
            string especialidad = "N/A";
            decimal sueldo = decimal.Parse(Request.Form["sueldo"].ToString());
            decimal incentivo = 0;
            decimal comision = 0;
            int rol = int.Parse(Request.Form["rol"].ToString());

            if (rol == 0)
            {
                especialidad = Request.Form["especialidad"].ToString();
                incentivo = decimal.Parse(Request.Form["incentivo"].ToString());
                comision = decimal.Parse(Request.Form["comision"].ToString());
            }
            else if (rol == 1)
            {
                especialidad = Request.Form["especialidad"].ToString();
                incentivo = decimal.Parse(Request.Form["incentivo"].ToString());
                comision = decimal.Parse(Request.Form["comision"].ToString());
            }

            var user = new Usuario(nombre, apellido, userName, password, cedula, sexo, estadoCivil, pais, correo, telefono, fechaUtc, direccion, rol, sueldo, especialidad, incentivo, comision);
            _context.Usuario.Add(user);
            _context.SaveChanges();
            return RedirectToAction("AgregarEmpleado");
        }

        [HttpGet]
        [Route("Empleado/ModificarEmpleado/{id}")]
        public ActionResult ModificarEmpleado(int id)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Abogado) || usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            Usuario? user = _context.Usuario.Find(id);
            if (user!=null) 
            {
                return View("AgregarEmpleado", user);
            }
            else 
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModEmpleado()
        {
            int id = int.Parse(Request.Form["id"].ToString());
            Usuario? user = _context.Usuario.Find(id);
            if (user != null)
            {
                String userName = Request.Form["usuario"].ToString();
                String password = Request.Form["password"].ToString();
                String nombre = Request.Form["nombre"].ToString();
                String apellido = Request.Form["apellido"].ToString();
                String cedula = Request.Form["docIdentidad"].ToString();
                String sexo = Request.Form["sexo"].ToString();
                String estadoCivil = Request.Form["estadoCivil"].ToString();
                String pais = Request.Form["nacionalidad"].ToString();
                String correo = Request.Form["correo"].ToString();
                String telefono = Request.Form["telefono"].ToString();
                String direccion = Request.Form["direccion"].ToString();
                DateTime fechanacimiento = DateTime.Parse(Request.Form["fecha"].ToString());
                DateTime fechaUtc = DateTime.SpecifyKind(fechanacimiento, DateTimeKind.Utc);
                fechaUtc = fechaUtc.AddDays(1);
                string especialidad = "N/A";
                decimal sueldo = decimal.Parse(Request.Form["sueldo"].ToString());
                decimal incentivo = 0;
                decimal comision = 0;
                int rol = int.Parse(Request.Form["rol"].ToString());

                if (rol == 0)
                {
                    especialidad = Request.Form["especialidad"].ToString();
                    incentivo = decimal.Parse(Request.Form["incentivo"].ToString());
                    comision = decimal.Parse(Request.Form["comision"].ToString());
                }
                else if (rol == 1)
                {
                    especialidad = Request.Form["especialidad"].ToString();
                    incentivo = decimal.Parse(Request.Form["incentivo"].ToString());
                    comision = decimal.Parse(Request.Form["comision"].ToString());
                }

                user.UserName = userName; 
                user.Password = password;
                user.Nombre = nombre;
                user.Apellido = apellido;
                user.Cedula = cedula;
                user.Sexo = sexo;
                user.EstadoCivil = estadoCivil;
                user.Pais = pais;
                user.Correo = correo;
                user.Telefono = telefono;
                user.Direccion = direccion;
                user.Fechanacimiento = fechaUtc;
                user.Especialidad = especialidad;
                user.Rol = rol;
                user.Sueldo = sueldo;
                user.Incentivo = incentivo;
                user.Comision = comision;
                _context.Update(user);
                _context.SaveChanges();
                return Redirect("/Empleado/ListarEmpleados/1");
            }
            else 
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("Empleado/EliminarEmpleado/{id}")]
        public ActionResult EliminarEmpleado(int id)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Abogado) || usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            Usuario? user = _context.Usuario.Find(id);
            if (user != null)
            {
                user.Eliminado = true;
                user.Fechanacimiento = DateTime.SpecifyKind(user.Fechanacimiento, DateTimeKind.Utc);
                user.Fechanacimiento = user.Fechanacimiento.AddDays(1);
                _context.Update(user);
                _context.SaveChanges(true);
                return Redirect("/Empleado/ListarEmpleados/1");
            }
            else 
            {
                return View("Error");
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
            if (usuario.Rol == ((int)Rolesapp.Abogado) || usuario.Rol == ((int)Rolesapp.Secretario))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            return View("Error");
        }

        private int CantidadEmpleados()
        {
            return _context.Usuario.Count(e => e.Eliminado == false);
        }

        private List<Usuario> LoadEmpleados(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadEmpleados();

            if (indIni >= cantidad)
            {
                return new List<Usuario>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Usuario> servicios = _context.Usuario
                .Where(e => e.Eliminado == false)
                .OrderBy(e => e.IdUsuario)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return servicios;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return (int)Math.Ceiling((double)_temp.Usuario.Count(e => e.Eliminado == false) / cantidadPorpagina);
        }
    }
}

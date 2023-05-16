using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ThemisworkshopContext _context;

        public EmpleadoController(ThemisworkshopContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Empleado/ListarEmpleados")]
        public ActionResult ListarEmpleados()
        {
            List<Usuario> usuarios = _context.Usuario.Where(e => e.Eliminado == false).ToList();
            return View("ListarEmpleados", usuarios);
        }

        [HttpGet]
        [Route("Empleado/AgregarEmpleado")]
        public ActionResult AgregarEmpleado()
        {
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
            Usuario user = _context.Usuario.Find(id);
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
            Usuario user = _context.Usuario.Find(id);
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

                Console.WriteLine(rol);

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
                return RedirectToAction("ListarEmpleados");
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
            Usuario user = _context.Usuario.Find(id);
            if (user != null)
            {
                user.Eliminado = true;
                user.Fechanacimiento = DateTime.SpecifyKind(user.Fechanacimiento, DateTimeKind.Utc);
                _context.Update(user);
                _context.SaveChanges(true);
                return RedirectToAction("ListarEmpleados");
            }
            else 
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View("Error");
        }
    }
}

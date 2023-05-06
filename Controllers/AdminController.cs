using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class AdminController : Controller
    {
        private readonly ThemisworkshopContext _context;

        public AdminController(ThemisworkshopContext context)
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

        [HttpGet]
        [Route("Empleado/ModificarEmpleado/{id}")]
        public ActionResult ModificarEmpleado(int id)
        {
            Usuario user = null;
            return View("AgregarEmpleado", user);
        }

        [HttpGet]
        [Route("Empleado/EliminarEmpleado/{id}")]
        public ActionResult EliminarEmpleado(int id)
        {
            return RedirectToAction("ListarEmpleados");
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View("Error");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ThemisworkshopContext _context;

        public ClienteController(ThemisworkshopContext context)
        {
            _context = context;
        }

        // Acción GET para listar todos los clientes
        [HttpGet]
        public ActionResult ListarClientes()
        {

            List<Cliente> clientes = _context.Clientes.ToList();

            // Devolver una vista con la lista de clientes
            return View("ListarClientes", clientes);
        }

        // Acción GET para mostrar el formulario de agregar un nuevo cliente
        [HttpGet]
        public ActionResult AgregarCliente()
        {
            // Devolver una vista con el formulario de agregar cliente vacío
            return View("AgregarCliente");
        }

        // Acción POST para agregar un nuevo cliente
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCliente(String nombre, String apellido, String cedula, char sexo, String pais, String correo, String telefono, DateTime fechanacimiento)
        {
            var cliente = new Cliente(nombre, apellido, cedula, sexo, pais, correo, telefono, fechanacimiento);

            _context.Clientes.Add(cliente);
            _context.SaveChanges();


            // Redirigir a la vista para registrar otro cliente
            return RedirectToAction("AgregarCliente");

        }*/

         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult AddCliente()
         {
             String nombre = Request.Form["nombre"].ToString();
             String apellido = Request.Form["apellido"].ToString(); 
             String cedula = Request.Form["docIdentidad"].ToString(); 
             char sexo = Request.Form["sexo"].ToString().First();
            String estadoCivil = Request.Form["estadoCivil"].ToString();
            String pais = Request.Form["nacionalidad"].ToString(); 
             String correo = Request.Form["correo"].ToString(); 
             String telefono = Request.Form["telefono"].ToString();
            DateTime fechanacimiento = DateTime.Parse(Request.Form["fecha"].ToString()).Date;
            DateTime fechaUtc = DateTime.SpecifyKind(fechanacimiento, DateTimeKind.Utc);
            
            var cliente = new Cliente(nombre, apellido, cedula, sexo, estadoCivil, pais, correo, telefono, fechaUtc);

             _context.Clientes.Add(cliente);
             _context.SaveChanges();


             // Redirigir a la vista para registrar otro cliente
             return RedirectToAction("AgregarCliente");

         }

        // Acción GET para mostrar el formulario de modificar un cliente existente por ID
        [HttpGet]
        public ActionResult ModificarCliente(String id)
        {

            Cliente cliente = null;

            if (cliente != null)
            {
                // Devolver una vista con el formulario de modificar cliente y los detalles del cliente
                return View("ModificarCliente", cliente);
            }
            else
            {
                // Devolver una vista de error si no se encontró el cliente
                return View("Error");
            }
        }

        // Acción POST para modificar un cliente existente por ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModCliente(int id)
        {
            Cliente cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Update(cliente);
                List<Cliente> clientes = _context.Clientes.ToList();
                // Redirigir a la vista de listar clientes
                return RedirectToAction("ListarClientes", clientes);
            }
            else
            {
                // Error
                return View("Error");
            }
        }

        // Acción GET para eliminar un cliente existente por ID
        [HttpGet]
        public ActionResult EliminarCliente(int id)
        {
            Cliente cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Remove(cliente);
                List<Cliente> clientes = _context.Clientes.ToList();
                return View("ListarClientes", clientes);
            }
            else
            {
                // Redirigir a una vista que liste todos los clientes
                return RedirectToAction("Error");
            }
        }

        //Vista para manejar errores
        [HttpGet]
        public ActionResult Error() { 
        return View("Error");
        }
    }
}

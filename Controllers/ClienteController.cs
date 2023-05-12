using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ThemisworkshopContext _context;
        static ThemisworkshopContext _temp; /*Para uso exclusivo en el frontend*/


        public ClienteController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = context;
        }

        // Acción GET para listar todos los clientes
        [HttpGet]
        [Route("Cliente/ListarClientes/{pag}")]
        [Route("Cliente/ListarClientes")]
        public ActionResult ListarClientes(int pag)
        {
            if (pag <= 0)
            {
                pag = 1;
            }
            List<Cliente> clientes = LoadClientes(pag);
            int cantidadPag = ObtenerPaginas(10);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCliente()
        {
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

            var cliente = new Cliente(nombre, apellido, cedula, sexo, estadoCivil, pais, correo, telefono, fechaUtc, direccion);

            _context.Clientes.Add(cliente);
            _context.SaveChanges();


            // Redirigir a la vista para registrar otro cliente
            return RedirectToAction("AgregarCliente");

        }

        // Acción GET para mostrar el formulario de modificar un cliente existente por ID
        [HttpGet]
        [Route("Cliente/ModificarCliente/{id}")]
        public ActionResult ModificarCliente(int id)
        {

            int idClienteSelecionado = id;
            Cliente cliente = _context.Clientes.Find(idClienteSelecionado);

            if (cliente != null)
            {
                // Devolver una vista con el formulario de modificar cliente y los detalles del cliente
                return View("AgregarCliente", cliente);
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
        public ActionResult ModCliente()
        {
            int idClienteSelecionado = int.Parse(Request.Form["id"].ToString());
            Cliente cliente = _context.Clientes.Find(idClienteSelecionado);
            if (cliente != null)
            {
                String nombre = Request.Form["nombre"].ToString();
                String apellido = Request.Form["apellido"].ToString();
                String cedula = Request.Form["docIdentidad"].ToString();
                String sexo = Request.Form["sexo"].ToString();
                String estadoCivil = Request.Form["estadoCivil"].ToString();
                String pais = Request.Form["nacionalidad"].ToString();
                String correo = Request.Form["correo"].ToString();
                String telefono = Request.Form["telefono"].ToString();
                String direccion = Request.Form["direccion"].ToString();
                DateTime fechanacimiento = DateTime.Parse(Request.Form["fecha"].ToString()).Date;
                DateTime fechaUtc = DateTime.SpecifyKind(fechanacimiento, DateTimeKind.Utc);
                fechaUtc =  fechaUtc.AddDays(1);

                cliente.Nombre = nombre;
                cliente.Apellido = apellido;
                cliente.Cedula = cedula;
                cliente.Sexo = sexo;
                cliente.EstadoCivil = estadoCivil;
                cliente.Pais = pais;
                cliente.Correo = correo;
                cliente.Telefono = telefono;
                cliente.Direccion = direccion;
                cliente.Fechanacimiento = fechaUtc;

                _context.Clientes.Update(cliente);
                _context.SaveChanges(true);

                // Redirigir a la vista de listar clientes
                return RedirectToAction("ListarClientes");
            }
            else
            {
                // Error
                return View("Error");
            }
        }

        // Acción GET para eliminar un cliente existente por ID
        [HttpGet]
        [Route("Cliente/EliminarCliente/{id}")]
        public ActionResult EliminarCliente(int id)
        {
            int idClienteSelecionado = id;
            Cliente cliente = _context.Clientes.Find(idClienteSelecionado);
            if (cliente != null)
            {
                cliente.Eliminado = true;
                cliente.Fechanacimiento = DateTime.SpecifyKind(cliente.Fechanacimiento, DateTimeKind.Utc);
                _context.Update(cliente);
                _context.SaveChanges();
                return RedirectToAction("ListarClientes");
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

        private int CantidadClientes() { 
        return _context.Clientes.Count(e => e.Eliminado == false);
        }

        private int ObtenerPaginas(int cantidadPorpagina) {
            return CantidadClientes() / cantidadPorpagina;
        }

        private List<Cliente> LoadClientes(int numPag) {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadClientes();

            if (indIni >= cantidad)
            {
                return new List<Cliente>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Cliente> clientes = _context.Clientes
                .Where(e => e.Eliminado == false)
                .OrderBy(e => e.IdClientes)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return clientes;

        }

        public  static int ObtenerPaginasFronted(int cantidadPorpagina) {
            return _temp.Clientes.Count(e => e.Eliminado == false)/cantidadPorpagina;
        }
    }
}

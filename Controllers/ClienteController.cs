using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ThemisworkshopContext _context;
        public static ThemisworkshopContext _temp; /*Para uso exclusivo en el frontend*/
        public static Usuario? usuario;


        public ClienteController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = context;
        }

        // Acción GET para listar todos los clientes
        [HttpGet]
        [Route("Cliente/ListarClientes/{pag}")]
        public ActionResult ListarClientes(int pag)
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
            List<Cliente> clientes = LoadClientes(pag);

            // Devolver una vista con la lista de clientes
            return View("ListarClientes", clientes);
        }

        // Acción GET para mostrar el formulario de agregar un nuevo cliente
        [HttpGet]
        public ActionResult AgregarCliente()
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Abogado))
            {
                Response.StatusCode = 403;
                 return Redirect("/"+ Response.StatusCode.ToString());
            }
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
            String telefono2 = Request.Form["telefono2"].ToString();
            String tipo = Request.Form["tipo"].ToString();
            DateTime fechanacimiento = DateTime.Parse(Request.Form["fecha"].ToString());
            DateTime fechaUtc = DateTime.SpecifyKind(fechanacimiento, DateTimeKind.Utc);
            fechaUtc = fechaUtc.AddDays(1);

            Cliente cliente;
            if (!tipo.Equals("Moral"))
            {
                cliente = new Cliente(nombre, apellido, cedula, sexo, estadoCivil, pais, correo, telefono, fechaUtc, direccion, telefono2, tipo);
            }
            else 
            { 
             cliente = new Cliente(nombre,cedula,pais,correo,telefono,telefono2,tipo,direccion);
            }

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
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Abogado))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            int idClienteSelecionado = id;
            Cliente? cliente = _context.Clientes.Where(e => e.IdClientes == idClienteSelecionado && e.Eliminado == false).FirstOrDefault();

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
            Cliente? cliente = _context.Clientes.Find(idClienteSelecionado);
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
                String telefono2 = Request.Form["telefono2"].ToString();
                String tipo = Request.Form["tipo"].ToString();
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
                cliente.Telefono2 = telefono2;
                cliente.Tipo = tipo;
                cliente.Fechanacimiento = fechaUtc;

                _context.Clientes.Update(cliente);
                _context.SaveChanges(true);

                // Redirigir a la vista de listar clientes
                return Redirect("/Cliente/ListarClientes/1");
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
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (usuario.Rol == ((int)Rolesapp.Abogado))
            {
                Response.StatusCode = 403;
                return Redirect("/" + Response.StatusCode.ToString());
            }
            int idClienteSelecionado = id;
            Cliente? cliente = _context.Clientes.Find(idClienteSelecionado);
            if (cliente != null)
            {
                if (cliente.Credito <= 0)
                {
                    cliente.Eliminado = true;
                    cliente.Fechanacimiento = DateTime.SpecifyKind(cliente.Fechanacimiento, DateTimeKind.Utc);
                    cliente.Fechanacimiento = cliente.Fechanacimiento.AddDays(1);
                    _context.Update(cliente);
                    _context.SaveChanges();
                    return Redirect("/Cliente/ListarClientes/1");
                }
                else 
                {
                  return RedirectToAction("CreditoPendiente");    
                }
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
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            return View("Error");
        }

        [HttpGet]
        public ActionResult CreditoPendiente() 
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            return View("CreditoPendiente");
        }

        private int CantidadClientes() { 
        return _context.Clientes.Count(e => e.Eliminado == false);
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
            return (int)Math.Ceiling((double)_temp.Clientes.Count(e => e.Eliminado == false)/cantidadPorpagina);
        }
    }
}

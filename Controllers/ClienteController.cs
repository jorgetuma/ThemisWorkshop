using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ClienteController : Controller
    {

        // Acción GET para listar todos los clientes
        [HttpGet]
        public ActionResult ListarClientes()
        {

            List<Cliente> clientes = FakeDB.getInstance().ObtenerClientes();

            // Devolver una vista con la lista de clientes
            return View("ListarClientes",clientes);
        }

        // Acción GET para mostrar los detalles de un cliente por ID
        [HttpGet]
        public ActionResult MostrarCliente(int id)
        {
            List<Cliente> clientes = FakeDB.getInstance().ObtenerClientes();

            // Código para obtener el cliente con el ID dado
            Cliente cliente = clientes.FirstOrDefault(c => c.id == id);

            if (cliente != null)
            {
                // Devolver una vista con los detalles del cliente
                return View("ListarClientes",cliente);
            }
            else
            {
                // Devolver una vista de error si no se encontró el cliente
                return View("Error");
            }
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
        public ActionResult AgregarCliente(Cliente nuevoCliente)
        {

            List<Cliente> clientes = FakeDB.getInstance().ObtenerClientes();
            if (ModelState.IsValid)
            {
                // Generar un nuevo ID para el cliente y agregarlo a la lista
                nuevoCliente.id = clientes.Count + 1;
                clientes.Add(nuevoCliente);
                FakeDB.getInstance().AgregarCliente(nuevoCliente);

                // Redirigir a una vista que muestre los detalles del nuevo cliente
                return RedirectToAction("ListarCliente", clientes);
            }
            else
            {
                // Error
                return View("Error");
            }
        }

        // Acción GET para mostrar el formulario de modificar un cliente existente por ID
        [HttpGet]
        public ActionResult ModificarCliente(int id)
        {
            List<Cliente> clientes = FakeDB.getInstance().ObtenerClientes();

            // Código para obtener el cliente con el ID dado
            Cliente cliente = clientes.FirstOrDefault(c => c.id == id);

            if (cliente != null)
            {
                // Devolver una vista con el formulario de modificar cliente y los detalles del cliente
                return View("ModificarCliente",cliente);
            }
            else
            {
                // Devolver una vista de error si no se encontró el cliente
                return View("Error");
            }
        }

        // Acción POST para modificar un cliente existente por ID
        [HttpPost]
        public ActionResult ModificarCliente(Cliente clienteModificado)
        {
            List<Cliente> clientes = FakeDB.getInstance().ObtenerClientes();

            if (ModelState.IsValid)
            {
                // Código para modificar el cliente existente en la lista
                int indice = clientes.FindIndex(c => c.id == clienteModificado.id);
                clientes[indice] = clienteModificado;
                FakeDB.getInstance().ModificarCliente(clienteModificado);

                // Redirigir a una vista que muestre los detalles del cliente modificado
                return RedirectToAction("ListarClientes",clientes);
            }
            else
            {
                // Error
                return View("Error");
            }
        }

        // Acción GET para eliminar un cliente existente por ID
        [HttpGet]
        public ActionResult EliminarCliente(int id, FormCollection form)
        {
            List<Cliente> clientes = FakeDB.getInstance().ObtenerClientes();

            // Código para eliminar el cliente existente de la lista
            Cliente cliente = clientes.FirstOrDefault(c => c.id == id);
            if (cliente != null)
            {
                FakeDB.getInstance().EliminarCliente(cliente.id);
                clientes.Remove(cliente);
                return View("ListarClientes", clientes);
            }
            else
            {
                // Redirigir a una vista que liste todos los clientes
                return RedirectToAction("Error");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class ClienteController : Controller
    {
        private List<Cliente> clientes = FakeDB.getInstance().ObtenerClientes();

        // Acción GET para listar todos los clientes
        [HttpGet]
        public ActionResult ListarClientes()
        {
            // Devolver una vista con la lista de clientes
            return View(clientes);
        }

        // Acción GET para mostrar los detalles de un cliente por ID
        [HttpGet]
        public ActionResult MostrarCliente(int id)
        {
            // Código para obtener el cliente con el ID dado
            Cliente cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente != null)
            {
                // Devolver una vista con los detalles del cliente
                return View(cliente);
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
            return View(new Cliente());
        }

        // Acción POST para agregar un nuevo cliente
        [HttpPost]
        public ActionResult AgregarCliente(Cliente nuevoCliente)
        {
            if (ModelState.IsValid)
            {
                // Generar un nuevo ID para el cliente y agregarlo a la lista
                nuevoCliente.id = clientes.Count + 1;
                clientes.Add(nuevoCliente);

                // Redirigir a una vista que muestre los detalles del nuevo cliente
                return RedirectToAction("MostrarCliente", new { id = nuevoCliente.id });
            }
            else
            {
                // Devolver la vista de agregar cliente con los errores de validación
                return View("AgregarCliente", nuevoCliente);
            }
        }

        // Acción GET para mostrar el formulario de modificar un cliente existente por ID
        [HttpGet]
        public ActionResult ModificarCliente(int id)
        {
            // Código para obtener el cliente con el ID dado
            Cliente cliente = clientes.FirstOrDefault(c => c.id == id);

            if (cliente != null)
            {
                // Devolver una vista con el formulario de modificar cliente y los detalles del cliente
                return View(cliente);
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
            if (ModelState.IsValid)
            {
                // Código para modificar el cliente existente en la lista
                int indice = clientes.FindIndex(c => c.id == clienteModificado.id);
                clientes[indice] = clienteModificado;

                // Redirigir a una vista que muestre los detalles del cliente modificado
                return RedirectToAction("MostrarCliente", new { id = clienteModificado.id });
            }
            else
            {
                // Devolver la vista de modificar cliente con los errores de validación
                return View("ModificarCliente", clienteModificado);
            }
        }
        // Acción GET para mostrar el formulario de eliminar un cliente existente por ID
        [HttpGet]
        public ActionResult EliminarCliente(int id)
        {
            // Código para obtener el cliente con el ID dado
            Cliente cliente = clientes.FirstOrDefault(c => c.id == id);

            if (cliente != null)
            {
                // Devolver una vista con el formulario de eliminar cliente y los detalles del cliente
                return View(cliente);
            }
            else
            {
                // Devolver una vista de error si no se encontró el cliente
                return View("Error");
            }
        }

        // Acción POST para eliminar un cliente existente por ID
        [HttpPost]
        public ActionResult EliminarCliente(int id, FormCollection form)
        {
            // Código para eliminar el cliente existente de la lista
            Cliente cliente = clientes.FirstOrDefault(c => c.id == id);
            clientes.Remove(cliente);

            // Redirigir a una vista que liste todos los clientes
            return RedirectToAction("ListarClientes");
        }
    }
}

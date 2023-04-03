using System.ComponentModel;

namespace ThemisWorkshop.Models
{
    public class FakeDB
    {
        private List<Cliente> clientes { get; set; }
        private static FakeDB? instance = null; 

        private FakeDB()
        {
            this.clientes = new List<Cliente>();
        }

        public static FakeDB getInstance() {
            if (instance == null)
            {
                instance = new FakeDB();
            }
           return instance;
        }

        public void AgregarCliente(Cliente cliente)
        {
            int nuevoId = 1;
            if (clientes.Count > 0)
            {
                nuevoId = clientes.Max(c => c.id) + 1;
            }
            cliente.id = nuevoId;

            clientes.Add(cliente);
        }

        public void ModificarCliente(Cliente clienteModificado)
        {
            Cliente clienteOriginal = clientes.FirstOrDefault(c => c.id == clienteModificado.id);

            if (clienteOriginal != null)
            {
                clienteOriginal.Nombre = clienteModificado.Nombre;
                clienteOriginal.Apellido = clienteModificado.Apellido;
                clienteOriginal.telefono = clienteModificado.telefono;
                clienteOriginal.cedula = clienteModificado.cedula;
                clienteOriginal.correo = clienteModificado.correo;
                clienteOriginal.credito = clienteModificado.credito;
                clienteOriginal.FechaNacimiento = clienteOriginal.FechaNacimiento;

            }
        }

        public void EliminarCliente(int id)
        {
            Cliente cliente = clientes.FirstOrDefault(c => c.id == id);
            clientes.Remove(cliente);
        }

        public List<Cliente> ObtenerClientes()
        {
            return new List<Cliente>(clientes);
        }

        public Cliente ObtenerClientePorId(int id)
        {
            return clientes.FirstOrDefault(c => c.id == id);
        }
    }
}

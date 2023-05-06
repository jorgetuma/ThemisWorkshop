using System.ComponentModel.DataAnnotations;

namespace ThemisWorkshop.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public int Rol { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string Pais { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fechanacimiento { get; set; }

        public String Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public string? Correo { get; set; }

        public string Telefono { get; set; } = null!;

        public String Direccion { get; set; }

        public float Sueldo { get; set; }

        public String Especialidad { get; set; }

        public float Comision { get; set; }

        public float Incentivo { get; set; }

        public bool Eliminado { get; set; }

        public Usuario(string nombre, string apellido, string cedula, String sexo, string estadoCivil, string pais, string correo, string telefono, DateTime fechanacimiento, string direccion, float sueldo, string especialidad, float incentivo, float comision)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Cedula = cedula;
            this.Sexo = sexo;
            this.EstadoCivil = estadoCivil;
            this.Pais = pais;
            this.Correo = correo;
            this.Telefono = telefono;
            this.Fechanacimiento = fechanacimiento;
            this.Direccion = direccion;
            this.Sueldo = sueldo;
            this.Especialidad = especialidad;
            this.Incentivo = incentivo;
            this.Comision = comision;
            this.Eliminado = false;
        }

        public float CalcularSueldo() 
        {
            float sueldo = 0;
            switch (Rol) {
                case 1 :
                    sueldo = Sueldo; 
                    break;
                    case 2 :
                    sueldo = (Sueldo + Incentivo) + (Sueldo * Comision);                    
                   break;
            }
            return sueldo;
        }
    }
}

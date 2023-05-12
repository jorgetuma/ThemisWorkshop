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

        public decimal Sueldo { get; set; }

        public String Especialidad { get; set; }

        public decimal Comision { get; set; }

        public decimal Incentivo { get; set; }

        public bool Eliminado { get; set; }

        public Usuario(string nombre, string apellido, String userName,String password, string cedula, String sexo, string estadoCivil, string pais, string correo, string telefono, DateTime fechanacimiento, string direccion,int rol, decimal sueldo, string especialidad, decimal incentivo, decimal comision)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.UserName = userName;
            this.Password = password;
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

        public decimal CalcularSueldo() 
        {
            decimal sueldo = 0;
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

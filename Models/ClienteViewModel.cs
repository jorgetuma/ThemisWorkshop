using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ThemisWorkshop.Models
{
    public class ClienteViewModel
    {
        public int IdClientes { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo Nombre debe tener una longitud máxima de 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo Apellido debe tener una longitud máxima de 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo Cedula es obligatorio")]
        [StringLength(11, ErrorMessage = "El campo Cedula debe tener una longitud máxima de 11 caracteres")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El campo País es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo País debe tener una longitud máxima de 50 caracteres")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "El campo Fecha de nacimiento es obligatorio")]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime Fechanacimiento { get; set; }

        [Required(ErrorMessage = "El campo Sexo es obligatorio")]
        [StringLength(1, ErrorMessage = "El campo Sexo es obligatorio")]
        public string Sexo { get; set; }

        [StringLength(50, ErrorMessage = "El campo Estado Civil es obligatorio")]
        public string Estadocivil { get; set; }

        [EmailAddress(ErrorMessage = "El campo Correo debe ser una dirección de correo electrónico válida")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es obligatorio")]
        [Phone(ErrorMessage = "El campo Teléfono debe ser un número de teléfono válido")]
        public string Telefono { get; set; }

        [Display(Name = "Crédito")]
        [DataType(DataType.Currency)]
        public decimal? Credito { get; set; }

    }
}

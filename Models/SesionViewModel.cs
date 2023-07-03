using System.ComponentModel.DataAnnotations;

namespace ThemisWorkshop.Models
{
    public class SesionViewModel
    {
        [Required(ErrorMessage = "El campo Nombre de usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo Nombre de usuario debe tener una longitud máxima de 50 caracteres")]
        public string User { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo contraseña debe tener una longitud máxima de 50 caracteres")]
        public string Pass { get; set; }

        public SesionViewModel(string user,string pass) 
        { 
            this.User = user;
            this.Pass = pass;
        }
    }
}

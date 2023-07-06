using System.ComponentModel.DataAnnotations;

namespace ThemisWorkshop.Models
{
    public class SesionViewModel
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo Usuario debe tener una longitud máxima de 50 caracteres")]
        public string User { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo Contraseña debe tener una longitud máxima de 50 caracteres")]
        public string Pass { get; set; }

        public bool Error { get; set; }

        public SesionViewModel(string user,string pass,bool error) 
        { 
            this.User = user;
            this.Pass = pass;
            this.Error = error;
        }
    }
}

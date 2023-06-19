namespace ThemisWorkshop.Models
{
    public class TareaViewModel
    {
        public Tarea? Tarea { get; set; }

        public Usuario? Usuario { get; set; }

        public bool EsMod { get; set; }

        public TareaViewModel(Tarea? tarea,Usuario? usuario,bool esmod) { 
         this.Tarea = tarea;
         this.Usuario = usuario;
         this.EsMod = esmod;
        }
    }
}

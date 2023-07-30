namespace ThemisWorkshop.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        public string Nombre { get; set; }

        public bool Eliminado { get; set; }

        public Categoria(string nombre) 
        { 
            this.Nombre = nombre;
            this.Eliminado = false;
        }
    }
}

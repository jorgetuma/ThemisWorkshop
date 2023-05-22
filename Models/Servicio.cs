namespace ThemisWorkshop.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public decimal Preciofijo { get; set; }

    public bool Eliminado { get; set; }

    public Servicio(string nombre,string descripcion, decimal preciofijo) 
    {
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.Preciofijo = preciofijo;
        this.Eliminado = false;
    }
}

namespace ThemisWorkshop.Models;

public partial class Detalleservicio
{
    public int IdDetalleservicio { get; set; }

    public int IdServicio { get; set; }

    public int IdExpediente { get; set; }

    public bool Eliminado { get; set; }

    public bool Facturado { get; set; }

    public Detalleservicio(int idServicio, int idExpediente) 
    {
     this.IdServicio = idServicio;
    this.IdExpediente = idExpediente;
    this.Eliminado = false;
    this.Facturado = false;
    }
}

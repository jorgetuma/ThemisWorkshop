namespace ThemisWorkshop.Models;

public partial class Expediente
{
    public int IdExpediente { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public Cliente Cliente { get; set; }

    public Usuario Abogado { get; set; }

    public string Asunto { get; set; }

    public string Descripcion { get; set; }

    public bool Activo { get; set; }
    
    public Expediente(int idCliente, int idUsuario,string asunto, string descripcion) 
    {
        this.IdCliente = idCliente;
        this.IdUsuario = idUsuario;
        this.Asunto = asunto;
        this.Descripcion = descripcion;
        this.Activo = true;
    }
}

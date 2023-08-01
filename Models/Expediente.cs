using System.ComponentModel.DataAnnotations;

namespace ThemisWorkshop.Models;

public partial class Expediente
{
    public int IdExpediente { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public int IdCategoria { get; set; }

    public int Numeracion { get; set; }  

    public string Asunto { get; set; }

    public string Descripcion { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaApertura { get; set; }

    public bool Activo { get; set; }

    public Expediente(int idCliente, int idUsuario, string asunto, string descripcion, DateTime fechaApertura, int numeracion,int idCategoria)
    {
        this.IdCliente = idCliente;
        this.IdUsuario = idUsuario;
        this.Asunto = asunto;
        this.Descripcion = descripcion;
        this.Activo = true;
        this.FechaApertura = fechaApertura;
        this.Numeracion = numeracion;
        this.IdCategoria = idCategoria;
    }
}

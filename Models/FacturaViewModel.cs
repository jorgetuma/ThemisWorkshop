namespace ThemisWorkshop.Models
{
    public class FacturaViewModel
    {
        public int IdServicio { get; set; }

        public int IdConsulta { get; set; }

        public int IdExpediente { get; set; }

        public Factura? Factura { get; set; }

        public FacturaViewModel(int idServicio, int idConsulta,int idExpediente, Factura? factura) 
        { 
            this.Factura = factura;
            this.IdServicio = idServicio;
            this.IdConsulta = idConsulta;
            this.IdExpediente = idExpediente;
        }
    }
}

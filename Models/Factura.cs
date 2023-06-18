namespace ThemisWorkshop.Models
{
    public partial class Factura
    {
        public int IdFactura { get; set; }

        public int IdServicio { get; set; }

        public int IdCliente { get; set;}

        public decimal Costo { get; set;}

        public decimal MontoVariable { get; set;}

        public decimal MontoPorPagar { get; set;}

        public DateTime FechaEmision { get; set; }

        public DateTime FechaLimite { get; set; }

        public bool EsCredito { get; set; }

        public bool Eliminado { get; set; }

        public Factura(int idServicio, int idCliente,decimal costo, decimal montoVariable,DateTime fechaEmision,DateTime fechaLimite,bool esCredito) 
        { 
            this.IdServicio = idServicio;
            this.IdCliente = idCliente;
            this.Costo = costo;
            this.MontoVariable = montoVariable;
            this.MontoPorPagar = 0;
            this.FechaEmision = fechaEmision;
            this.FechaLimite = fechaLimite;
            this.EsCredito = esCredito;
            this.Eliminado = false;
        } 
    }
}

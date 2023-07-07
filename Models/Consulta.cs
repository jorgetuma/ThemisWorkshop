namespace ThemisWorkshop.Models
{
    public class Consulta
    {
        public int IdConsulta { get; set; }

        public int IdCita { get; set; }

        public int IdExpediente { get; set;}

        public int IdUsuario { get; set; }

        public decimal Precio { get; set;}

        public TimeOnly HoraInicial { get; set; }

        public TimeOnly HoraFinal { get; set; }

        public bool Eliminado { get; set; }

        public bool Facturado { get; set; }

        public Consulta(int idCita, int idExpediente,int idUsuario, TimeOnly horaInicial, TimeOnly horaFinal,decimal precio) 
        { 
            this.IdCita = idCita;
            this.IdExpediente = idExpediente;
            this.IdUsuario = idUsuario;
            this.HoraInicial = horaInicial;
            this.HoraFinal = horaFinal;
            this.Precio = precio;
            this.Eliminado = false;
            this.Facturado = false;
        }
    }
}

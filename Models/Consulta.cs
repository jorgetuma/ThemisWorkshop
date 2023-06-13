namespace ThemisWorkshop.Models
{
    public class Consulta
    {
        public int IdConsulta { get; set; }

        public int IdCita { get; set; }

        public int IdExpediente { get; set;}

        public decimal Precio { get; set;}

        public TimeOnly HoraInicial { get; set; }

        public TimeOnly HoraFinal { get; set; }

        public bool Eliminado { get; set; }

        public Consulta(int idCita, int idExpediente, TimeOnly horaInicial, TimeOnly horaFinal,decimal precio) 
        { 
            this.IdCita = idCita;
            this.IdExpediente = idExpediente;
            this.HoraInicial = horaInicial;
            this.HoraFinal = horaFinal;
            this.Precio = precio;
            this.Eliminado = false;
        }
    }
}

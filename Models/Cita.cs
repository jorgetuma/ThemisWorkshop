namespace ThemisWorkshop.Models
{
    public partial class Cita
    {
        public int IdCita { get; set; }

        public int IdCliente { get; set; }

        public int IdUsuario { get; set; }

        public string Asunto { get; set; }

        public string Lugar { get; set; }

        public string Descripcion { get; set; }
        
        public DateTime Fecha { get; set; }

        public TimeOnly HoraInicial { get; set; }

        public TimeOnly HoraFinal { get; set; }

        public bool Realizado { get; set; }
        
        public Cita(int idCliente, int idUsuario,string asunto,string lugar,string descripcion,DateTime fecha, TimeOnly horaInicial, TimeOnly horaFinal) 
        { 
        this.IdCliente = idCliente;
        this.IdUsuario = idUsuario;
        this.Asunto = asunto;
        this.Lugar = lugar;
        this.Descripcion = descripcion;
        this.Fecha = fecha;
        this.HoraInicial = horaInicial;
        this.HoraFinal = horaFinal;
        this.Realizado = false;
        }
    }
}

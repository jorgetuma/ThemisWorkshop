namespace ThemisWorkshop.Models
{
    public partial class Tarea
    {
        public int IdTarea { get; set; }

        public int IdUsuario { get; set; }

        public int IdExpediente { get; set; }

        public string Asunto { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public TimeOnly Hora { get; set; }

        public bool Realizado { get; set; }

        public Tarea(int idUsuario,int idExpediente, string asunto, string descripcion, DateTime fecha, TimeOnly hora)
        {
            this.IdUsuario = idUsuario;
            this.IdExpediente = idExpediente;
            this.Asunto = asunto;
            this.Descripcion = descripcion;
            this.Fecha = fecha;
            this.Hora = hora;
            this.Realizado = false;
        }
    }
}

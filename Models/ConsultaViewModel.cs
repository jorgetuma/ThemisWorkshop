namespace ThemisWorkshop.Models
{
    public class ConsultaViewModel
    {
        public Consulta? Consulta { get; set; }

        public Cita? Cita { get; set; }

        public Expediente? Expediente { get; set; }

        public bool? EsMod { get; set; }

        public ConsultaViewModel(Consulta? consulta,Cita? cita, Expediente? expediente, bool Esmod) 
        { 
            this.Consulta = consulta;
            this.Cita = cita;
            this.Expediente = expediente;
            this.EsMod = Esmod;
        }
        }
    }

namespace ThemisWorkshop.Models
{
    public class CitaViewModel
    {
        public Cita? Cita;

       public Cliente? Cliente { get; set; }

        public bool EsMod { get; set; }

        public CitaViewModel(Cita? cita,Cliente? cliente, bool esmod) 
        { 
            this.Cita = cita;
            this.Cliente = cliente;
            this.EsMod = esmod;
        }
    }
}

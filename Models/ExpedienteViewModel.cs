namespace ThemisWorkshop.Models
{
    public class ExpedienteViewModel
    {
        public Expediente Expediente { get; set; }

        public bool EsMod { get; set; }

        public ExpedienteViewModel(Expediente expediente,bool esMod) 
        {
            this.Expediente = expediente;
            this.EsMod = esMod;
        }
    }
}

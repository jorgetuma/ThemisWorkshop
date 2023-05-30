﻿namespace ThemisWorkshop.Models
{
    public class ExpedienteViewModel
    {
        public Expediente? Expediente { get; set; }

        public Cliente? Cliente { get; set; }

        public List<Servicio>? Servicios { get; set; }

        public bool EsMod { get; set; }

        public ExpedienteViewModel(Expediente? expediente,Cliente? Cliente,bool esMod) 
        {
            this.Expediente = expediente;
            this.Cliente = Cliente;
            this.EsMod = esMod;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThemisWorkshop.Models;

public partial class Cliente
{

    public Cliente(string nombre, string apellido, string cedula, char sexo,string estadoCivil, string pais, string correo, string telefono, DateTime fechanacimiento)
    {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Cedula = cedula;
        this.Sexo = sexo;
        this.EstadoCivil = estadoCivil;
        this.Pais = pais;
        this.Correo = correo;
        this.Telefono = telefono;
        this.Fechanacimiento = fechanacimiento;
        this.Credito = 0;
      
    }

    public int IdClientes { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Pais { get; set; } = null!;

    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Fechanacimiento { get; set; }
    
    public char Sexo { get; set; }

    public string EstadoCivil { get; set; } 

    public string? Correo { get; set; }

    public string Telefono { get; set; } = null!;

    public decimal? Credito { get; set; }
}

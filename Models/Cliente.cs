using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThemisWorkshop.Models;

public partial class Cliente
{
    public int IdClientes { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Pais { get; set; } = null!;

    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Fechanacimiento { get; set; }

    public String Sexo { get; set; }

    public string EstadoCivil { get; set; }

    public string? Correo { get; set; }

    public string Telefono { get; set; } = null!;

    public String Direccion { get; set; }

    public decimal? Credito { get; set; }

    public bool Eliminado { get; set; }

    public Cliente(string nombre, string apellido, string cedula, String sexo,string estadoCivil, string pais, string correo, string telefono, DateTime fechanacimiento, string direccion)
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
        this.Eliminado = false;
        this.Direccion = direccion;
    }
}

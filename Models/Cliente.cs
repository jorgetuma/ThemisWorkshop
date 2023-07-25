using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThemisWorkshop.Models;

public partial class Cliente
{
    public int IdClientes { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; } 

    public string Cedula { get; set; } = null!;

    public string? Pais { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Fechanacimiento { get; set; }

    public String? Sexo { get; set; }

    public string? EstadoCivil { get; set; }

    public string? Correo { get; set; }

    public string Telefono { get; set; } = null!;

    public string? Telefono2 { get; set; }

    public String Direccion { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal? Credito { get; set; }

    public bool Eliminado { get; set; }

    public Cliente(string nombre, string apellido, string cedula, String sexo,string estadoCivil, string pais, string correo, string telefono, DateTime fechanacimiento, string direccion,string telefono2,string tipo)
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
        this.Telefono2 = telefono2;
        this.Tipo = tipo;
    }

    public Cliente(string nombre,string cedula, string pais, string correo, string telefono, string telefono2, string tipo, string direccion) 
    {
        this.Nombre = nombre;
        this.Cedula = cedula;
        this.Pais = pais;
        this.Correo = correo;
        this.Telefono = telefono;
        this.Credito = 0;
        this.Eliminado = false;
        this.Direccion = direccion;
        this.Telefono2 = telefono2;
        this.Tipo = tipo;

    }
}

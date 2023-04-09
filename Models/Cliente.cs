using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThemisWorkshop.Models;

public partial class Cliente
{

    public Cliente(string nombre, string apellido, string docIdentidad, char sexo, string nacionalidad, string correo, string telefono, DateTime fechanacimiento)
    {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Cedula = docIdentidad;
        this.Sexo = sexo;
        this.Pais = nacionalidad;
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

    public string Estadocivil { get; set; } = null!;

    public string? Correo { get; set; }

    public string Telefono { get; set; } = null!;

    public decimal? Credito { get; set; }
}

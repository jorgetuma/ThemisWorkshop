namespace ThemisWorkshop.Models
{
   public class Cliente 
   {
    public int id {get;set;}
    public string Nombre{get;set;}
    public string Apellido{get;set;}
    public string cedula{get;set;}
    public string lugarNacimiento{get;set;}
    public DateTime FechaNacimiento{get;set;}
    public string nacionalidad{get;set;}
    public char sexo{get;set;}
    public string estadoCivil{get;set;}
    public string correo{get;set;}
    public string telefono{get;set;}
    public float credito{get;set;}

    public Cliente(string Nombre, string Apellido, string cedula, string lugarNacimiento,DateTime FechaNacimiento, string nacionalidad,char sexo, string estadoCivil,string correo,string telefono)
    {
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.cedula =cedula;
        this.lugarNacimiento = lugarNacimiento;
        this.FechaNacimiento = FechaNacimiento;
        this.nacionalidad = nacionalidad;
        this.sexo = sexo;
        this.estadoCivil = estadoCivil;
        this.correo =correo;
        this.telefono = telefono;
        this.credito = 0;
    }

   } 

}
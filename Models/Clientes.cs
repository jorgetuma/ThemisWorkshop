namespace ThemisWorkshop.Models
{
   public class Clientes 
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

    public Clientes(string Nombre, string Apellido, string cedula, string lugarNacimiento,DateTime FechaNacimiento, string nacionalidad,char sexo, string estadoCivil,string correo,string telefono){
        Nombre = Nombre;
        Apellido = Apellido;
        cedula =cedula;
        lugarNacimiento = lugarNacimiento;
        FechaNacimiento = FechaNacimiento;
        nacionalidad = nacionalidad;
        sexo = sexo;
        estadoCivil = estadoCivil;
        correo =correo;
        telefono= telefono;
    }

   } 

}
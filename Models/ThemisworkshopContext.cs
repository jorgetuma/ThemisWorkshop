using Microsoft.EntityFrameworkCore;

namespace ThemisWorkshop.Models;

public partial class ThemisworkshopContext : DbContext
{
    public ThemisworkshopContext()
    {
    }

    public ThemisworkshopContext(DbContextOptions<ThemisworkshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Expediente> Expediente { get; set; }

    public virtual DbSet<Servicio> Servicio { get; set; }

    public virtual DbSet<Detalleservicio> Detalleservicio { get; set; }

    public virtual DbSet<Cita> Cita { get; set; }

    public virtual DbSet<Consulta> Consulta { get; set; }

    public virtual DbSet<Factura> Factura { get; set; }

    public virtual DbSet<Tarea> Tarea { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdClientes).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.Property(e => e.IdClientes).HasColumnName("id_clientes");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(15)
                .HasColumnName("cedula");
            entity.Property(e => e.Correo)
                .HasMaxLength(101)
                .HasColumnName("correo");
            entity.Property(e => e.Credito)
                .HasColumnType("money")
                .HasColumnName("credito");
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(15)
                .HasColumnName("estadocivil");
            entity.Property(e => e.Fechanacimiento).HasColumnName("fechanacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(40)
                .HasColumnName("pais");
            entity.Property(e => e.Sexo)
                .HasMaxLength(9)
                .HasColumnName("sexo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
            entity.Property(e => e.Direccion)
            .HasMaxLength(101)
            .HasColumnName("direccion");
            entity.Property(e => e.Eliminado)
            .HasColumnName("eliminado");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.UserName)
            .HasMaxLength(50)
            .HasColumnName("username");
            entity.Property(e => e.Password)
            .HasMaxLength(50)
            .HasColumnName("contrasena");
            entity.Property(e => e.Cedula)
                .HasMaxLength(15)
                .HasColumnName("cedula");
            entity.Property(e => e.Correo)
                .HasMaxLength(101)
                .HasColumnName("correo");
            entity.Property(e => e.Sueldo)
                .HasColumnType("money")
                .HasColumnName("sueldo");
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(15)
                .HasColumnName("estadocivil");
            entity.Property(e => e.Fechanacimiento).HasColumnName("fechanacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(40)
                .HasColumnName("pais");
            entity.Property(e => e.Sexo)
                .HasMaxLength(9)
                .HasColumnName("sexo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
            entity.Property(e => e.Direccion)
            .HasMaxLength(101)
            .HasColumnName("direccion");
            entity.Property(e => e.Especialidad)
            .HasMaxLength(50)
            .HasColumnName("especialidad");
            entity.Property(e => e.Incentivo)
            .HasColumnType("money")
            .HasColumnName("incentivo");
            entity.Property(e => e.Comision)
            .HasColumnName("comision");
            entity.Property(e => e.Rol)
            .HasColumnName("rol");
            entity.Property(e => e.Eliminado)
            .HasColumnName("eliminado");
        });

        modelBuilder.Entity<Expediente>(entity =>
        {
            entity.HasKey(e => e.IdExpediente).HasName("expediente_pkey");

            entity.ToTable("expediente");

            entity.Property(e => e.IdExpediente).HasColumnName("id_expediente");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Asunto)
                .HasMaxLength(60)
                .HasColumnName("asunto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaApertura).HasColumnName("fechaapertura");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("servicio_pkey");

            entity.ToTable("servicio");

            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Preciofijo)
                .HasColumnType("money")
                .HasColumnName("preciofijo");

            entity.Property(e => e.Eliminado)
           .HasColumnName("eliminado");
        });

        modelBuilder.Entity<Detalleservicio>(entity =>
        {
            entity.HasKey(e => e.IdDetalleservicio).HasName("detalleservicio_pkey");

            entity.ToTable("detalleservicio");

            entity.Property(e => e.IdDetalleservicio).HasColumnName("id_detalleservicio");
            entity.Property(e => e.IdExpediente).HasColumnName("id_expediente");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.Facturado).HasColumnName("facturado");
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("id_cita_pkey");
            entity.ToTable("cita");

            entity.Property(e => e.IdCita).HasColumnName("id_cita");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Asunto)
                .HasMaxLength(60)
                .HasColumnName("asunto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Lugar)
                .HasMaxLength(60)
                .HasColumnName("lugar");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraInicial).HasColumnName("horainicial");
            entity.Property(e => e.HoraFinal).HasColumnName("horafinal");
            entity.Property(e => e.Realizado).HasColumnName("realizado");
        });

        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.IdConsulta).HasName("id_consulta_pkey");
            entity.ToTable("consulta");

            entity.Property(e => e.IdConsulta).HasColumnName("id_consulta");
            entity.Property(e => e.IdCita).HasColumnName("id_cita");
            entity.Property(e => e.IdExpediente).HasColumnName("id_expediente");
            entity.Property(e => e.HoraInicial).HasColumnName("horainicial");
            entity.Property(e => e.HoraFinal).HasColumnName("horafinal");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.Facturado).HasColumnName("facturado");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("id_factura_pkey");
            entity.ToTable("factura");

            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.FechaEmision).HasColumnName("fechaemision");
            entity.Property(e => e.FechaLimite).HasColumnName("fechalimite");
            entity.Property(e => e.Costo)
                .HasColumnType("money")
                .HasColumnName("costo");
            entity.Property(e => e.MontoVariable)
                .HasColumnType("money")
                .HasColumnName("montovariable");
            entity.Property(e => e.MontoPorPagar)
                .HasColumnType("money")
                .HasColumnName("montoporpagar");
            entity.Property(e => e.EsCredito).HasColumnName("escredito");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.IdTarea).HasName("id_tarea_pkey");
            entity.ToTable("tarea");

            entity.Property(e => e.IdTarea).HasColumnName("id_tarea");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Asunto)
            .HasMaxLength(60)
                .HasColumnName("asunto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.Realizado).HasColumnName("realizado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Themisworkshop;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdClientes).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.Cedula, "unique_cedula").IsUnique();

            entity.HasIndex(e => e.Correo, "unique_correo").IsUnique();

            entity.HasIndex(e => e.Telefono, "unique_telefono").IsUnique();

            entity.Property(e => e.IdClientes).HasColumnName("id_clientes");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(15)
                .HasColumnName("cedula");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.Credito)
                .HasColumnType("money")
                .HasColumnName("credito");
            entity.Property(e => e.Estadocivil)
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
                .HasMaxLength(1)
                .HasColumnName("sexo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

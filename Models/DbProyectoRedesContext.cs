using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace U1_evaluacion_sumativa.Models;

public partial class DbProyectoRedesContext : DbContext
{
    public DbProyectoRedesContext()
    {
    }

    public DbProyectoRedesContext(DbContextOptions<DbProyectoRedesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleProyecto> DetalleProyectos { get; set; }

    public virtual DbSet<DireccionamientoIp> DireccionamientoIps { get; set; }

    public virtual DbSet<InicioProyecto> InicioProyectos { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Trabajadore> Trabajadores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=db_proyecto_redes;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<DetalleProyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detalle_proyecto");

            entity.HasIndex(e => e.ProyectoId, "fk_detalle_proyecto_proyectos1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ProyectoId).HasColumnType("int(11)");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.DetalleProyectos)
                .HasForeignKey(d => d.ProyectoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalle_proyecto_proyectos1");
        });

        modelBuilder.Entity<DireccionamientoIp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("direccionamiento_ip");

            entity.HasIndex(e => e.InicioProyectoId, "fk_direccionamiento_ip_inicio_proyecto1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CantIpHost).HasColumnType("int(11)");
            entity.Property(e => e.CantIptotales)
                .HasColumnType("int(11)")
                .HasColumnName("CantIPTotales");
            entity.Property(e => e.InicioProyectoId).HasColumnType("int(11)");
            entity.Property(e => e.IpBroadcast).HasMaxLength(45);
            entity.Property(e => e.IpNetwork).HasMaxLength(45);
            entity.Property(e => e.Mask).HasMaxLength(45);
            entity.Property(e => e.Prefijo).HasMaxLength(4);
            entity.Property(e => e.RangoFinal).HasMaxLength(45);
            entity.Property(e => e.RangoInicial).HasMaxLength(45);
            entity.Property(e => e.SubRed).HasMaxLength(45);

            entity.HasOne(d => d.InicioProyecto).WithMany(p => p.DireccionamientoIps)
                .HasForeignKey(d => d.InicioProyectoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_direccionamiento_ip_inicio_proyecto1");
        });

        modelBuilder.Entity<InicioProyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inicio_proyecto");

            entity.HasIndex(e => e.ProyectoId, "fk_inicio_proyecto_proyectos1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.EntidadInvolucrada).HasMaxLength(200);
            entity.Property(e => e.Estado).HasColumnType("int(11)");
            entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Observaciones).HasMaxLength(1000);
            entity.Property(e => e.ProyectoId).HasColumnType("int(11)");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.InicioProyectos)
                .HasForeignKey(d => d.ProyectoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inicio_proyecto_proyectos1");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectos");

            entity.HasIndex(e => e.UsuarioId, "fk_Proyectos_usuarios_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasMaxLength(400);
            entity.Property(e => e.Estado)
                .HasComment("0: Finalizado ; 1: Proceso; 2: Cancelado")
                .HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Tipo)
                .HasComment("0: Cableado UTP; 1: Configuración Equipos; 2: Ambos")
                .HasColumnType("int(11)");
            entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Proyectos_usuarios");
        });

        modelBuilder.Entity<Trabajadore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("trabajadores");

            entity.HasIndex(e => e.InicioproyectoId, "fk_inicio_proyecto_has_usuarios_inicio_proyecto1_idx");

            entity.HasIndex(e => e.UsuarioId, "fk_inicio_proyecto_has_usuarios_usuarios1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Cargo).HasMaxLength(200);
            entity.Property(e => e.InicioproyectoId).HasColumnType("int(11)");
            entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

            entity.HasOne(d => d.Inicioproyecto).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.InicioproyectoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inicio_proyecto_has_usuarios_inicio_proyecto1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inicio_proyecto_has_usuarios_usuarios1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Estado)
                .HasComment("0: Desactivado ; 1: Activado")
                .HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Rol)
                .HasComment("0: Administador ; 1: Trabajador")
                .HasColumnType("int(11)");
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

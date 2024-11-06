using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Data;

public partial class SistemaGestionContext : DbContext
{
    public SistemaGestionContext()
    {
    }

    public SistemaGestionContext(DbContextOptions<SistemaGestionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<CategoriaEquipo> CategoriaEquipos { get; set; }

    public virtual DbSet<CategoriaTarea> CategoriaTareas { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Dispone> Dispones { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<EquiposRepuesto> EquiposRepuestos { get; set; }

    public virtual DbSet<HistorialMantenimiento> HistorialMantenimientos { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Programacione> Programaciones { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<PuedeSer> PuedeSers { get; set; }

    public virtual DbSet<Repuesto> Repuestos { get; set; }

    public virtual DbSet<Suministra> Suministras { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Tiene> Tienes { get; set; }

    public virtual DbSet<TieneAsignado> TieneAsignados { get; set; }

    public virtual DbSet<TipoPersonal> TipoPersonals { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-39FG47A\\SQLEXPRESS;Database=19-Sistema_de_Gestión_de_Tareas_de_Mantenimiento;Integrated Security=True;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<CategoriaEquipo>(entity =>
        {
            entity.HasKey(e => e.CategoriaEquiposId);

            entity.ToTable(tb => tb.HasTrigger("TR_CategoriaEquipos_ValidaCategoria"));

            entity.HasIndex(e => e.EquipoId, "IX_CategoriaEquipos").IsUnique();

            entity.Property(e => e.CategoriaEquiposId)
                .ValueGeneratedNever()
                .HasColumnName("CategoriaEquiposID");
            entity.Property(e => e.EquipoId)
                .HasComment("")
                .HasColumnName("EquipoID");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Equipo).WithOne(p => p.CategoriaEquipo)
                .HasForeignKey<CategoriaEquipo>(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoriaEquipos_Equipos");
        });

        modelBuilder.Entity<CategoriaTarea>(entity =>
        {
            entity.HasKey(e => e.CategoriaTareasId);

            entity.ToTable(tb => tb.HasTrigger("TR_CategoriaTareas_ValidarTipo"));

            entity.Property(e => e.CategoriaTareasId)
                .ValueGeneratedNever()
                .HasColumnName("CategoriaTareasID");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasIndex(e => e.JefeId, "IX_Departamentos");

            entity.Property(e => e.DepartamentoId)
                .ValueGeneratedNever()
                .HasColumnName("DepartamentoID");
            entity.Property(e => e.JefeId).HasColumnName("JefeID");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Dispone>(entity =>
        {
            entity.HasKey(e => e.TareaId);

            entity.ToTable("Dispone");

            entity.Property(e => e.TareaId)
                .ValueGeneratedNever()
                .HasColumnName("TareaID");
            entity.Property(e => e.ProgramacionId).HasColumnName("ProgramacionID");

            entity.HasOne(d => d.Programacion).WithMany(p => p.Dispones)
                .HasForeignKey(d => d.ProgramacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dispone_Programaciones");

            entity.HasOne(d => d.Tarea).WithOne(p => p.Dispone)
                .HasForeignKey<Dispone>(d => d.TareaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dispone_Tareas");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.ToTable(tb =>
                {
                    tb.HasTrigger("TR_Equipos_HistorialMantenimiento_Restriccion");
                    tb.HasTrigger("trg_ValidateFechaAdquisicion");
                });

            entity.Property(e => e.EquipoId)
                .ValueGeneratedNever()
                .HasComment("")
                .HasColumnName("EquipoID");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EquiposRepuesto>(entity =>
        {
            entity.HasKey(e => new { e.EquipoId, e.RepuestoId });

            entity.Property(e => e.EquipoId).HasColumnName("EquipoID");
            entity.Property(e => e.RepuestoId).HasColumnName("RepuestoID");

            entity.HasOne(d => d.Equipo).WithMany(p => p.EquiposRepuestos)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquiposRepuestos_Equipos");

            entity.HasOne(d => d.Repuesto).WithMany(p => p.EquiposRepuestos)
                .HasForeignKey(d => d.RepuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquiposRepuestos_Repuestos");
        });

        modelBuilder.Entity<HistorialMantenimiento>(entity =>
        {
            entity.HasKey(e => e.HistorialId);

            entity.ToTable("HistorialMantenimiento");

            entity.HasIndex(e => e.EquipoId, "IX_HistorialMantenimiento");

            entity.Property(e => e.HistorialId)
                .ValueGeneratedNever()
                .HasColumnName("HistorialID");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.EquipoId).HasColumnName("EquipoID");
            entity.Property(e => e.PersonalId).HasColumnName("PersonalID");

            entity.HasOne(d => d.Equipo).WithMany(p => p.HistorialMantenimientos)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialMantenimiento_Equipos1");

            entity.HasOne(d => d.Personal).WithMany(p => p.HistorialMantenimientos)
                .HasForeignKey(d => d.PersonalId)
                .HasConstraintName("FK_HistorialMantenimiento_Personal");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.ToTable("Personal");

            entity.HasIndex(e => e.DepartamentoId, "IX_Personal");

            entity.Property(e => e.PersonalId)
                .ValueGeneratedNever()
                .HasComment("Identificador de personal.")
                .HasColumnName("PersonalID");
            entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Departamento).WithMany(p => p.Personals)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personal_Departamentos");
        });

        modelBuilder.Entity<Programacione>(entity =>
        {
            entity.HasKey(e => e.ProgramacionId);

            entity.Property(e => e.ProgramacionId)
                .ValueGeneratedNever()
                .HasColumnName("ProgramacionID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedorId);

            entity.Property(e => e.ProveedorId)
                .ValueGeneratedNever()
                .HasColumnName("ProveedorID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PuedeSer>(entity =>
        {
            entity.HasKey(e => e.PersonalId);

            entity.ToTable("PuedeSer", tb => tb.HasTrigger("TR_PuedeSer_ValidaTipoPersonal"));

            entity.Property(e => e.PersonalId)
                .ValueGeneratedNever()
                .HasComment("Identificador de personal.")
                .HasColumnName("PersonalID");
            entity.Property(e => e.TipoPersonalId).HasColumnName("TipoPersonalID");

            entity.HasOne(d => d.Personal).WithOne(p => p.PuedeSer)
                .HasForeignKey<PuedeSer>(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PuedeSer_Personal");

            entity.HasOne(d => d.TipoPersonal).WithMany(p => p.PuedeSers)
                .HasForeignKey(d => d.TipoPersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PuedeSer_TipoPersonal");
        });

        modelBuilder.Entity<Repuesto>(entity =>
        {
            entity.Property(e => e.RepuestoId)
                .ValueGeneratedNever()
                .HasColumnName("RepuestoID");
            entity.Property(e => e.NombreRepuesto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Suministra>(entity =>
        {
            entity.HasKey(e => e.ProveedorId);

            entity.ToTable("Suministra");

            entity.Property(e => e.ProveedorId)
                .ValueGeneratedNever()
                .HasColumnName("ProveedorID");
            entity.Property(e => e.RepuestoId).HasColumnName("RepuestoID");

            entity.HasOne(d => d.Proveedor).WithOne(p => p.Suministra)
                .HasForeignKey<Suministra>(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Suministra_Proveedores");

            entity.HasOne(d => d.Repuesto).WithMany(p => p.Suministras)
                .HasForeignKey(d => d.RepuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Suministra_Repuestos");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_Tareas_PreventDelete"));

            entity.Property(e => e.TareaId)
                .ValueGeneratedNever()
                .HasColumnName("TareaID");
            entity.Property(e => e.Descripcion).HasColumnType("text");

            entity.HasMany(d => d.CategoriaTareas).WithMany(p => p.Tareas)
                .UsingEntity<Dictionary<string, object>>(
                    "Clasifica",
                    r => r.HasOne<CategoriaTarea>().WithMany()
                        .HasForeignKey("CategoriaTareasId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Clasifica_CategoriaTareas"),
                    l => l.HasOne<Tarea>().WithMany()
                        .HasForeignKey("TareaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Clasifica_Tareas"),
                    j =>
                    {
                        j.HasKey("TareaId", "CategoriaTareasId");
                        j.ToTable("Clasifica");
                        j.IndexerProperty<int>("TareaId").HasColumnName("TareaID");
                        j.IndexerProperty<int>("CategoriaTareasId").HasColumnName("CategoriaTareasID");
                    });

            entity.HasMany(d => d.Personals).WithMany(p => p.Tareas)
                .UsingEntity<Dictionary<string, object>>(
                    "Realiza",
                    r => r.HasOne<Personal>().WithMany()
                        .HasForeignKey("PersonalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Realiza_Personal"),
                    l => l.HasOne<Tarea>().WithMany()
                        .HasForeignKey("TareaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Realiza_Tareas"),
                    j =>
                    {
                        j.HasKey("TareaId", "PersonalId");
                        j.ToTable("Realiza");
                        j.IndexerProperty<int>("TareaId").HasColumnName("TareaID");
                        j.IndexerProperty<int>("PersonalId")
                            .HasComment("Identificador de personal.")
                            .HasColumnName("PersonalID");
                    });
        });

        modelBuilder.Entity<Tiene>(entity =>
        {
            entity.HasKey(e => e.DepartamentoId);

            entity.ToTable("Tiene");

            entity.Property(e => e.DepartamentoId)
                .ValueGeneratedNever()
                .HasColumnName("DepartamentoID");
            entity.Property(e => e.PersonalId)
                .HasComment("Identificador de personal.")
                .HasColumnName("PersonalID");

            entity.HasOne(d => d.Departamento).WithOne(p => p.Tiene)
                .HasForeignKey<Tiene>(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tiene_Departamentos");

            entity.HasOne(d => d.Personal).WithMany(p => p.Tienes)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tiene_Personal");
        });

        modelBuilder.Entity<TieneAsignado>(entity =>
        {
            entity.HasKey(e => e.EquipoId);

            entity.ToTable("TieneAsignado");

            entity.Property(e => e.EquipoId)
                .ValueGeneratedNever()
                .HasComment("")
                .HasColumnName("EquipoID");
            entity.Property(e => e.TareaId).HasColumnName("TareaID");

            entity.HasOne(d => d.Equipo).WithOne(p => p.TieneAsignado)
                .HasForeignKey<TieneAsignado>(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TieneAsignado_Equipos");

            entity.HasOne(d => d.Tarea).WithMany(p => p.TieneAsignados)
                .HasForeignKey(d => d.TareaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TieneAsignado_Tareas");
        });

        modelBuilder.Entity<TipoPersonal>(entity =>
        {
            entity.ToTable("TipoPersonal", tb => tb.HasTrigger("TR_TipoPersonal_ValidaTipoPersonal"));

            entity.Property(e => e.TipoPersonalId)
                .ValueGeneratedNever()
                .HasColumnName("TipoPersonalID");
            entity.Property(e => e.TipoPersonal1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TipoPersonal");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuario__52311169116D17D9");

            entity.ToTable("Usuario");

            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

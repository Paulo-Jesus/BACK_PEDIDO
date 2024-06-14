using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BACK_PEDIDO.Models;

public partial class BdPedidosContext : DbContext
{
    public BdPedidosContext()
    {
    }

    public BdPedidosContext(DbContextOptions<BdPedidosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__CB903349D74C9D5E");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("Estado_IdEstado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK__CATEGORIA__Estad__46E78A0C");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__EMPRESA__443B3D9D472CC16C");

            entity.ToTable("EMPRESA");

            entity.Property(e => e.IdEmpresa).HasColumnName("Id_Empresa");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("Estado_IdEstado");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Ruc)
                .HasMaxLength(13)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK__EMPRESA__Estado___3C69FB99");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__ESTADO__AB2EB6F8DA572A7A");

            entity.ToTable("ESTADO");

            entity.Property(e => e.IdEstado).HasColumnName("Id_Estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__PEDIDOS__A5D0013927ADC029");

            entity.ToTable("PEDIDOS");

            entity.Property(e => e.IdPedido).HasColumnName("Id_Pedido");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("Estado_IdEstado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Registro");
            entity.Property(e => e.ProductoIdProducto).HasColumnName("Producto_IdProducto");
            entity.Property(e => e.UsuarioIdUsuario).HasColumnName("Usuario_IdUsuario");

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK__PEDIDOS__Estado___5165187F");

            entity.HasOne(d => d.ProductoIdProductoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ProductoIdProducto)
                .HasConstraintName("FK__PEDIDOS__Product__5070F446");

            entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioIdUsuario)
                .HasConstraintName("FK__PEDIDOS__Usuario__4F7CD00D");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__2085A9CF6B9A6DD6");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.CategoriaIdCategoria).HasColumnName("Categoria_IdCategoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoIdEstado).HasColumnName("Estado_IdEstado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(3, 2)");

            entity.HasOne(d => d.CategoriaIdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaIdCategoria)
                .HasConstraintName("FK__PRODUCTO__Catego__4AB81AF0");

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK__PRODUCTO__Estado__4BAC3F29");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__55932E86C136E84F");

            entity.ToTable("ROL");

            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("Estado_IdEstado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Rols)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK__ROL__Estado_IdEs__398D8EEE");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USUARIO__3214EC07498C4514");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaIdEmpresa).HasColumnName("Empresa_IdEmpresa");
            entity.Property(e => e.EstadoIdEstado).HasColumnName("Estado_IdEstado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.RolIdRol).HasColumnName("Rol_IdRol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.EmpresaIdEmpresaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpresaIdEmpresa)
                .HasConstraintName("FK__USUARIO__Empresa__3F466844");

            entity.HasOne(d => d.EstadoIdEstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EstadoIdEstado)
                .HasConstraintName("FK__USUARIO__Estado___412EB0B6");

            entity.HasOne(d => d.RolIdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolIdRol)
                .HasConstraintName("FK__USUARIO__Rol_IdR__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

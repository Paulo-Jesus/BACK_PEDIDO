using DataLayer.Common;
using EntityLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Database;

public partial class PedidosDatabaseContext : DbContext
{
    public PedidosDatabaseContext()
    {
    }

    public PedidosDatabaseContext(DbContextOptions<PedidosDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proovedor> Proovedors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable(DLVariables.ConnectionString)!);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10B781C5C4");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033EAE92071F");

            entity.ToTable("Empresa");

            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ruc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RUC");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empresa__IdEstad__4E88ABD4");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC1F66A3281");

            entity.ToTable("Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__9D335DC38B95E001");

            entity.ToTable("Pedido");

            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__IdProduc__6383C8BA");

            entity.HasOne(d => d.IdRestauranteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdRestaurante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__IdRestau__628FA481");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__IdUsuari__619B8048");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892106981363E");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(3, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__IdCate__5BE2A6F2");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__IdEsta__5DCAEF64");

            entity.HasOne(d => d.IdRestauranteNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdRestaurante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__IdRest__5CD6CB2B");
        });

        modelBuilder.Entity<Proovedor>(entity =>
        {
            entity.HasKey(e => e.IdRestaurante).HasName("PK__Restaura__29CE64FA4EAF54EE");

            entity.ToTable("Proovedor");

            entity.Property(e => e.Contrasena)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ruc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RUC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Proovedors)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Restauran__IdEst__571DF1D5");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Proovedors)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Restauran__IdRol__5629CD9C");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C22D5CFF7");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Rols)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rol__IdEstado__4BAC3F29");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97A57FC3E2");

            entity.ToTable("Usuario");

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdEmpre__52593CB8");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdEstad__534D60F1");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

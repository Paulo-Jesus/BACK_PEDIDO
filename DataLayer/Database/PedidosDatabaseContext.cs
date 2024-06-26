using EntitiLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Database
{
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

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<MenuDetalle> MenuDetalles { get; set; }

        public virtual DbSet<Pedido> Pedidos { get; set; }

        public virtual DbSet<Producto> Productos { get; set; }

        public virtual DbSet<Proveedor> Proveedors { get; set; }

        public virtual DbSet<Rol> Rols { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=Pedidos_Database; Trusted_Connection=True; MultipleActiveResultSets=true; TrustServerCertificate=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A101B244D5D");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033E8DAA65C3");

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
                    .HasConstraintName("FK__Empresa__IdEstad__3C69FB99");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC16F950CBF");

                entity.ToTable("Estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu).HasName("PK__Menu__4D7EA8E1DC4BEBC1");

                entity.ToTable("Menu");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");
                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.HasOne(d => d.IdMenuDetalleNavigation).WithMany(p => p.Menus)
                    .HasForeignKey(d => d.IdMenuDetalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Menu__IdMenuDeta__5812160E");

                entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Menus)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Menu__IdProveedo__571DF1D5");
            });

            modelBuilder.Entity<MenuDetalle>(entity =>
            {
                entity.HasKey(e => e.IdMenuDetalle).HasName("PK__MenuDeta__CAAB5862A305B664");

                entity.ToTable("MenuDetalle");

                entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.MenuDetalles)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuDetal__IdPro__5441852A");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__9D335DC30D659384");

                entity.ToTable("Pedido");

                entity.Property(e => e.FechaPedido)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__IdProduc__5165187F");

                entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__IdProvee__5070F446");

                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__IdUsuari__4F7CD00D");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto).HasName("PK__Producto__0988921013150A9D");

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
                    .HasConstraintName("FK__Producto__IdCate__4AB81AF0");

                entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Producto__IdEsta__4BAC3F29");

                entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Producto__IdProv__49C3F6B7");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AF939CF133");

                entity.ToTable("Proveedor");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(100)
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

                entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proveedor__IdEst__44FF419A");

                entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proveedor__IdRol__440B1D61");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CF9B314BC");

                entity.ToTable("Rol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Rols)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rol__IdEstado__398D8EEE");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF971CA22615");

                entity.ToTable("Usuario");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.Contrasena)
                    .HasMaxLength(100)
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
                    .HasConstraintName("FK__Usuario__IdEmpre__403A8C7D");

                entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__IdEstad__412EB0B6");

                entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__IdRol__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

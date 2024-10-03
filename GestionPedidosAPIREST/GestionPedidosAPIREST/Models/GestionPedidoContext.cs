using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestionPedidosAPIREST.Models
{
    public partial class GestionPedidoContext : DbContext
    {
        public GestionPedidoContext()
        {
        }

        public GestionPedidoContext(DbContextOptions<GestionPedidoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CabeceraPedido> CabeceraPedidos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-ANJ8TOG\\SQLEXPRESS;DataBase=GestionPedido;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CabeceraPedido>(entity =>
            {
                entity.HasKey(e => e.CodigoProducto)
                    .HasName("PK__cabecera__105107A98CE9F478");

                entity.ToTable("cabecera_pedido");

                entity.HasIndex(e => e.CodigoProducto, "UQ__cabecera__105107A8A6F2A173")
                    .IsUnique();

                entity.Property(e => e.CodigoProducto).HasColumnName("codigo_producto");

                entity.Property(e => e.CantidadTotal).HasColumnName("cantidad_total");

                entity.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

                entity.Property(e => e.DireccionEntrega)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("direccion_entrega");

                entity.Property(e => e.Igv)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("igv");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total");

                entity.HasOne(d => d.CodigoClienteNavigation)
                    .WithMany(p => p.CabeceraPedidos)
                    .HasForeignKey(d => d.CodigoCliente)
                    .HasConstraintName("FK__cabecera___codig__6A30C649");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodigoCliente)
                    .HasName("PK__cliente__4D182E8D7C396CF3");

                entity.ToTable("cliente");

                entity.HasIndex(e => e.CodigoCliente, "UQ__cliente__4D182E8C876B61E9")
                    .IsUnique();

                entity.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

                entity.Property(e => e.DireccionEntrega)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("direccion_entrega");

                entity.Property(e => e.DireccionFiscal)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("direccion_fiscal");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecha_registro");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("razon_social");

                entity.Property(e => e.RucDni).HasColumnName("ruc_dni");

                entity.Property(e => e.UsuarioRegistro)
                    .HasColumnType("date")
                    .HasColumnName("usuario_registro");
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasKey(e => new { e.CodigoPedido, e.NumeroLinea })
                    .HasName("PK__detalle___E14C76E3901D8171");

                entity.ToTable("detalle_pedido");

                entity.HasIndex(e => e.CodigoPedido, "UQ__detalle___BBD0C51AE48E4FA4")
                    .IsUnique();

                entity.Property(e => e.CodigoPedido)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("codigo_pedido");

                entity.Property(e => e.NumeroLinea).HasColumnName("numero_linea");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoProducto).HasColumnName("codigo_producto");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total");

                entity.HasOne(d => d.CodigoProductoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.CodigoProducto)
                    .HasConstraintName("FK__detalle_p__codig__6B24EA82");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.CodigoProducto)
                    .HasName("PK__producto__105107A984EDC8B1");

                entity.ToTable("productos");

                entity.HasIndex(e => e.CodigoProducto, "UQ__producto__105107A8E1B5D7AE")
                    .IsUnique();

                entity.Property(e => e.CodigoProducto).HasColumnName("codigo_producto");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_producto");

                entity.Property(e => e.Moneda)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("moneda");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("precio_unitario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.CodigoUsuario)
                    .HasName("PK__usuarios__37F064A0CB4151A8");

                entity.ToTable("usuarios");

                entity.HasIndex(e => e.CodigoUsuario, "UQ__usuarios__37F064A1CCF36A42")
                    .IsUnique();

                entity.Property(e => e.CodigoUsuario).HasColumnName("codigo_usuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.EstadoSession)
                    .HasColumnName("estado_session")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FotoUsuario)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("foto_usuario");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.UltimaFecha)
                    .HasColumnType("date")
                    .HasColumnName("ultima_fecha");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

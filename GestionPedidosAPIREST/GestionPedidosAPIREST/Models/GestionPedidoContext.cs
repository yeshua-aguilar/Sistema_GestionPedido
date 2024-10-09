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
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CabeceraPedido>(entity =>
            {
                entity.HasKey(e => e.CodigoPedido)
                    .HasName("PK__cabecera__BBD0C51B5D2426BF");

                entity.ToTable("cabecera_pedido");

                entity.HasIndex(e => e.CodigoPedido, "UQ__cabecera__BBD0C51A9841A594")
                    .IsUnique();

                entity.Property(e => e.CodigoPedido).HasColumnName("codigo_pedido");

                entity.Property(e => e.CantidadTotal).HasColumnName("cantidad_total");

                entity.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

                entity.Property(e => e.DireccionEntrega)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("direccion_entrega");

                entity.Property(e => e.Igv)
                    .HasColumnType("decimal(20, 2)")
                    .HasColumnName("igv");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(20, 2)")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(20, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.CodigoClienteNavigation)
                    .WithMany(p => p.CabeceraPedidos)
                    .HasForeignKey(d => d.CodigoCliente)
                    .HasConstraintName("FK__cabecera___codig__3587F3E0");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodigoCliente)
                    .HasName("PK__cliente__4D182E8DDDBE5463");

                entity.ToTable("cliente");

                entity.HasIndex(e => e.CodigoCliente, "UQ__cliente__4D182E8CBB0AD63E")
                    .IsUnique();

                entity.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

                entity.Property(e => e.DireccionEntrega)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion_entrega");

                entity.Property(e => e.DireccionFiscal)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion_fiscal");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecha_registro");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(100)
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
                    .HasName("PK__detalle___E14C76E36F0D07D5");

                entity.ToTable("detalle_pedido");

                entity.Property(e => e.CodigoPedido).HasColumnName("codigo_pedido");

                entity.Property(e => e.NumeroLinea).HasColumnName("numero_linea");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoProducto).HasColumnName("codigo_producto");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(20, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.CodigoPedidoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.CodigoPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__detalle_p__codig__37703C52");

                entity.HasOne(d => d.CodigoProductoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.CodigoProducto)
                    .HasConstraintName("FK__detalle_p__codig__367C1819");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.CodigoProducto)
                    .HasName("PK__producto__105107A9CE06010B");

                entity.ToTable("productos");

                entity.HasIndex(e => e.CodigoProducto, "UQ__producto__105107A8FFF4F761")
                    .IsUnique();

                entity.Property(e => e.CodigoProducto).HasColumnName("codigo_producto");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_producto");

                entity.Property(e => e.Moneda)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("moneda");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(20, 2)")
                    .HasColumnName("precio_unitario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.CodigoUsuario)
                    .HasName("PK__usuarios__37F064A010CCAA07");

                entity.ToTable("usuarios");

                entity.HasIndex(e => e.CodigoUsuario, "UQ__usuarios__37F064A153DD9F63")
                    .IsUnique();

                entity.Property(e => e.CodigoUsuario).HasColumnName("codigo_usuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.EstadoSession)
                    .HasColumnName("estado_session")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FotoUsuario)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("foto_usuario");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.UltimaFecha)
                    .HasColumnType("date")
                    .HasColumnName("ultima_fecha");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

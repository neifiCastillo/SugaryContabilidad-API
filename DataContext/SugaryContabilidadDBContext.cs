using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SugaryContabilidad_API.Models;

namespace SugaryContabilidad_API.DataContext
{
    public partial class SugaryContabilidadDBContext : DbContext
    {
        public SugaryContabilidadDBContext()
        {
        }

        public SugaryContabilidadDBContext(DbContextOptions<SugaryContabilidadDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Caja> Cajas { get; set; } = null!;
        public virtual DbSet<Deuda> Deudas { get; set; } = null!;
        public virtual DbSet<EstadoProducto> EstadoProductos { get; set; } = null!;
        public virtual DbSet<Facturable> Facturables { get; set; } = null!;
        public virtual DbSet<ImagenesProducto> ImagenesProductos { get; set; } = null!;
        public virtual DbSet<ImagenesUsuario> ImagenesUsuarios { get; set; } = null!;
        public virtual DbSet<MetodoVentum> MetodoVenta { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caja>(entity =>
            {
                entity.HasKey(e => e.IdCaja)
                    .HasName("PK__Caja__3B7BF2C5BED9FF28");

                entity.ToTable("Caja", "SCC");

                entity.Property(e => e.FechaCierreCaja).HasColumnType("date");

                entity.Property(e => e.FechaCreacionCaja).HasColumnType("date");

                entity.Property(e => e.NombreCaja)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Deuda>(entity =>
            {
                entity.HasKey(e => e.IdDeuda)
                    .HasName("PK__Deudas__7F8C86B165E703EA");

                entity.ToTable("Deudas", "SCC");

                entity.Property(e => e.CedulaDeudor)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionDeuda).IsUnicode(false);

                entity.Property(e => e.FechaAporte).HasColumnType("date");

                entity.Property(e => e.FechaFinalDeuda).HasColumnType("date");

                entity.Property(e => e.FechaInicioDeuda).HasColumnType("date");

                entity.Property(e => e.NombreDeudor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TicketDeuda).IsUnicode(false);
            });

            modelBuilder.Entity<EstadoProducto>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estado_P__FBB0EDC1780024C0");

                entity.ToTable("Estado_Producto", "SCC");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Facturable>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PK__Facturab__50E7BAF1016F33FB");

                entity.ToTable("Facturables", "SCC");

                entity.Property(e => e.CantidadProductoVendido).IsUnicode(false);

                entity.Property(e => e.CategoriaFactura)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CedulaDeudor)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFactura).HasColumnType("date");

                entity.Property(e => e.MetodoVenta)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.NombreDeudor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProducto).IsUnicode(false);

                entity.Property(e => e.PrecioVenta).IsUnicode(false);

                entity.Property(e => e.TicketDeuda).IsUnicode(false);

                entity.Property(e => e.TicketFactura).IsUnicode(false);

                entity.Property(e => e.TicketVenta).IsUnicode(false);
            });

            modelBuilder.Entity<ImagenesProducto>(entity =>
            {
                entity.ToTable("ImagenesProductos", "SCC");

                entity.Property(e => e.Path).IsUnicode(false);
            });

            modelBuilder.Entity<ImagenesUsuario>(entity =>
            {
                entity.ToTable("ImagenesUsuarios", "SCC");

                entity.Property(e => e.Path).IsUnicode(false);
            });

            modelBuilder.Entity<MetodoVentum>(entity =>
            {
                entity.HasKey(e => e.IdMetodo)
                    .HasName("PK__Metodo_V__63A2128902FED498");

                entity.ToTable("Metodo_Venta", "SCC");

                entity.Property(e => e.CedulaReferencia)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Metodo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePerteneceReferencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroReferencia)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TipoReferencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__098892108D18C252");

                entity.ToTable("Productos", "SCC");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCompra).HasColumnType("date");

                entity.Property(e => e.FechaCreacion).HasColumnType("date");

                entity.Property(e => e.FechaVencimiento).HasColumnType("date");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__5B65BF97EEE76232");

                entity.ToTable("Usuarios", "SCC");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegFeha)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioType)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Ventas__BC1240BD2DBD4EF5");

                entity.ToTable("Ventas", "SCC");

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaVenta).HasColumnType("date");

                entity.Property(e => e.MetodoVenta)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TicketVenta).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

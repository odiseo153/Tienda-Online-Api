using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tienda_Online_Api.Modelos;

public partial class TiendaOnlineContext : DbContext
{
    public TiendaOnlineContext()
    {
    }

    public TiendaOnlineContext(DbContextOptions<TiendaOnlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carritocompra> Carritocompras { get; set; }

    public virtual DbSet<Detallespedido> Detallespedidos { get; set; }

    public virtual DbSet<Historialcambio> Historialcambios { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=TiendaOnline;Username=postgres;Password=padomo153");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carritocompra>(entity =>
        {
            entity.HasKey(e => e.Carritoid).HasName("carritocompras_pkey");

            entity.ToTable("carritocompras");

            entity.Property(e => e.Carritoid)
                .HasColumnName("carritoid")
                .ValueGeneratedOnAdd();// Esto indica que el valor se generará automáticamente al insertar.
                

            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Productoid).HasColumnName("productoid");
            entity.Property(e => e.Usuarioid).HasColumnName("usuarioid");

            entity.HasOne(d => d.Producto).WithMany(p => p.Carritocompras)
                .HasForeignKey(d => d.Productoid)
                .HasConstraintName("carritocompras_productoid_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritocompras)
                .HasForeignKey(d => d.Usuarioid)
                .HasConstraintName("carritocompras_usuarioid_fkey");
        });

        modelBuilder.Entity<Detallespedido>(entity =>
        {
            entity.HasKey(e => e.Detallepedidoid).HasName("detallespedido_pkey");

            entity.ToTable("detallespedido");

            entity.Property(e => e.Detallepedidoid)
                .HasColumnName("detallepedidoid")
                .ValueGeneratedOnAdd();
                 

            // Otras propiedades...

            entity.HasOne(d => d.Pedido).WithMany(p => p.Detallespedidos)
                .HasForeignKey(d => d.Pedidoid)
                .HasConstraintName("detallespedido_pedidoid_fkey");

            entity.HasOne(d => d.Producto).WithMany(p => p.Detallespedidos)
                .HasForeignKey(d => d.Productoid)
                .HasConstraintName("detallespedido_productoid_fkey");
        });

        modelBuilder.Entity<Historialcambio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("historialcambios_pkey");

            entity.ToTable("historialcambios");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            

            // Otras propiedades...

            entity.HasOne(d => d.Pedido).WithMany(p => p.Historialcambios)
                .HasForeignKey(d => d.Pedidoid)
                .HasConstraintName("historialcambios_pedidoid_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Historialcambios)
                .HasForeignKey(d => d.Usuarioid)
                .HasConstraintName("historialcambios_usuarioid_fkey");
        });


        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Pedidoid).HasName("pedidos_pkey");

            entity.ToTable("pedidos");

            entity.Property(e => e.Pedidoid)
                .HasColumnName("pedidoid")
                .ValueGeneratedOnAdd();
            

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");

            entity.Property(e => e.Fechapedido)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechapedido");

            entity.Property(e => e.Usuarioid).HasColumnName("usuarioid");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Usuarioid)
                .HasConstraintName("pedidos_usuarioid_fkey");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Productoid).HasName("productos_pkey");

            entity.ToTable("productos");

            entity.Property(e => e.Productoid)
                .HasColumnName("productoid")
                .ValueGeneratedOnAdd();
              

            entity.Property(e => e.Descripcion).HasColumnName("descripcion");

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");

            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuarioid).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.Usuarioid)
                .HasColumnName("usuarioid")
                .ValueGeneratedOnAdd();
               

            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");

            entity.Property(e => e.Correoelectronico)
                .HasMaxLength(255)
                .HasColumnName("correoelectronico");

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.Property(e => e.Rol)
                .HasMaxLength(10)
                .HasColumnName("rol");
        });





        OnModelCreatingPartial(modelBuilder);


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

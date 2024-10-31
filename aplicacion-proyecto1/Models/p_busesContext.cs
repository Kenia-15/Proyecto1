using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aplicacion_proyecto1.Models
{
    public partial class p_busesContext : DbContext
    {
        public p_busesContext()
        {
        }

        public p_busesContext(DbContextOptions<p_busesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAsientosXReserva> TblAsientosXReservas { get; set; } = null!;
        public virtual DbSet<TblBuse> TblBuses { get; set; } = null!;
        public virtual DbSet<TblHistorialPago> TblHistorialPagos { get; set; } = null!;
        public virtual DbSet<TblHorario> TblHorarios { get; set; } = null!;
        public virtual DbSet<TblHorariosXBuse> TblHorariosXBuses { get; set; } = null!;
        public virtual DbSet<TblLugare> TblLugares { get; set; } = null!;
        public virtual DbSet<TblMetodosPago> TblMetodosPagos { get; set; } = null!;
        public virtual DbSet<TblPersona> TblPersonas { get; set; } = null!;
        public virtual DbSet<TblPromocione> TblPromociones { get; set; } = null!;
        public virtual DbSet<TblReserva> TblReservas { get; set; } = null!;
        public virtual DbSet<TblRuta> TblRutas { get; set; } = null!;
        public virtual DbSet<TblUsuario> TblUsuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //              optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=p_buses;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAsientosXReserva>(entity =>
            {
                entity.HasKey(e => new { e.IdAsiento, e.IdReserva })
                    .HasName("pk_asientos");

                entity.ToTable("tbl_asientos_x_reserva");

                entity.Property(e => e.IdAsiento)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_asiento")
                    .HasComment("Identificador del asiento");

                entity.Property(e => e.IdReserva)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_reserva")
                    .HasComment("Identificador de la reserva");

                entity.Property(e => e.TipoAsiento)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tipo_asiento")
                    .HasComment("Tipo de asiento. Posibles valores: P (Preferencial), C (Comun)");

                entity.HasOne(d => d.IdReservaNavigation)
                    .WithMany(p => p.TblAsientosXReservas)
                    .HasForeignKey(d => d.IdReserva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_asientos_reserva_01");
            });

            modelBuilder.Entity<TblBuse>(entity =>
            {
                entity.HasKey(e => e.IdBus)
                    .HasName("pk_buses");

                entity.ToTable("tbl_buses");

                entity.Property(e => e.IdBus)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_bus")
                    .HasComment("Identificador del bus");

                entity.Property(e => e.Capacidad)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("capacidad")
                    .HasComment("Capacidad maxima del bus");
            });

            modelBuilder.Entity<TblHistorialPago>(entity =>
            {
                entity.HasKey(e => new { e.IdHistorial, e.IdReserva })
                    .HasName("pk_historial");

                entity.ToTable("tbl_historial_pagos");

                entity.Property(e => e.IdHistorial)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_historial")
                    .HasComment("Identificador del historial");

                entity.Property(e => e.IdReserva)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_reserva")
                    .HasComment("Identificador de la reserva");

                entity.Property(e => e.FechaPago)
                    .HasColumnType("date")
                    .HasColumnName("fecha_pago")
                    .HasComment("Fecha en que se realizo el pago de la compra");

                entity.Property(e => e.IdPromocion)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_promocion")
                    .HasComment("Identificador de la promocion");

                entity.Property(e => e.Monto)
                    .HasColumnType("numeric(14, 2)")
                    .HasColumnName("monto")
                    .HasComment("Monto total de la reserva");

                entity.HasOne(d => d.IdPromocionNavigation)
                    .WithMany(p => p.TblHistorialPagos)
                    .HasForeignKey(d => d.IdPromocion)
                    .HasConstraintName("fk_historial_02");

                entity.HasOne(d => d.IdReservaNavigation)
                    .WithMany(p => p.TblHistorialPagos)
                    .HasForeignKey(d => d.IdReserva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_historial_01");
            });

            modelBuilder.Entity<TblHorario>(entity =>
            {
                entity.HasKey(e => e.IdHorario)
                    .HasName("pk_horarios");

                entity.ToTable("tbl_horarios");

                entity.Property(e => e.IdHorario)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_horario")
                    .HasComment("Identificador del horario");

                entity.Property(e => e.Hora)
                    .HasColumnName("hora")
                    .HasComment("Hora de la ruta");

                entity.Property(e => e.IdRuta)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_ruta")
                    .HasComment("Identificador de la ruta");
            });

            modelBuilder.Entity<TblHorariosXBuse>(entity =>
            {
                entity.HasKey(e => new { e.IdHorario, e.IdBus })
                    .HasName("pk_horarios_buses");

                entity.ToTable("tbl_horarios_x_buses");

                entity.Property(e => e.IdHorario)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_horario")
                    .HasComment("Identificador del horario");

                entity.Property(e => e.IdBus)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_bus")
                    .HasComment("Identificador del bus");

                entity.Property(e => e.AsientosDisponibles)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("asientos_disponibles")
                    .HasComment("Representa la cantidad de asientos disponibles por horario de cada bus");

                entity.HasOne(d => d.IdBusNavigation)
                    .WithMany(p => p.TblHorariosXBuses)
                    .HasForeignKey(d => d.IdBus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_horarios_buses_02");

                entity.HasOne(d => d.IdHorarioNavigation)
                    .WithMany(p => p.TblHorariosXBuses)
                    .HasForeignKey(d => d.IdHorario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_horarios_buses_01");
            });

            modelBuilder.Entity<TblLugare>(entity =>
            {
                entity.HasKey(e => e.IdLugar)
                    .HasName("pk_lugares");

                entity.ToTable("tbl_lugares");

                entity.Property(e => e.IdLugar)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_lugar");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<TblMetodosPago>(entity =>
            {
                entity.HasKey(e => e.IdMetodoPago)
                    .HasName("pk_metodos_pago");

                entity.ToTable("tbl_metodos_pago");

                entity.Property(e => e.IdMetodoPago)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_metodo_pago")
                    .HasComment("Identificador de la tabla");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Nombre del metodo de pago");
            });

            modelBuilder.Entity<TblPersona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("pk_personas");

                entity.ToTable("tbl_personas");

                entity.Property(e => e.IdPersona)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_persona")
                    .HasComment("Identificador de la tabla");

                entity.Property(e => e.IdMetodoPago)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_metodo_pago")
                    .HasComment("Referencia el metodo de pago seleccionado por la persona");

                entity.Property(e => e.NumeroIdentificacion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("numero_identificacion")
                    .HasComment("Corresponde al numero de identificacion de la persona");

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("primer_apellido")
                    .HasComment("Corresponde al primer apellido de la persona");

                entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("primer_nombre")
                    .HasComment("Corresponde al primer nombre de la persona");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("segundo_apellido")
                    .HasComment("Corresponde al segundo apellido de la persona");

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("segundo_nombre")
                    .HasComment("Corresponde al segundo nombre de la persona");

                entity.HasOne(d => d.IdMetodoPagoNavigation)
                    .WithMany(p => p.TblPersonas)
                    .HasForeignKey(d => d.IdMetodoPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_personas");
            });

            modelBuilder.Entity<TblPromocione>(entity =>
            {
                entity.HasKey(e => e.IdPromocion)
                    .HasName("pk_promociones");

                entity.ToTable("tbl_promociones");

                entity.Property(e => e.IdPromocion)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_promocion")
                    .HasComment("Identificador de la promocion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Descripcion de la promocion");

                entity.Property(e => e.Descuento)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("descuento")
                    .HasComment("Porcentaje de descuento de la promocion");

                entity.Property(e => e.FechaFinal)
                    .HasColumnType("date")
                    .HasColumnName("fecha_final")
                    .HasComment("Fecha final de la promocion");

                entity.Property(e => e.FechaInicial)
                    .HasColumnType("date")
                    .HasColumnName("fecha_inicial")
                    .HasComment("Fecha inicial de la promocion");

                entity.HasMany(d => d.IdRuta)
                    .WithMany(p => p.IdPromocions)
                    .UsingEntity<Dictionary<string, object>>(
                        "TblPromocionesXRutum",
                        l => l.HasOne<TblRuta>().WithMany().HasForeignKey("IdRuta").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_promociones_ruta_01"),
                        r => r.HasOne<TblPromocione>().WithMany().HasForeignKey("IdPromocion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_promociones_ruta_02"),
                        j =>
                        {
                            j.HasKey("IdPromocion", "IdRuta").HasName("pk_promo_rura");

                            j.ToTable("tbl_promociones_x_ruta");

                            j.IndexerProperty<string>("IdPromocion").HasMaxLength(5).IsUnicode(false).HasColumnName("id_promocion").HasComment("Identificador de la promocion");

                            j.IndexerProperty<string>("IdRuta").HasMaxLength(5).IsUnicode(false).HasColumnName("id_ruta").HasComment("Identificador de la ruta");
                        });
            });

            modelBuilder.Entity<TblReserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva);

                entity.ToTable("tbl_reservas");

                entity.Property(e => e.IdReserva)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_reserva")
                    .HasComment("Identificador de la tabla");

                entity.Property(e => e.EstadoPago)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado_pago")
                    .HasComment("Corresponde al estado del pago. Posibles valores: P (Pendiente), C (Pagado).");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasComment("Fecha de la reserva");

                entity.Property(e => e.Hora)
                    .HasColumnName("hora")
                    .HasComment("Hora de la reserva");

                entity.Property(e => e.IdHorario)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_horario")
                    .HasComment("Representa el horario de la reserva");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_usuario")
                    .HasComment("Corresponde al usuario que reserva");

                entity.HasOne(d => d.IdHorarioNavigation)
                    .WithMany(p => p.TblReservas)
                    .HasForeignKey(d => d.IdHorario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservas_01");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TblReservas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservas_02");
            });

            modelBuilder.Entity<TblRuta>(entity =>
            {
                entity.HasKey(e => e.IdRuta)
                    .HasName("pk_rutas");

                entity.ToTable("tbl_rutas");

                entity.Property(e => e.IdRuta)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_ruta")
                    .HasComment("Identificador de la tabla");

                entity.Property(e => e.IdLugarDestino)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_lugar_destino")
                    .HasComment("Corresponde al identificador del lugar de destino");

                entity.Property(e => e.IdLugarOrigen)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_lugar_origen")
                    .HasComment("Corresponde al identificador del lugar de origen");

                entity.Property(e => e.Precio)
                    .HasColumnType("numeric(14, 2)")
                    .HasColumnName("precio")
                    .HasComment("Precio del ticket de la ruta");

                entity.HasOne(d => d.IdLugarDestinoNavigation)
                    .WithMany(p => p.TblRutaIdLugarDestinoNavigations)
                    .HasForeignKey(d => d.IdLugarDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rutas_02");

                entity.HasOne(d => d.IdLugarOrigenNavigation)
                    .WithMany(p => p.TblRutaIdLugarOrigenNavigations)
                    .HasForeignKey(d => d.IdLugarOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rutas_01");
            });

            modelBuilder.Entity<TblUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("pk_usuarios");

                entity.ToTable("tbl_usuarios");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_usuario")
                    .HasComment("Identificador de la tabla");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("contrasena")
                    .HasComment("Corresponde a la contraseña del usuario");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasComment("Corresponde al correo del usuario");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .HasComment("Representa el estado del usuario. Posibles valores: A (Activo), I (Inactivo) ");

                entity.Property(e => e.IdPersona)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("id_persona")
                    .HasComment("Identificador de la persona");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TblUsuarios)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuarios");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

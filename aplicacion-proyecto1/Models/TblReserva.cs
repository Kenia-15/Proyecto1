using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblReserva
    {
        public TblReserva()
        {
            TblAsientosXReservas = new HashSet<TblAsientosXReserva>();
            TblHistorialPagos = new HashSet<TblHistorialPago>();
        }

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public string IdReserva { get; set; } = null!;
        /// <summary>
        /// Representa el horario de la reserva
        /// </summary>
        public string IdHorario { get; set; } = null!;
        /// <summary>
        /// Corresponde al usuario que reserva
        /// </summary>
        public string IdUsuario { get; set; } = null!;
        /// <summary>
        /// Corresponde al estado del pago. Posibles valores: P (Pendiente), C (Pagado).
        /// </summary>
        public string EstadoPago { get; set; } = null!;
        /// <summary>
        /// Fecha de la reserva
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Hora de la reserva
        /// </summary>
        public TimeSpan Hora { get; set; }

        public virtual TblHorario IdHorarioNavigation { get; set; } = null!;
        public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<TblAsientosXReserva> TblAsientosXReservas { get; set; }
        public virtual ICollection<TblHistorialPago> TblHistorialPagos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblHistorialPago
    {
        /// <summary>
        /// Identificador del historial
        /// </summary>
        public string IdHistorial { get; set; } = null!;
        /// <summary>
        /// Identificador de la reserva
        /// </summary>
        public string IdReserva { get; set; } = null!;
        /// <summary>
        /// Identificador de la promocion
        /// </summary>
        public string? IdPromocion { get; set; }
        /// <summary>
        /// Monto total de la reserva
        /// </summary>
        public decimal Monto { get; set; }
        /// <summary>
        /// Fecha en que se realizo el pago de la compra
        /// </summary>
        public DateTime FechaPago { get; set; }

        public virtual TblPromocione? IdPromocionNavigation { get; set; }
        public virtual TblReserva IdReservaNavigation { get; set; } = null!;
    }
}

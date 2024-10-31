using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblAsientosXReserva
    {
        /// <summary>
        /// Identificador del asiento
        /// </summary>
        public string IdAsiento { get; set; } = null!;
        /// <summary>
        /// Identificador de la reserva
        /// </summary>
        public string IdReserva { get; set; } = null!;
        /// <summary>
        /// Tipo de asiento. Posibles valores: P (Preferencial), C (Comun)
        /// </summary>
        public string TipoAsiento { get; set; } = null!;

        public virtual TblReserva IdReservaNavigation { get; set; } = null!;
    }
}

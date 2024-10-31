using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblHorariosXBuse
    {
        /// <summary>
        /// Identificador del horario
        /// </summary>
        public string IdHorario { get; set; } = null!;
        /// <summary>
        /// Identificador del bus
        /// </summary>
        public string IdBus { get; set; } = null!;
        /// <summary>
        /// Representa la cantidad de asientos disponibles por horario de cada bus
        /// </summary>
        public decimal AsientosDisponibles { get; set; }

        public virtual TblBuse IdBusNavigation { get; set; } = null!;
        public virtual TblHorario IdHorarioNavigation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblBuse
    {
        public TblBuse()
        {
            TblHorariosXBuses = new HashSet<TblHorariosXBuse>();
        }

        /// <summary>
        /// Identificador del bus
        /// </summary>
        public string IdBus { get; set; } = null!;
        /// <summary>
        /// Capacidad maxima del bus
        /// </summary>
        public string Capacidad { get; set; } = null!;

        public virtual ICollection<TblHorariosXBuse> TblHorariosXBuses { get; set; }
    }
}

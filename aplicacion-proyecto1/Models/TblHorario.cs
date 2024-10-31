using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblHorario
    {
        public TblHorario()
        {
            TblHorariosXBuses = new HashSet<TblHorariosXBuse>();
            TblReservas = new HashSet<TblReserva>();
        }

        /// <summary>
        /// Identificador del horario
        /// </summary>
        public string IdHorario { get; set; } = null!;
        /// <summary>
        /// Identificador de la ruta
        /// </summary>
        public string IdRuta { get; set; } = null!;
        /// <summary>
        /// Hora de la ruta
        /// </summary>
        public TimeSpan Hora { get; set; }

        public virtual ICollection<TblHorariosXBuse> TblHorariosXBuses { get; set; }
        public virtual ICollection<TblReserva> TblReservas { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblPromocione
    {
        public TblPromocione()
        {
            IdRuta = new HashSet<TblRuta>();
        }

        /// <summary>
        /// Identificador de la promocion
        /// </summary>
        public string IdPromocion { get; set; } = null!;
        /// <summary>
        /// Descripcion de la promocion
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Porcentaje de descuento de la promocion
        /// </summary>
        public string Descuento { get; set; } = null!;
        /// <summary>
        /// Fecha inicial de la promocion
        /// </summary>
        public DateTime FechaInicial { get; set; }
        /// <summary>
        /// Fecha final de la promocion
        /// </summary>
        public DateTime FechaFinal { get; set; }

        public virtual ICollection<TblRuta> IdRuta { get; set; }
    }
}

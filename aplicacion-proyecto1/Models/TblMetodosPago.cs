using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblMetodosPago
    {
        public TblMetodosPago()
        {
            TblPersonas = new HashSet<TblPersona>();
        }

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public string IdMetodoPago { get; set; } = null!;
        /// <summary>
        /// Nombre del metodo de pago
        /// </summary>
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<TblPersona> TblPersonas { get; set; }
    }
}

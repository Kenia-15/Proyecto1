using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblRuta
    {
        public TblRuta()
        {
            IdPromocions = new HashSet<TblPromocione>();
        }

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public string IdRuta { get; set; } = null!;
        /// <summary>
        /// Corresponde al identificador del lugar de origen
        /// </summary>
        public string IdLugarOrigen { get; set; } = null!;
        /// <summary>
        /// Corresponde al identificador del lugar de destino
        /// </summary>
        public string IdLugarDestino { get; set; } = null!;
        /// <summary>
        /// Precio del ticket de la ruta
        /// </summary>
        public decimal Precio { get; set; }

        public virtual TblLugare IdLugarDestinoNavigation { get; set; } = null!;
        public virtual TblLugare IdLugarOrigenNavigation { get; set; } = null!;

        public virtual ICollection<TblPromocione> IdPromocions { get; set; }
    }
}

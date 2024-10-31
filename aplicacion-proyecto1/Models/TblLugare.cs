using System;
using System.Collections.Generic;

namespace aplicacion_proyecto1.Models
{
    public partial class TblLugare
    {
        public TblLugare()
        {
            TblRutaIdLugarDestinoNavigations = new HashSet<TblRuta>();
            TblRutaIdLugarOrigenNavigations = new HashSet<TblRuta>();
        }

        public string IdLugar { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<TblRuta> TblRutaIdLugarDestinoNavigations { get; set; }
        public virtual ICollection<TblRuta> TblRutaIdLugarOrigenNavigations { get; set; }
    }
}

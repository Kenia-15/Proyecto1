using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacion_proyecto1.Models
{
    public partial class TblPersona
    {
        public TblPersona()
        {
            TblUsuarios = new HashSet<TblUsuario>();
        }

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public string IdPersona { get; set; } = null!;
        /// <summary>
        /// Referencia el metodo de pago seleccionado por la persona
        /// </summary>
        [ForeignKey("IdMetodoPago")]
        public string IdMetodoPago { get; set; } = null!;
        /// <summary>
        /// Corresponde al numero de identificacion de la persona
        /// </summary>
        public string NumeroIdentificacion { get; set; } = null!;
        /// <summary>
        /// Corresponde al primer nombre de la persona
        /// </summary>
        public string PrimerNombre { get; set; } = null!;
        /// <summary>
        /// Corresponde al segundo nombre de la persona
        /// </summary>
        public string? SegundoNombre { get; set; }
        /// <summary>
        /// Corresponde al primer apellido de la persona
        /// </summary>
        public string PrimerApellido { get; set; } = null!;
        /// <summary>
        /// Corresponde al segundo apellido de la persona
        /// </summary>
        public string? SegundoApellido { get; set; }

        public virtual TblMetodosPago IdMetodoPagoNavigation { get; set; } = null!;
        public virtual ICollection<TblUsuario> TblUsuarios { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacion_proyecto1.Models
{
    public partial class TblUsuario
    {
        public TblUsuario()
        {
            TblReservas = new HashSet<TblReserva>();
        }

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public string IdUsuario { get; set; } = null!;
        /// <summary>
        /// Identificador de la persona
        /// </summary>
        [ForeignKey("IdPersona")]
        public string IdPersona { get; set; } = null!;
        /// <summary>
        /// Corresponde al correo del usuario
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Corresponde a la contraseña del usuario
        /// </summary>
        public string Contrasena { get; set; } = null!;
        /// <summary>
        /// Representa el estado del usuario. Posibles valores: A (Activo), I (Inactivo) 
        /// </summary>
        public string Estado { get; set; } = null!;        

        public virtual TblPersona IdPersonaNavigation { get; set; } = null!;
        public virtual ICollection<TblReserva> TblReservas { get; set; }

       // public List<SelectListItem> listaMetodosPago { get; set; }
    }
}

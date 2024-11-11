namespace aplicacion_proyecto1.Models
{
    public class MdlFactura
    {
        public TblHorario? horario { get; set; }
        public TblRuta? ruta { get; set; }
        public TblPersona? persona { get; set; }
        public TblReserva? reserva { get; set; }    
        public TblHorariosXBuse horarioBus { get; set; } = null!;
        /// <summary>
        public TblHistorialPago historial { get; set; } = null!;
        /// <summary>
    }
}

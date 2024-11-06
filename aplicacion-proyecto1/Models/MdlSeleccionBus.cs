namespace aplicacion_proyecto1.Models
{
    public class MdlSeleccionBus
    {
        public TblHorariosXBuse horarioBus  { get; set; }

        public TblRuta rutas { get; set; }

        public TblHorario horarios { get; set; }

        public TblAsientosXReserva asientosReserva { get; set; }

        public int cantidadPersonas { get; set; }

        public DateTime fecha { get; set; } 
    }
}

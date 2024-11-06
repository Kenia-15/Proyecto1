using aplicacion_proyecto1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace aplicacion_proyecto1.Controllers
{
    public class SeleccionBusesController : Controller
    {
        private readonly p_busesContext _context;
        public IConfiguration Configuration { get; }

        public SeleccionBusesController(p_busesContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Seleccion(string id)
        {
            ViewData["LugarOrigen"] = new SelectList(_context.TblLugares, "IdLugar", "Descripcion");
            ViewData["LugarDestino"] = new SelectList(_context.TblLugares, "IdLugar", "Descripcion");

            TempData["Usuario"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult Seleccion(MdlSeleccionBus seleccion)
        {
            var lorigen = seleccion.rutas.IdLugarOrigen.ToString();
            var ldestino = seleccion.rutas.IdLugarDestino.ToString();
            var ruta = "";
            var query = "";

            try
            {
                query = "Select t.id_ruta from p_buses.dbo.tbl_rutas t where t.id_lugar_origen = '" + lorigen + "' and t.id_lugar_destino = '" + ldestino + "';";

                using (SqlConnection sqlConn = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
                {
                    using (SqlCommand com = new SqlCommand(query, sqlConn))
                    {
                        sqlConn.Open();

                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                ruta = read.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ruta = null;
                TempData["Mensaje"] = ex;
            }


            var horario = _context.TblHorarios.Where(x => x.IdRuta == ruta).OrderBy(x => x.Hora).Select(x => new { IdHorario = x.IdHorario, Hora = x.Hora }).ToList();

            if (horario == null || horario.Count == 0)
            {
                ViewData["ValHorario"] = "N";
                TempData["Mensaje"] = "No hay horarios para esa ruta";
            } else
            {
                ViewData["ValHorario"] = "S";
                ViewData["Horario"] = new SelectList(horario, "IdHorario", "Hora");
            }

            ViewData["LugarOrigen"] = new SelectList(_context.TblLugares, "IdLugar", "Descripcion");
            ViewData["LugarDestino"] = new SelectList(_context.TblLugares, "IdLugar", "Descripcion");
            ViewData["Fecha"] = seleccion.fecha.ToString("dd/MM/yyyy");

            ViewData["idLugarOrigen"] = lorigen;
            ViewData["idLugarDestino"] = ldestino;

            return View();
        }

        public IActionResult Reserva(string id)
        {
            ViewData["LugarOrigen"] = new SelectList(_context.TblLugares, "IdLugar", "Descripcion");
            ViewData["LugarDestino"] = new SelectList(_context.TblLugares, "IdLugar", "Descripcion");

            TempData["Usuario"] = id;

            return View();
        }

        [HttpPost]
        public IActionResult Reserva(MdlSeleccionBus seleccion)
        {
            string idHorario = seleccion.horarioBus.IdHorario;
            string idLugarOrigen = seleccion.rutas.IdLugarOrigen;
            string idLugarDestino = seleccion.rutas.IdLugarDestino;
            string fecha = seleccion.fecha.ToString("dd/MM/yyyy");
            int cantAsientos = seleccion.cantidadPersonas;

            TblHorariosXBuse horarioBus = new TblHorariosXBuse();

            horarioBus = _context.TblHorariosXBuses.FirstOrDefault(t => t.IdHorario == idHorario);

            ViewData["Asientos"] = horarioBus.AsientosDisponibles;

            //lógica para poder ver si hay asientos disponibles para la fecha y hora seleccionadas, si no hay
            //mostrar mensaje, si hay mostrar vista para continuar con la reserva

            //ver como enviar los datos para las diferentes vistas

            //ver como pasar el id de la persona en las diferentes vistas


           return View();
        }
    }
}

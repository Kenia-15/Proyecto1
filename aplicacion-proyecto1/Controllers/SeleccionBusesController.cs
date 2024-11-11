using aplicacion_proyecto1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        /*Procedimiento encargado de obtener la informaciión de la vista Selección para enviarla al controlador ReservasController
         el cual se encargará de realizar la lógica de la reserva*/
        [HttpPost]
        public IActionResult Seleccion(MdlSeleccionBus seleccion, string id, string hora, string fecha)
        {
            var ruta = "";
            var query = "";
            var lorigen = seleccion.rutas.IdLugarOrigen.ToString();
            var ldestino = seleccion.rutas.IdLugarDestino.ToString();
            TblHorariosXBuse horarioBus = new TblHorariosXBuse();
            TblReserva reserva = new TblReserva();

            TempData["Usuario"] = id;

            //Se obtiene la ruta según el lugar de origen y destino seleccionados
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

            //Se obtiene los horarios disponibles para la ruta seleccionada
            var horario = _context.TblHorarios.Where(x => x.IdRuta == ruta).OrderBy(x => x.Hora).Select(x => new { IdHorario = x.IdHorario, Hora = x.Hora }).ToList();

            if (horario == null || horario.Count == 0)
            {
                ViewData["ValHorario"] = "N";
                TempData["Mensaje"] = "No hay horarios para esa ruta";
            }
            else
            {
                //Si se encontró un horario, se muestra en la vista
                ViewData["ValHorario"] = "S";
                ViewBag.Lista = horario;
                ViewData["Fecha"] = fecha;
            }

            ViewData["LugarOrigen"] = new SelectList(_context.TblLugares, "IdLugar", "Descripcion");
            ViewData["LugarDestino"] = new SelectList(_context.TblLugares, "IdLugar", "Descripcion");

            ViewData["Fecha"] = fecha;
            ViewData["Hora"] = hora;
            TempData["idLugarOrigen"] = lorigen;
            TempData["idLugarDestino"] = ldestino;            

            return View();
        }
    }
}

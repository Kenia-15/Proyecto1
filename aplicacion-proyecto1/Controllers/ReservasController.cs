using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aplicacion_proyecto1.Models;
using System.Security.Cryptography;
using System.Diagnostics.Metrics;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using NuGet.Protocol;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aplicacion_proyecto1.Controllers
{
    public class ReservasController : Controller
    {
        private readonly p_busesContext _context;
        public IConfiguration Configuration { get; }

        public ReservasController(p_busesContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var p_busesContext = _context.TblReservas.Include(t => t.IdHorarioNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await p_busesContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TblReservas == null)
            {
                return NotFound();
            }

            var tblReserva = await _context.TblReservas
                .Include(t => t.IdHorarioNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (tblReserva == null)
            {
                return NotFound();
            }

            return View(tblReserva);
        }

      

        // GET: Reservas/Create
        //
        public IActionResult Create(string id, string idH, string valH, string valF)
        {
            ViewData["IdHorario"] = new SelectList(_context.TblHorarios, "IdHorario", "IdHorario");
            ViewData["IdUsuario"] = new SelectList(_context.TblUsuarios, "IdUsuario", "IdUsuario");
            
            TblRuta ruta = new TblRuta();
            TblHorario horario = new TblHorario();
            TblReserva reservas = new TblReserva();
            List<string> asientos = new List<string>();
            List<string> Tablero = new List<string>();
            TblHorariosXBuse hBus = new TblHorariosXBuse();            
            decimal cantidadAsientos = 0;
            string[] listaNueva = new string[28];
            string fecha = DateTime.Parse(valF).ToString("dd/MM/yyyy");
            int count = 0;

            //Se obtiene el horario
            horario = _context.TblHorarios.FirstOrDefault(p => p.IdHorario == idH);

            //Se obtiene la ruta
            ruta = _context.TblRutas.FirstOrDefault(p => p.IdRuta == horario.IdRuta);

            //Se obtienen los asientos por reserva
            try
            {
                var queryAsientos = "select t.id_asiento from p_buses.dbo.tbl_asientos_x_reserva t, p_buses.dbo.tbl_reservas r where t.id_reserva = r.id_reserva and r.fecha = '" + fecha + "' and r.hora = '" + valH + "';";


                using (SqlConnection sqlConn = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
                {
                    using (SqlCommand com = new SqlCommand(queryAsientos, sqlConn))
                    {
                        sqlConn.Open();

                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                asientos.Add(read.GetValue(0).ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                asientos = null;
                TempData["Mensaje"] = ex;
            }

            //Se obtiene la cantidad de asientos para la ruta con fecha y hora que ya están reservados
            cantidadAsientos = 28 - asientos.Count();

            //Se llena el tablero de asientos con el número de asiento secuencial del 1 al 28 (cada bus cuenta con 28 asientos)
            /*for (int i = 0; i < listaNueva.Count(); i++)
            {
                count = count + 1;
                listaNueva[i] = count.ToString();
                Tablero.Add(listaNueva[i]);
            }                       

            //Se busca cuáles asientos están ocupados para proceder a eliminarlos
            for (int i = 0; i < listaNueva.Count(); i++)
            {
                for (int j = 0; j < asientos.Count(); j++)
                    {
                        if (listaNueva[i] == asientos[j])
                        {
                            Tablero.Remove(listaNueva[i]);
                        }
                    }
             }
            //Se pasa la lista de asientos a la vista
            ViewBag.Lista = Tablero;*/

            ViewData["Asientos"] = cantidadAsientos;
            TempData["Usuario"] = id;
            ViewData["Horario"] = idH;
            ViewData["Fecha"] = fecha;
            ViewData["Hora"] = valH;
            ViewData["Precio"] = ruta.Precio;

            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReserva,IdHorario,IdUsuario,EstadoPago,Fecha,Hora,NumeroAsientos")] TblReserva tblReserva, string id, string fecha, string hora, decimal asientos, string hor)
        {
            //, IFormCollection asiento
            //Se obtiene el número o números de asiento a reservar            
            //lista = asiento["asiento"];
            //lista = "4,5,6";
            decimal lista = tblReserva.NumeroAsientos;
            //string[] ListaAsientos = lista.Split(',');
            decimal cantAsientosOcupados = 0;
            List<String> asientosOcupados = new List<String>();
            decimal asientosQuedan = 0;

            //--
            TblHistorialPago historial = new TblHistorialPago();
            TblAsientosXReserva asientosXreserva = new TblAsientosXReserva();
            TblHorario horario = new TblHorario();
            TblRuta ruta = new TblRuta();

            //--
            var secuenciaIdRs = 0;
            var secuenciaIdH = 0;
            var countReserva = _context.TblReservas.Count();
            var countHistorial = _context.TblHistorialPagos.Count();            
            var query = "";
            string fec = tblReserva.Fecha.ToString("dd/MM/yyyy");

            try
            {
                //Se realiza el autoincrementado de la reserva
                    if (countReserva == 0)
                {
                    secuenciaIdRs = 1;
                }
                else
                {
                    secuenciaIdRs = _context.TblReservas.Max(p => Convert.ToInt32(p.IdReserva));
                    secuenciaIdRs += 1;
                }

                //Se realiza el autoincrementado del historial de la reserva
                if (countHistorial == 0)
                {
                    secuenciaIdH = 1;
                }
                else
                {
                    secuenciaIdH = _context.TblHistorialPagos.Max(p => Convert.ToInt32(p.IdHistorial));
                    secuenciaIdH += 1;
                }


                //Se obtienen los asientos por reserva
                try
                {
                    var queryAsientos = "select t.id_asiento from p_buses.dbo.tbl_asientos_x_reserva t, p_buses.dbo.tbl_reservas r where t.id_reserva = r.id_reserva and r.fecha = '" + fec + "' and r.hora = '" + tblReserva.Hora + "';";


                    using (SqlConnection sqlConn = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
                    {
                        using (SqlCommand com = new SqlCommand(queryAsientos, sqlConn))
                        {
                            sqlConn.Open();

                            using (SqlDataReader read = com.ExecuteReader())
                            {
                                while (read.Read())
                                {
                                    asientosOcupados.Add(read.GetValue(0).ToString());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    asientosOcupados = null;
                    TempData["Mensaje"] = ex;
                }

                //Se recopila la información necesaria para realizar la reserva 
                decimal cantAsientos = lista;
                horario = _context.TblHorarios.FirstOrDefault(p => p.IdHorario == hor);
                ruta = _context.TblRutas.FirstOrDefault(p => p.IdRuta == horario.IdRuta);

                //Se obtiene la cantidad de asientos para la ruta con fecha y hora que ya están reservados
                cantAsientosOcupados = 28 - asientosOcupados.Count();
                               
                //Se actualiza la cantidad de asientos disponibles
                try
                {
                    asientosQuedan = cantAsientosOcupados - cantAsientos;
                    var queryUpdate = "update p_buses.dbo.tbl_horarios_x_buses set asientos_disponibles = '"+ asientosQuedan + "' where id_horario = '"+ horario.IdHorario+ "';";
                        using (SqlConnection sqlConn = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
                        {
                            using (SqlCommand com = new SqlCommand(queryUpdate, sqlConn))
                            {
                                sqlConn.Open();
                                com.ExecuteNonQuery();
                                sqlConn.Close();
                            }
                        }
                }
                catch (Exception ex)
                {
                    TempData["Mensaje"] = "Ocurrió un error al intentar realizar la reserva" + ex.ToString();
                }

                //Se realiza la inserción de la reserva en la tabla tbl_reservas 
                tblReserva.IdReserva = secuenciaIdRs.ToString();
                tblReserva.IdHorario = hor;
                tblReserva.Fecha = tblReserva.Fecha;
                tblReserva.Hora = tblReserva.Hora;
                tblReserva.EstadoPago = "C";
                tblReserva.NumeroAsientos = cantAsientos;
                tblReserva.IdUsuario = id;
                _context.Add(tblReserva);


                //Se inserta la reserva en el historial
                historial.IdHistorial = secuenciaIdH.ToString();
                historial.IdReserva = secuenciaIdRs.ToString();
                historial.Monto = ruta.Precio * cantAsientos;
                historial.FechaPago = tblReserva.Fecha;
                _context.Add(historial);

                await _context.SaveChangesAsync();

                ViewData["IdHorario"] = new SelectList(_context.TblHorarios, "IdHorario", "IdHorario", tblReserva.IdHorario);
                ViewData["IdUsuario"] = new SelectList(_context.TblUsuarios, "IdUsuario", "IdUsuario", tblReserva.IdUsuario);
                return RedirectToAction("Index", "Factura");                

            }
            catch(Exception e)
            {
                TempData["Mensaje"] = "Ocurrió un error al intentar realizar la reserva" + e.ToString();
                return View(tblReserva);
            }
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TblReservas == null)
            {
                return NotFound();
            }

            var tblReserva = await _context.TblReservas.FindAsync(id);
            if (tblReserva == null)
            {
                return NotFound();
            }
            ViewData["IdHorario"] = new SelectList(_context.TblHorarios, "IdHorario", "IdHorario", tblReserva.IdHorario);
            ViewData["IdUsuario"] = new SelectList(_context.TblUsuarios, "IdUsuario", "IdUsuario", tblReserva.IdUsuario);
            return View(tblReserva);
        }

       


        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdReserva,IdHorario,IdUsuario,EstadoPago,Fecha,Hora")] TblReserva tblReserva)
        {
            if (id != tblReserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblReserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblReservaExists(tblReserva.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHorario"] = new SelectList(_context.TblHorarios, "IdHorario", "IdHorario", tblReserva.IdHorario);
            ViewData["IdUsuario"] = new SelectList(_context.TblUsuarios, "IdUsuario", "IdUsuario", tblReserva.IdUsuario);
            return View(tblReserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TblReservas == null)
            {
                return NotFound();
            }

            var tblReserva = await _context.TblReservas
                .Include(t => t.IdHorarioNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (tblReserva == null)
            {
                return NotFound();
            }

            return View(tblReserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TblReservas == null)
            {
                return Problem("Entity set 'p_busesContext.TblReservas'  is null.");
            }
            var tblReserva = await _context.TblReservas.FindAsync(id);
            if (tblReserva != null)
            {
                _context.TblReservas.Remove(tblReserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblReservaExists(string id)
        {
          return (_context.TblReservas?.Any(e => e.IdReserva == id)).GetValueOrDefault();
        }
    }
}

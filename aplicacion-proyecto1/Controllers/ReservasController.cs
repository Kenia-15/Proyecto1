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
using System.Globalization;

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
        public IActionResult Create(string id, string idH, string valH, DateTime valF)
        {
            ViewData["IdHorario"] = new SelectList(_context.TblHorarios, "IdHorario", "IdHorario");
            ViewData["IdUsuario"] = new SelectList(_context.TblUsuarios, "IdUsuario", "IdUsuario");
            
            TblRuta ruta = new TblRuta();
            TblHorario horario = new TblHorario();
            TblReserva reservas = new TblReserva();
            decimal asientos = 0;        
            decimal cantidadAsientos = 0;
            string fecha = valF.ToString("dd/MM/yyyy");
            int count = 0;

            //Se obtiene el horario
            horario = _context.TblHorarios.FirstOrDefault(p => p.IdHorario == idH);

            //Se obtiene la ruta
            ruta = _context.TblRutas.FirstOrDefault(p => p.IdRuta == horario.IdRuta);

            //Se obtienen los asientos ocupados por reserva para la fecha y hora indicadas
            reservas = _context.TblReservas.FirstOrDefault(p => p.Hora == valH && p.Fecha.Equals(fecha));            

            //Se obtiene la cantidad de asientos para la ruta con fecha y hora que ya están reservados
            if (reservas == null)
            {
                cantidadAsientos = 28 - 0;
            } else
            {
                cantidadAsientos = 28 - reservas.NumeroAsientos;
            }

            ViewData["Asientos"] = cantidadAsientos;
            TempData["Usuario"] = id;
            ViewData["Horario"] = idH;
            ViewData["Fecha"] = valF;
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
            decimal lista = tblReserva.NumeroAsientos;
            decimal asientosOcupados = 0;
            decimal asientosQuedan = 0;

            //--
            TblLugare lugarOrigen = new TblLugare();
            TblLugare lugarDestino = new TblLugare();
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
            string fec = fecha;

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

                //Se recopila la información necesaria para realizar la reserva 
                decimal cantAsientos = lista;
                horario = _context.TblHorarios.FirstOrDefault(p => p.IdHorario == hor);
                ruta = _context.TblRutas.FirstOrDefault(p => p.IdRuta == horario.IdRuta);
                lugarOrigen = _context.TblLugares.FirstOrDefault(p => p.IdLugar == ruta.IdLugarOrigen);
                lugarDestino = _context.TblLugares.FirstOrDefault(p => p.IdLugar == ruta.IdLugarDestino);

                //Se obtienen los asientos ocupados por reserva para la fecha y hora indicadas
                asientosOcupados = asientos;
                               
                //Se actualiza la cantidad de asientos disponibles
                try
                {
                    asientosQuedan = asientosOcupados - cantAsientos;
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

                string idR = secuenciaIdRs.ToString();
                string formatoFecha = DateTime.Parse(fecha).ToString("dd/MM/yyyy");
                DateTime fechaDate = Convert.ToDateTime(fecha);
                decimal monto = ruta.Precio* cantAsientos;

                //Se realiza la inserción de la reserva en la tabla tbl_reservas 
                tblReserva.IdReserva = secuenciaIdRs.ToString();
                tblReserva.IdHorario = hor;
                tblReserva.Fecha = fechaDate;
                tblReserva.Hora = tblReserva.Hora;
                tblReserva.EstadoPago = "C";
                tblReserva.NumeroAsientos = cantAsientos;
                tblReserva.IdUsuario = id;
                _context.Add(tblReserva);

                //Se inserta la reserva en el historial
                historial.IdHistorial = secuenciaIdH.ToString();
                historial.IdReserva = secuenciaIdRs.ToString();
                historial.Monto = ruta.Precio * cantAsientos;
                historial.FechaPago = fechaDate;
                _context.Add(historial);

                await _context.SaveChangesAsync();

                TempData["IdReserva"] = idR;
                TempData["IdUsuario"] = id;
                TempData["FechaR"] = fechaDate.ToString("dd/MM/yyyy");
                TempData["lOrigen"] = lugarOrigen.Descripcion;
                TempData["lDestino"] = lugarDestino.Descripcion;
                TempData["horaR"] = hora;
                TempData["montoR"] = monto.ToString();
                TempData["cantidad"] = cantAsientos.ToString();

                return RedirectToAction("Index", "Factura", new { idReserva = idR, fecha = fechaDate.ToString("dd/MM/yyyy"), lorigen = lugarOrigen.Descripcion, ldestino = lugarDestino.Descripcion, horaR = hora, total = monto.ToString(), cantA = cantAsientos.ToString() });             

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

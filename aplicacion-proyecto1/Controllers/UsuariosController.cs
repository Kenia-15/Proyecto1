using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aplicacion_proyecto1.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;

namespace aplicacion_proyecto1.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly p_busesContext _context;

        public UsuariosController(p_busesContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var p_busesContext = _context.TblUsuarios.Include(t => t.IdPersonaNavigation);
            return View(await p_busesContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TblUsuarios == null)
            {
                return NotFound();
            }

            var tblUsuario = await _context.TblUsuarios
                .Include(t => t.IdPersonaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tblUsuario == null)
            {
                return NotFound();
            }

            return View(tblUsuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona");
            return View();
        }

        public JsonResult obtenerMetodosPago()
        {
            List<TblMetodosPago> lista = new List<TblMetodosPago>();
            var query = "select t.id_metodo_pago, t.descripcion from p_buses.dbo.tbl_metodos_pago t;";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
                {
                    using (SqlCommand com = new SqlCommand(query, sqlConn))
                    {
                        sqlConn.Open();

                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                lista.Add(new TblMetodosPago
                                {
                                    IdMetodoPago = read.GetString(0),
                                    Descripcion = read.GetString(1)
                                });
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return Json(lista);
        }

        public void crearPersona()
        {

        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,IdPersona,Email,Contrasena,Estado")] TblUsuario tblUsuario)
        {
            var secuenciaIdUser = 0;
            var secuenciaIdPer = 0;
            var countUser = _context.TblUsuarios.Count();
            var countPer = _context.TblPersonas.Count();

            //Se realiza el autoincrementado del usuario
            if (countUser == 0)
            {
                secuenciaIdUser = 1;
            }
            else
            {
                secuenciaIdUser = _context.TblUsuarios.Max(p => Convert.ToInt32(p.IdUsuario));
                secuenciaIdUser += 1;
            }

            //Se realiza el autoincrementado de la persona
            if (countPer == 0)
            {
                secuenciaIdPer = 1;
            }
            else
            {
                secuenciaIdPer = _context.TblPersonas.Max(p => Convert.ToInt32(p.IdPersona));
                secuenciaIdPer += 1;
            }

            //Se realiza el registro de la persona
            //var prueba = tblUsuario.IdPersonaNavigation.

            try
            {
                tblUsuario.IdUsuario = secuenciaIdUser.ToString();
                _context.Add(tblUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblUsuario.IdPersona);
            return View(tblUsuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TblUsuarios == null)
            {
                return NotFound();
            }

            var tblUsuario = await _context.TblUsuarios.FindAsync(id);
            if (tblUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblUsuario.IdPersona);
            return View(tblUsuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdUsuario,IdPersona,Email,Contrasena,Estado")] TblUsuario tblUsuario)
        {
            if (id != tblUsuario.IdUsuario)
            {
                return NotFound();
            }
            try
                {
                    _context.Update(tblUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblUsuarioExists(tblUsuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["IdPersona"] = new SelectList(_context.TblPersonas, "IdPersona", "IdPersona", tblUsuario.IdPersona);
            return View(tblUsuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TblUsuarios == null)
            {
                return NotFound();
            }

            var tblUsuario = await _context.TblUsuarios
                .Include(t => t.IdPersonaNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tblUsuario == null)
            {
                return NotFound();
            }

            return View(tblUsuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TblUsuarios == null)
            {
                return Problem("Entity set 'p_busesContext.TblUsuarios'  is null.");
            }
            var tblUsuario = await _context.TblUsuarios.FindAsync(id);
            if (tblUsuario != null)
            {
                _context.TblUsuarios.Remove(tblUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblUsuarioExists(string id)
        {
          return (_context.TblUsuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}

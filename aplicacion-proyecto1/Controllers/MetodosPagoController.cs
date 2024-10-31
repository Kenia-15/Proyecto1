using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aplicacion_proyecto1.Models;
using Microsoft.Data.SqlClient;

namespace aplicacion_proyecto1.Controllers
{
    public class MetodosPagoController : Controller
    {
        private readonly p_busesContext _context;

        public MetodosPagoController(p_busesContext context)
        {
            _context = context;
        }

        // GET: MetodosPago
        public async Task<IActionResult> Index()
        {
              return _context.TblMetodosPagos != null ? 
                          View(await _context.TblMetodosPagos.ToListAsync()) :
                          Problem("Entity set 'p_busesContext.TblMetodosPagos'  is null.");
        }

        // GET: MetodosPago/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TblMetodosPagos == null)
            {
                return NotFound();
            }

            var tblMetodosPago = await _context.TblMetodosPagos
                .FirstOrDefaultAsync(m => m.IdMetodoPago == id);
            if (tblMetodosPago == null)
            {
                return NotFound();
            }

            return View(tblMetodosPago);
        }

        // GET: MetodosPago/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetodosPago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMetodoPago,Descripcion")] TblMetodosPago tblMetodosPago)
        {
            var secuenciaId = 0;
            var count = _context.TblMetodosPagos.Count();

            if (count == 0) 
            {
                secuenciaId = 1;
            } else {
                secuenciaId = _context.TblMetodosPagos.Max(p => Convert.ToInt32(p.IdMetodoPago));
                secuenciaId += 1;
            }         

            if (ModelState.IsValid)
            {
                tblMetodosPago.IdMetodoPago = secuenciaId.ToString();
                _context.Add(tblMetodosPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblMetodosPago);
        }

        // GET: MetodosPago/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TblMetodosPagos == null)
            {
                return NotFound();
            }

            var tblMetodosPago = await _context.TblMetodosPagos.FindAsync(id);
            if (tblMetodosPago == null)
            {
                return NotFound();
            }
            return View(tblMetodosPago);
        }

        // POST: MetodosPago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdMetodoPago,Descripcion")] TblMetodosPago tblMetodosPago)
        {
            if (id != tblMetodosPago.IdMetodoPago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMetodosPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMetodosPagoExists(tblMetodosPago.IdMetodoPago))
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
            return View(tblMetodosPago);
        }

        // GET: MetodosPago/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TblMetodosPagos == null)
            {
                return NotFound();
            }

            var tblMetodosPago = await _context.TblMetodosPagos
                .FirstOrDefaultAsync(m => m.IdMetodoPago == id);
            if (tblMetodosPago == null)
            {
                return NotFound();
            }

            return View(tblMetodosPago);
        }

        // POST: MetodosPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TblMetodosPagos == null)
            {
                return Problem("Entity set 'p_busesContext.TblMetodosPagos'  is null.");
            }
            var tblMetodosPago = await _context.TblMetodosPagos.FindAsync(id);
            if (tblMetodosPago != null)
            {
                _context.TblMetodosPagos.Remove(tblMetodosPago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMetodosPagoExists(string id)
        {
          return (_context.TblMetodosPagos?.Any(e => e.IdMetodoPago == id)).GetValueOrDefault();
        }
    }
}

using aplicacion_proyecto1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace aplicacion_proyecto1.Controllers
{
    public class FacturaController : Controller
    {
        private readonly p_busesContext _context;

        public FacturaController(p_busesContext context)
        {
            _context = context;
        }

        public IActionResult Index(string idReserva, string fecha, string lorigen, string ldestino, string horaR, string total, string cantA)
        {
            string id_reserva = TempData["IdReserva"].ToString();
            string fechaReserva = TempData["FechaR"].ToString();
            string lugarOrigen = TempData["lOrigen"].ToString();
            string lugarDestino = TempData["lDestino"].ToString();
            string horaRes = TempData["horaR"].ToString();
            string totalR = TempData["montoR"].ToString();
            string cantAsientos = TempData["cantidad"].ToString();

            ViewData["NumeroAsientos"] = cantAsientos;
            ViewData["Monto"] = totalR;
            ViewData["Fecha"] = fechaReserva;
            ViewData["Hora"] = horaRes;
            ViewData["idLugarOrigen"] = lugarOrigen;
            ViewData["idLugarDestino"] = lugarDestino;

            return View();
        }

    }
}

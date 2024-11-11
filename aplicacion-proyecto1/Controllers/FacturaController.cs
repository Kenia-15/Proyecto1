using aplicacion_proyecto1.Models;
using Microsoft.AspNetCore.Mvc;

namespace aplicacion_proyecto1.Controllers
{
    public class FacturaController : Controller
    {
        private readonly p_busesContext _context;

        public IConfiguration Configuration { get; }

        public FacturaController(IConfiguration configuration, p_busesContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        public IActionResult Index(string id, string reserva)
        {
            return View();
        }



    }
}

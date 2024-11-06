using aplicacion_proyecto1.Models;
using Microsoft.AspNetCore.Mvc;

namespace aplicacion_proyecto1.Controllers
{
    public class PaginaPrincipalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly p_busesContext _context;
        public IConfiguration Configuration { get; }

        public PaginaPrincipalController(IConfiguration configuration, p_busesContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        public IActionResult Inicio()
        {
            return View();
        }

        /*[HttpPost]
        public IActionResult Inicio()
        {

        }*/
    }
}

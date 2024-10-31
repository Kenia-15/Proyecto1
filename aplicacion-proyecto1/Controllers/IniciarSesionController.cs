using aplicacion_proyecto1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace aplicacion_proyecto1.Controllers
{
    public class IniciarSesionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly p_busesContext _context;
        public IConfiguration Configuration { get; }

        public IniciarSesionController(IConfiguration configuration, p_busesContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }


        [HttpPost]
        public IActionResult IniciarSesion(IniciarSesion usuario)
        {
            var validaUsuario = 0;
            var query = "";

            if (usuario.email == null || usuario.contrasena == null)
            {
                TempData["Mensaje"] = "Debe ingresar el usuario y la contraseña";
            } 
            else {
                try
                {
                    query = "Select 1 from p_buses.dbo.tbl_usuarios t where t.email = '" + usuario.email + "' and t.contrasena = '" + usuario.contrasena + "';";

                    using (SqlConnection sqlConn = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
                    {
                        using (SqlCommand com = new SqlCommand(query, sqlConn))
                        {
                            sqlConn.Open();

                            using (SqlDataReader read = com.ExecuteReader())
                            {
                                while (read.Read())
                                {
                                    validaUsuario = read.GetInt32(0);
                                }
                            }
                        }
                    }
                }catch (Exception ex)
                {
                    validaUsuario = 0;
                    TempData["Mensaje"] = ex;
                }
            }

            if (validaUsuario == 0 && (usuario.email != null || usuario.contrasena != null))
            {
                TempData["Mensaje"] = "El usuario o la contraseña son incorrectos";
            }


            var idUsuario = (from i in _context.TblUsuarios where i.Email == usuario.email && i.Contrasena == usuario.contrasena select i.IdUsuario);

            TempData["Usuario"] = idUsuario.First();

            return RedirectToAction("Privacy", "Home");
        }

    }
}

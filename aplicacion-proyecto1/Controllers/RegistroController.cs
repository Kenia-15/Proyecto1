using aplicacion_proyecto1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;

namespace aplicacion_proyecto1.Controllers
{
    public class RegistroController : Controller
    {

        private readonly p_busesContext _context;
        public IConfiguration Configuration { get; }

        public RegistroController(p_busesContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            ViewData["MetodosPago"] = new SelectList(_context.TblMetodosPagos, "IdMetodoPago", "Descripcion");
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(MdlRegistro registro)
        {
            var idPersona = "";
            var valido = false;

            idPersona = insertarPersona(registro);
            valido = insertarUsuario(registro, idPersona);

            if (valido == true)
            {
                return RedirectToAction("IniciarSesion", "IniciarSesion");
            }
            else
            {
                return View();
            }
        }

        public String insertarPersona(MdlRegistro persona)
        {            
            var secuenciaIdPer = 0;            
            var countPer = _context.TblPersonas.Count();
            var query = "";
            
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

            if (!validarPersona(persona))
            {
                TempData["Mensaje"] = "Debe completar los campos requeridos (*)";
            }
            else
            {
                try
                {
                    query = "insert into p_buses.dbo.tbl_personas(id_persona, id_metodo_pago, numero_identificacion, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido) values('" + secuenciaIdPer + "', '" + persona.personas.IdMetodoPago + "', '" + persona.personas.NumeroIdentificacion + "', '" + persona.personas.PrimerNombre + "', '" + persona.personas.SegundoNombre + "', '" + persona.personas.PrimerApellido + "', '" + persona.personas.SegundoApellido + "');";

                    using (SqlConnection sqlConn = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
                    {
                        using (SqlCommand com = new SqlCommand(query, sqlConn))
                        {
                            sqlConn.Open();
                            com.ExecuteNonQuery();
                            sqlConn.Close();
                        }
                    }
                } catch (Exception ex)
                {
                    TempData["Mensaje"] = ex.ToString();
                }                
            }                     

            //Se setea el id de la persona para crear el usuario
            return secuenciaIdPer.ToString();  
        }

        public bool validarPersona(MdlRegistro persona)
        {
            if (persona.personas.NumeroIdentificacion == null || persona.personas.PrimerNombre == null || persona.personas.PrimerApellido == null || persona.personas.SegundoApellido == null)
            {
                return false;
            } else
            {
                return true;
            }           
        }

        public bool validarUsuario(MdlRegistro usuario)
        {
            if (usuario.usuarios.Email == null || usuario.usuarios.Contrasena == null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public bool insertarUsuario(MdlRegistro usuario, String IdPersona)
        {
            var identificadorPersona = "";
            var secuenciaIdUser = 0;
            var flag = false;
            var query = "";
            var correo = 0;

            var countUser = _context.TblUsuarios.Count();

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

            //Se obtiene el id de la persona
            identificadorPersona = IdPersona;

            if (!validarUsuario(usuario))
            {
                TempData["Mensaje"] = "Debe completar el usuario y la contraseña"; 
            }
            else
            {
                try
                    {
                        query = "insert into p_buses.dbo.tbl_usuarios(id_usuario, id_persona, email, contrasena, estado) values('" + secuenciaIdUser + "', '" + identificadorPersona + "', '" + usuario.usuarios.Email + "', '" + usuario.usuarios.Contrasena + "', 'A');";

                        using (SqlConnection sqlConn = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
                        {
                            using (SqlCommand com = new SqlCommand(query, sqlConn))
                            {
                                sqlConn.Open();
                                com.ExecuteNonQuery();
                                sqlConn.Close();
                            }
                        }

                        TempData["MensajeExito"] = "Registro creado con éxito";
                        flag = true;
                    } catch (Exception ex)
                    {
                        TempData["Mensaje"] = ex.ToString();
                        flag = false;
                    }                
            }
            return flag;
        }
    }
}

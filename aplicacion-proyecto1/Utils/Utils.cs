using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography;
using System.Text;

namespace aplicacion_proyecto1.Utils
{
    public class Utils
    {
        public static string Encriptar(string contrasena)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] resultado = hash.ComputeHash(enc.GetBytes(contrasena));

                foreach (byte i in resultado)
                    sb.Append(i.ToString("x2"));

            }

                return sb.ToString();
        }        
    }
}

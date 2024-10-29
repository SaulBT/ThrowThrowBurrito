using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class Utilidades
    {
        public static string GenerarCodigo(int longitud)
        {
            Random random = new Random();
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            StringBuilder resultado = new StringBuilder(longitud);
            for (int i = 0; i < longitud; i++)
            {
                resultado.Append(caracteres[random.Next(caracteres.Length)]);
            }
            return resultado.ToString();
        }
    }
}

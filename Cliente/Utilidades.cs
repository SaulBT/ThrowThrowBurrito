using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class Utilidades
    {
        public static string encriptar(string contrasenia)
        {
            SHA256Managed sHA256Managed = new SHA256Managed();
            string contraHash = String.Empty;
            byte[] contraByte = sHA256Managed.ComputeHash(Encoding.UTF8.GetBytes(contrasenia));

            foreach (byte b in contraByte)
            {
                contraHash += b.ToString("x2");
            }

            return contraHash;
        }
    }
}

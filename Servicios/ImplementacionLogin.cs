using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    internal class ImplementacionLogin : IServicioLogin
    {
        public bool Login(string nombreUsuario, string contrasenia)
        {
            using (var contexto = new ModeloDBContainer())
            {

            }
        }
    }
}

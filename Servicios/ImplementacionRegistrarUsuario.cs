using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using AccesoDatos;

namespace Servicios
{
    public class ImplementacionRegistrarUsuario : IServicioRegistrarUsuario
    {
        public bool EnviarCodigoCorreo(string correo)
        {
            /*
             * Función que envía el código al correo ingresado
             * debería hacer una validación para que no se haga spam
             */
            Console.WriteLine("Se envía el código al correo: " + correo);

            return true;
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            /*
             * Función que guarda el usuario en la base de datos
             */
            Console.WriteLine("añadiendo al usuario con nombre: " + usuario.NombreUsuario + " y contraseña: " + usuario.Contrasenia + "...");
            

            return true;

        }

        public bool ValidarCorreo(string codigoCorreo)
        {
            /*
             * Función que valida si el código ingresado
             * es el mismo enviado al correo
             */
            Console.WriteLine("Se valida que el código ingresado sí es el mismo que el enviado al correo");

            return true;
        }

        public bool ValidarDatos(Usuario usuario)
        {
            /*
             * Función que valida si los datos nombre de usuario, contrasenia
             * correo cumplen el formato y si el correo existe
             */
            Console.WriteLine("Se valida que los datos ingresados cumplen formato y el correo existe");

            return true;
        }

    }
}

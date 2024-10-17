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
        const int longitudCodigo = 6;
        public string EnviarCodigoCorreo(string correo)
        {
            /*
             * Función que envía el código al correo ingresado
             * debería hacer una validación para que no se haga spam
             * genera el codigo al momento y lo devuelve 
             */

            string codigo = GenerarCodigo(longitudCodigo);
            Console.WriteLine("Se envía el código " + codigo + " al correo: " + correo);
            
            return codigo;
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            /*
             * Función que guarda el usuario en la base de datos
             */
            Console.WriteLine("añadiendo al usuario con nombre: " + usuario.NombreUsuario + " y contraseña: " + usuario.Contrasenia + "...");
            

            return true;

        }


        public bool ValidarDatos(Usuario usuario)
        {
            /*
             * Función que valida si los datos nombre de usuario, contrasenia
             * correo cumplen el formato y si el correo existe
             */
            if (NombreCumpleFormato(usuario.NombreUsuario) && ContraseniaCumpleFormato(usuario.Contrasenia) && CorreoExiste(usuario.Correo)) {
                Console.WriteLine("Todos los datos son validos");
                return true;
            } else
            {
                Console.WriteLine("Los datos no son validos");
                return false;
            }
        }

        //Métodos no pertenecientes a la interfaz:

        public bool CorreoExiste(string correo)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(correo);
                if (addr.Address == correo)
                {
                    Console.WriteLine("El correo sí existe");
                    return true;
                }
                else
                {
                    Console.WriteLine("El correo no existe");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Error al verificar si el correo existe...");
                return false;
            }
        }
        public bool NombreCumpleFormato(string nombre)
        {
            if ((nombre.Length >= 3) && (nombre.Length <= 20))
            {
                foreach (char c in nombre)
                {
                    if (!char.IsLetterOrDigit(c) && c != '-' && c != '.')
                    {
                        Console.WriteLine("El nombre contiene símbolos no permitidos");
                        return false;
                    }
                }
                Console.WriteLine("Nombre valido");
                return true;
            }
            else
            {
                Console.WriteLine("El nombre es mayor a 20 caracteres o menor a 3");
                return false;
            }
        }

        public bool ContraseniaCumpleFormato(string contrasenia)
        {
            if ((contrasenia.Length >= 8) && (contrasenia.Length <= 100))
            {
                int contNumero = 0, contMayuscula = 0, contMinuscula = 0;
                foreach (char c in contrasenia)
                {
                    if (char.IsNumber(c))
                    {
                        contNumero++;
                    }
                    else if (char.IsUpper(c))
                    {
                        contMayuscula++;
                    }
                    else if (char.IsLower(c))
                    {
                        contMinuscula++;
                    }
                }
                if (contNumero > 0 && contMayuscula > 0 && contMinuscula > 0)
                {
                    Console.WriteLine("Contraseña valida");
                    return true;
                }
                else
                {
                    Console.WriteLine("La contraseña no contiene los parametros de mayuscula, minuscula y numero");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("La contraseña no debe ser menor a 8 caracteres ni mayor a 100");
                return false;
            }
        }

        public string GenerarCodigo(int longitud) {
            Random random = new Random();
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            
            StringBuilder resultado = new StringBuilder(longitud);
            for (int i = 0; i < longitud; i++) {
                resultado.Append(caracteres[random.Next(caracteres.Length)]);
            }
            return resultado.ToString();
        }

    }
}

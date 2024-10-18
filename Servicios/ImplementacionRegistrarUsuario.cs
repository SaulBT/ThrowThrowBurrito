using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using AccesoDatos;

namespace Servicios
{
    
    public class ImplementacionRegistrarUsuario : IServicioRegistrarUsuario
    {
        const int longitudCodigo = 6;
        const int longitudClaveJugador = 10;
        const string emailJuego = "Luispablolagunesnoriega@gmail.com";
        const string contraseniaEmail = "sfad yvzo rpwn ubyd";
        const string aliasJuego = "Throw Throw Burrito Game";
        const string asuntoCorreo = "Código de verificación - Registro de usuario";
        const string cuerpoCorreo = "Se ha solicitado el registro de un usuario bajo esta dirección de correo.\n" +
            "Si usted no lo has solicitado, por favor ignore este mensaje\n\n" +
            "\tCódigo de verificación: ";


        public string EnviarCodigoCorreo(string correo)
        {
            /*
             * Función que envía el código al correo ingresado
             * debería hacer una validación para que no se haga spam
             * genera el codigo al momento y lo devuelve 
             */

            string codigo = GenerarCodigo(longitudCodigo);

            //Creando el correo
            MailMessage correoCodigo = new MailMessage();
            correoCodigo.From = new MailAddress(emailJuego, aliasJuego, System.Text.Encoding.UTF8);
            correoCodigo.To.Add(correo);
            correoCodigo.Subject = asuntoCorreo;
            correoCodigo.Body = cuerpoCorreo + codigo;
            correoCodigo.Priority = MailPriority.Normal;

            //enviando el correo
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 25;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Credentials = new System.Net.NetworkCredential(emailJuego, contraseniaEmail);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                {
                    return true;
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(correoCodigo);
                Console.WriteLine("Se envía el código " + codigo + " al correo: " + correo);
                //MessageBox.Show("Enviado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return codigo;
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            Console.WriteLine("añadiendo al usuario con nombre: " + usuario.NombreUsuario + " y contraseña: " + usuario.Contrasenia + "...");
            try
            {
                Console.WriteLine("\nCheckPoint 1\n");
                
                using (var contexto = new ModeloDBContainer())
                {
                    Console.WriteLine("\nCheckPoint 2\n");
                    contexto.Database.Log = Console.WriteLine;
                    Console.WriteLine("\nCheckPoint 3\n");
                    var jugador = new Jugador
                    {
                        nombreUsuario = usuario.NombreUsuario,
                        contrasenia = usuario.Contrasenia,
                        correoElectronico = usuario.Correo,
                        claveUsuario = GenerarCodigo(longitudClaveJugador),
                        descripcion = null,
                        fotoPerfil = null,
                        estado = null,
                        esInvitado = false
                    };
                    Console.WriteLine("\nCheckPoint 4\n");

                    contexto.Jugador.Add(jugador);
                    Console.WriteLine("\nCheckPoint 5\n");
                    contexto.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de sql: " + ex.ToString());
            }

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

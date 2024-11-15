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
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using AccesoDatos;
using Servicios.Interfaces;

namespace Servicios.Implementaciones
{
    
    public class ImplementacionRegistrarUsuario : IServicioRegistrarUsuario
    {
        const int LONGITUD_CODIGO = 6;
        const int LONGITUD_CLAVE_JUGADOR = 10;
        const string EMAIL_JUEGO = "Luispablolagunesnoriega@gmail.com";
        const string CONTRASENIA_EMAIL = "sfad yvzo rpwn ubyd";
        const string ALIAS_JUEGO = "Throw Throw Burrito Game";
        const string ASUNTO_CORREO = "Código de verificación - Registro de usuario";
        const string CUERPO_CORREO = "Se ha solicitado el registro de un usuario bajo esta dirección de correo.\n" +
            "Si usted no lo has solicitado, por favor ignore este mensaje\n\n" +
            "\tCódigo de verificación: ";
        private ModeloDBContainer _contexto;
        public ImplementacionRegistrarUsuario() { }
        public ImplementacionRegistrarUsuario(ModeloDBContainer contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public string EnviarCodigoCorreo(string correo)
        {
            string codigo = Utilidades.GenerarCodigo(LONGITUD_CODIGO);
            MailMessage correoCodigo = new MailMessage();
            correoCodigo.From = new MailAddress(EMAIL_JUEGO, ALIAS_JUEGO, System.Text.Encoding.UTF8);
            correoCodigo.To.Add(correo);
            correoCodigo.Subject = ASUNTO_CORREO;
            correoCodigo.Body = CUERPO_CORREO + codigo;
            correoCodigo.Priority = MailPriority.Normal;
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 25;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Credentials = new System.Net.NetworkCredential(EMAIL_JUEGO, CONTRASENIA_EMAIL);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                {
                    return true;
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(correoCodigo);
                Console.WriteLine("Se envía el código " + codigo + " al correo: " + correo);
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
                using (var contexto = new ModeloDBContainer())
                {
                    contexto.Database.Log = Console.WriteLine;
                    var jugador = new Jugador
                    {
                        nombreUsuario = usuario.NombreUsuario,
                        contrasenia = usuario.Contrasenia,
                        correoElectronico = usuario.Correo,
                        claveUsuario = GenerarClaveUsuario(),
                        descripcion = null,
                        fotoPerfil = null,
                        estado = null,
                        esInvitado = false
                    };
                    contexto.Jugador.Add(jugador);
                    contexto.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de sql: " + ex.ToString());
            }

            return true;

        }

        public string GenerarClaveUsuario()
        {
            string clave;
            do
            {
                clave = Utilidades.GenerarCodigo(LONGITUD_CLAVE_JUGADOR);
            } while (!ValidarClaveNoRepetida(clave));
            return clave;
            
        }
        public bool ValidarNombreNoRepetido(string nombre)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = (from j in contexto.Jugador
                               where j.nombreUsuario == nombre
                               select j).FirstOrDefault();
                if (jugador == null)
                {
                    Console.WriteLine("Jugador es nulo");
                    return true;
                }
                else
                {
                    Console.WriteLine("Jugador no es nulo");
                    return false;
                }

            }
        }

        public bool ValidarClaveNoRepetida(string clave)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = (from j in contexto.Jugador
                               where j.claveUsuario == clave
                               select j).FirstOrDefault();
                
                if (jugador == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool ValidarCorreoNoRepetido(string correo)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = (from j in contexto.Jugador
                               where j.correoElectronico == correo
                               select j).FirstOrDefault();

                if (jugador == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}

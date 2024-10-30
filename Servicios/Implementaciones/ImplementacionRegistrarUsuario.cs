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
        private ModeloDBContainer _contexto;
        public ImplementacionRegistrarUsuario() { }
        public ImplementacionRegistrarUsuario(ModeloDBContainer contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public string EnviarCodigoCorreo(string correo)
        {
            string codigo = Utilidades.GenerarCodigo(longitudCodigo);
            MailMessage correoCodigo = new MailMessage();
            correoCodigo.From = new MailAddress(emailJuego, aliasJuego, System.Text.Encoding.UTF8);
            correoCodigo.To.Add(correo);
            correoCodigo.Subject = asuntoCorreo;
            correoCodigo.Body = cuerpoCorreo + codigo;
            correoCodigo.Priority = MailPriority.Normal;
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
                clave = Utilidades.GenerarCodigo(longitudClaveJugador);
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
    }
}

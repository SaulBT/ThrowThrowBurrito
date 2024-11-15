using AccesoDatos;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.ServiceModel;
using System.ComponentModel.Design;

namespace Servicios.Implementaciones
{
    public class ImplementacionGestionUsuarios : IServicioRegistrarUsuario, IServicioPersonalizarPerfil, IServicioLogin, IServicioCambiarContrasenia
    {
        const int LONGITUD_CODIGO = 6;
        const int LONGITUD_CLAVE_JUGADOR = 10;
        const string EMAIL_JUEGO = "Luispablolagunesnoriega@gmail.com";
        const string CONTRASENIA_EMAIL = "sfad yvzo rpwn ubyd";
        const string ALIAS_JUEGO = "Throw Throw Burrito Game";
        const string ASUNTO_CORREO_REGISTRO = "Código de verificación - Registro de usuario";
        const string CUERPO_CORREO_REGISTRO = "Se ha solicitado el registro de un usuario bajo esta dirección de correo.\n" +
            "Si usted no lo has solicitado, por favor ignore este mensaje\n\n" +
            "\tCódigo de verificación: ";
        const string ASUNTO_CORREO_RECUPERACION = "Código de recuperación - Recuperar contraseña";
        const string CUERPO_CORREO_RECUPERACION = "Se ha solicitado la recuperación de la contraseña asociada al usuario bajo esta dirección de correo\n" +
            "Si usted no lo has solicitado, por favor ignore este mensaje\n\n" +
            "\tCódigo de recuperación: ";

        /*
         * Servicio RegistrarUsuario
        */

        public string EnviarCodigoCorreo(string correo)
        {
            string codigo = Utilidades.GenerarCodigo(LONGITUD_CODIGO);
            MailMessage correoCodigo = new MailMessage();
            correoCodigo.From = new MailAddress(EMAIL_JUEGO, ALIAS_JUEGO, System.Text.Encoding.UTF8);
            correoCodigo.To.Add(correo);
            correoCodigo.Subject = ASUNTO_CORREO_REGISTRO;
            correoCodigo.Body = CUERPO_CORREO_REGISTRO + codigo;
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
                Console.WriteLine("Se envía el código de registro " + codigo + " al correo: " + correo);
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

        /*
         * Servicio PersonalizarPerfil
        */

        public bool GuardarCambios(Perfil perfil, string claveUsuario)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Configuration.ProxyCreationEnabled = false;
                contexto.Database.Log = Console.WriteLine;
                var jugador = contexto.Jugador.FirstOrDefault(c => c.claveUsuario == claveUsuario);
                if (jugador != null)
                {
                    jugador.nombreUsuario = perfil.NombreUsuario;
                    jugador.descripcion = perfil.Descripcion;
                    jugador.fotoPerfil = perfil.Foto;

                    contexto.Entry(jugador).State = EntityState.Modified;
                    contexto.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * Servicio Login
        */

        public Jugador Login(string nombreUsuario, string contrasenia)
        {
            try
            {
                Jugador jugador = null;
                jugador = AccesoDatos.DAOJugador.buscarJugador(nombreUsuario, contrasenia);

                return jugador;
            }

            catch (EntityException ex)
            {
                throw new FaultException("Error de la base de datos");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new FaultException("Error de la base de datos");
            }
        }

        /*
         *  Servicio CambiarContrasenia
         */

        public bool CambiarContrasenia(string contrasenia, string correo)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Configuration.ProxyCreationEnabled = false;
                contexto.Database.Log = Console.WriteLine;
                var jugador = contexto.Jugador.FirstOrDefault(c => c.correoElectronico == correo);
                if (jugador != null)
                {
                    jugador.contrasenia = contrasenia;

                    contexto.Entry(jugador).State = EntityState.Modified;
                    contexto.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string EnviarCorreoRecuperacion(string correo)
        {
            string codigo = Utilidades.GenerarCodigo(LONGITUD_CODIGO);
            MailMessage correoCodigo = new MailMessage();
            correoCodigo.From = new MailAddress(EMAIL_JUEGO, ALIAS_JUEGO, System.Text.Encoding.UTF8);
            correoCodigo.To.Add(correo);
            correoCodigo.Subject = ASUNTO_CORREO_RECUPERACION;
            correoCodigo.Body = CUERPO_CORREO_RECUPERACION + codigo;
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
                Console.WriteLine("Se envía el código de recuperacion " + codigo + " al correo: " + correo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return codigo;
        }

        public string VerificarExistenciaUsuario(string nombreOCorreo)
        {
            using (var contexto = new ModeloDBContainer())
            {
                if (nombreOCorreo.Length > 20)
                {
                    string correo = (from j in contexto.Jugador
                                     where j.correoElectronico == nombreOCorreo
                                     select j.correoElectronico).FirstOrDefault();
                    if (correo != null)
                    {
                        return correo;
                    }
                }
                else
                {
                    string correo = (from j in contexto.Jugador
                                     where j.correoElectronico == nombreOCorreo || j.nombreUsuario == nombreOCorreo
                                     select j.correoElectronico).FirstOrDefault();
                    if (correo != null)
                    {
                        return correo;
                    }
                }
            }
            return null;
        }
    }
}

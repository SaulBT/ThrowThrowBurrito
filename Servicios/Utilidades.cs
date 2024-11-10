using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class Utilidades
    {
        const String CARACTERES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        const string EMAIL_JUEGO = "Luispablolagunesnoriega@gmail.com";
        const string CONTRASENIA_EMAIL = "sfad yvzo rpwn ubyd";
        const string ALIAS_JUEGO = "Throw Throw Burrito Game";

        public static string GenerarCodigo(int longitud)
        {
            Random random = new Random();

            StringBuilder resultado = new StringBuilder(longitud);
            for (int i = 0; i < longitud; i++)
            {
                resultado.Append(CARACTERES[random.Next(CARACTERES.Length)]);
            }
            return resultado.ToString();
        }

        public static void EnviarCorreo(string asunto, string cuerpo, string correo)
        {
            MailMessage correoEnviado = new MailMessage();
            correoEnviado.From = new MailAddress(EMAIL_JUEGO, ALIAS_JUEGO, System.Text.Encoding.UTF8);
            correoEnviado.To.Add(correo);
            correoEnviado.Subject = asunto;
            correoEnviado.Body = cuerpo;
            correoEnviado.Priority = MailPriority.Normal;

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
            smtpClient.Send(correoEnviado);
        }
    }
}

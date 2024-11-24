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
using System.Runtime.Remoting.Contexts;

namespace Servicios.Implementaciones
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ImplementacionGestionUsuarios : IServicioRegistrarUsuario, IServicioPersonalizarPerfil, IServicioLogin, IServicioCambiarContrasenia, IServicioAmigos, IServicioSolicitudes
    {
        const int LONGITUD_CODIGO = 6;
        const int LONGITUD_CLAVE_JUGADOR = 10;
        const string ASUNTO_CORREO_REGISTRO = "Código de verificación - Registro de usuario";
        const string CUERPO_CORREO_REGISTRO = "Se ha solicitado el registro de un usuario bajo esta dirección de correo.\n" +
            "Si usted no lo has solicitado, por favor ignore este mensaje\n\n" +
            "\tCódigo de verificación: ";
        const string ASUNTO_CORREO_RECUPERACION = "Código de recuperación - Recuperar contraseña";
        const string CUERPO_CORREO_RECUPERACION = "Se ha solicitado la recuperación de la contraseña asociada al usuario bajo esta dirección de correo\n" +
            "Si usted no lo has solicitado, por favor ignore este mensaje\n\n" +
            "\tCódigo de recuperación: ";
        const string ASUNTO_CORREO_INVITACION = "¡Un amigo te está invitando a una partida!";
        const string CUERPO_CORREO_INVITACION = " te está invitando a que te unas a su partida. ¿Qué esperas para unirte?\nCódigo de la partida:\n\n";

        private static readonly RepositorioCallbacks<IServicioSolicitudesCallback> repositorioSolicitudes = new RepositorioCallbacks<IServicioSolicitudesCallback>();
        private Dictionary<int, IServicioSolicitudesCallback> clientesSolicitudes = new Dictionary<int, IServicioSolicitudesCallback>();



        /*
         * Servicio RegistrarUsuario
        */

        public string EnviarCodigoCorreo(string correo)
        {
            string codigo = Utilidades.GenerarCodigo(LONGITUD_CODIGO);
            try
            {
                Utilidades.EnviarCorreo(correo, ASUNTO_CORREO_REGISTRO, CUERPO_CORREO_REGISTRO + codigo);
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
            Jugador jugador = new Jugador
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
            try
            {
                DAOJugador.CrearJugador(jugador);
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
            Jugador jugador = DAOJugador.ObtenerJugadorPorNombre(nombre);

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

        public bool ValidarClaveNoRepetida(string clave)
        {
            Jugador jugador = DAOJugador.ObtenerJugadorPorClave(clave);

            if (jugador == null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool ValidarCorreoNoRepetido(string correo)
        {
            Jugador jugador = DAOJugador.ObtenerJugadorPorCorreo(correo);
            if (jugador != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*
         * Servicio PersonalizarPerfil
        */

        public bool GuardarCambios(Perfil perfil, string claveUsuario)
        {
            Jugador jugador = DAOJugador.ObtenerJugadorPorClave(claveUsuario);

            if (jugador != null)
            {
                jugador.nombreUsuario = perfil.NombreUsuario;
                jugador.descripcion = perfil.Descripcion;
                jugador.fotoPerfil = perfil.Foto;
                DAOJugador.ActualizarJugador(jugador);
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * Servicio Login
        */

        public Jugador Login(string nombreUsuario, string contrasenia)
        {
            Jugador jugador = null;
            jugador = AccesoDatos.DAOJugador.ObtenerJugadorPorNombreContrasenia(nombreUsuario, contrasenia);

            return jugador;
        }

        /*
         *  Servicio CambiarContrasenia
         */

        public bool CambiarContrasenia(string contrasenia, string correo)
        {
            Jugador jugador = DAOJugador.ObtenerJugadorPorCorreo(correo);
            if (jugador != null)
            {
                jugador.contrasenia = contrasenia;
                DAOJugador.ActualizarJugador(jugador);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string EnviarCorreoRecuperacion(string correo)
        {
            string codigo = Utilidades.GenerarCodigo(LONGITUD_CODIGO);
            try
            {
                Utilidades.EnviarCorreo(correo, ASUNTO_CORREO_RECUPERACION, CUERPO_CORREO_RECUPERACION + codigo);
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
            Jugador jugador = DAOJugador.ObtenerJugadorPorCorreo(nombreOCorreo);
            if (jugador != null)
            {
                return jugador.correoElectronico;
            }
            jugador = DAOJugador.ObtenerJugadorPorNombre(nombreOCorreo);
            if (jugador != null)
            {
                return jugador.correoElectronico;
            }

            return null;
        }

        /*
         * Servicio de solicitudes
        */
        public void EnviarCliente(int idJugador)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IServicioSolicitudesCallback>();
            var canal = (ICommunicationObject)callback;

            repositorioSolicitudes.AgregarCallback(canal, callback);
        }

        public int EnviarSolicitud(string claveJugadorReceptor, int idJugador)
        {
            int respuesta;
            int idJugadorRemitente = obtenerJugadorRemitente(claveJugadorReceptor);
            if (idJugadorRemitente > 0)
            {
                if (!determinarBloqueo(idJugador, idJugadorRemitente) && !determinarAmistad(idJugador, idJugadorRemitente))
                {
                    Amigo solicitud = new Amigo
                    {
                        idJugadorEmisor = idJugador,
                        idJugadorReceptor = idJugadorRemitente,
                        estado = "Pendiente",
                    };
                    DAOAmigo.CargarAmigo(solicitud);
                    HacerLlegarSolicitud(solicitud);

                    respuesta = 0;
                }
                else
                {
                    respuesta = 1;
                }
            }
            else
            {
                respuesta = 2;
            }

            return respuesta;
        }

        private void HacerLlegarSolicitud(Amigo solicitud)
        {
            if (clientesSolicitudes.ContainsKey(solicitud.idJugadorReceptor))
            {
                Jugador receptor = DAOJugador.ObtenerSolicitud(solicitud.idJugadorEmisor);
                clientesSolicitudes[solicitud.idJugadorReceptor].ObtenerNuevaSolicitud(receptor);
            }
            
        }

        private int obtenerJugadorRemitente(string claveJugadorRemitente)
        {
            Jugador jugador = DAOJugador.ObtenerJugadorPorClave(claveJugadorRemitente);
            return jugador.idJugador;
        }

        private bool determinarBloqueo(int idJugadorEmisor, int idJugadorReceptor)
        {
            Bloqueado bloqueado = DAOBloqueado.ObtenerBloqueo(idJugadorEmisor, idJugadorReceptor);
            if (bloqueado == null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        private bool determinarAmistad(int idJugadorEmisor, int idJugadorReceptor)
        {
            Amigo amigo = DAOAmigo.ObtenerAmigo(idJugadorEmisor, idJugadorReceptor);
            if (amigo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Jugador[] RecibirSolicitudes(int idJugador)
        {
            Jugador[] solicitudes = DAOAmigo.ObtenerSolicitudes(idJugador);
            if (solicitudes != null)
            {
                Console.WriteLine("Hay " + solicitudes.ToArray().Length + " solicitudes en el servidor.");
                return solicitudes;
            }
            else
            {
                return null;
            }
        }

        public void AceptarSolicitud(int idJugadorEmisor, int idJugadorReceptor)
        {
            Amigo solicitud = DAOAmigo.ObtenerAmigo(idJugadorEmisor, idJugadorReceptor);
            solicitud.estado = "Aceptada";
            DAOAmigo.AceptarSolicitud(solicitud);
            Jugador amigoEmisor = DAOJugador.ObtenerAmigo(idJugadorEmisor);
            Jugador amigoReceptor = DAOJugador.ObtenerAmigo(idJugadorReceptor);
            hacerLlegarAmigo(amigoEmisor, idJugadorReceptor);
            hacerLlegarAmigo(amigoReceptor, idJugadorEmisor);
        }

        private void hacerLlegarAmigo(Jugador amigo, int idJugador)
        {
            if (clientesSolicitudes.ContainsKey(idJugador))
            {
                clientesSolicitudes[idJugador].ObtenerAmigoNuevo(amigo);
            }
        }

        public void RechazarSolicitud(int idJugadorEmisor, int idJugadorReceptor)
        {
            Amigo solicitud = DAOAmigo.ObtenerAmigo(idJugadorEmisor, idJugadorReceptor);
            DAOAmigo.RechazarSolicitud(solicitud.idAmigo);
        }

        public void EnviarInvitacion(string codigoPartida, string correoJugadorInvitado, string nombreUsuarioInvitador)
        {
            string cuerpo = nombreUsuarioInvitador + CUERPO_CORREO_INVITACION + codigoPartida;
            Utilidades.EnviarCorreo(correoJugadorInvitado, ASUNTO_CORREO_INVITACION, cuerpo);
        }

        /*
         * Servicio amigos
        */

        public Jugador[] CargarAmigos(int idJugador)
        {
            Jugador[] amigos = DAOAmigo.ObtenerAmigos(idJugador);
            if (amigos != null)
            {
                return amigos;
            }
            else
            {
                return null;
            }
        }

        public void EliminarAmigo(int idJugadorEmisor, int idJugadorReceptor)
        {
            Console.WriteLine("El jugador id:" + idJugadorEmisor + " elimina al jugador id:" + idJugadorReceptor);
            Amigo amigo = DAOAmigo.ObtenerAmigo(idJugadorEmisor, idJugadorReceptor);
            DAOAmigo.EliminarAmigo(amigo.idAmigo);
            hacerLlegarEliminacion(idJugadorEmisor, idJugadorReceptor);
        }

        private void hacerLlegarEliminacion(int idJugadorEmisor, int idJugadorReceptor)
        {
            if (clientesSolicitudes.ContainsKey(idJugadorReceptor))
            {
                Console.WriteLine("El jugador id:" + idJugadorReceptor + " está conectado. Enviando la actualización de la eliminación.");
                clientesSolicitudes[idJugadorReceptor].EnviarEliminacionAmigo(idJugadorEmisor);
            }   
        }

        public int BloquearJugador(int idJugadorEmisor, string claveJugadorReceptor)
        {
            Console.WriteLine("Iniciando proceso de bloqueado");
            int respuesta;
            int idJugadorReceptor = obtenerJugadorRemitente(claveJugadorReceptor);
            if (idJugadorReceptor > 0)
            {
                if (!determinarBloqueo(idJugadorEmisor, idJugadorReceptor))
                {
                    Bloqueado bloqueado = new Bloqueado
                    {
                        idJugadorEmisor = idJugadorEmisor,
                        idJugadorReceptor = idJugadorReceptor,
                    };
                    DAOBloqueado.Bloquear(bloqueado);

                    if (determinarAmistad(idJugadorEmisor, idJugadorReceptor))
                    {
                        DAOAmigo.EliminarAmigo(DAOAmigo.ObtenerAmigo(idJugadorEmisor, idJugadorReceptor).idAmigo);
                        hacerLlegarEliminacion(idJugadorEmisor, idJugadorReceptor);
                        hacerLlegarEliminacion(idJugadorReceptor, idJugadorEmisor);
                    }

                    //hacerLlegarBloqueo(idJugadorReceptor, idJugadorEmisor);

                    respuesta = 0;
                } else
                {
                    respuesta = 1;
                }
            }
            else
            {
                respuesta = 2;
            }

            return respuesta;
        }

        [OperationBehavior]
        private void hacerLlegarBloqueo(int idJugadorBloqueado, int idJugadorEmisor)
        {
            Jugador bloqueado = DAOJugador.ObtenerBloqueado(idJugadorBloqueado);
            if (bloqueado.idJugador != -1)
            {
                if (clientesSolicitudes.ContainsKey(idJugadorEmisor))
                {
                    clientesSolicitudes[idJugadorEmisor].ObtenerNuevoBloqueo(bloqueado);
                }
            }
        }

        public void DesbloquearJugador(int idJugadorEmisor, int idJugadorRemitente)
        {
            DAOBloqueado.Desbloquear(DAOBloqueado.ObtenerBloqueado(idJugadorEmisor, idJugadorRemitente).idBloqueado);
        }

        public Jugador[] CargarBloqueados(int idJugadorEmisor)
        {
            Jugador[] bloqueados = DAOBloqueado.ObtenerJugadoresBloqueados(idJugadorEmisor);
            if (bloqueados != null)
            {
                return bloqueados;
            }
            else
            {
                return null;
            }
        }
    }
}

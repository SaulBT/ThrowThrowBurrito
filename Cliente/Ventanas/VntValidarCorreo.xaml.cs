using Cliente.Logica;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Cliente.ServiciosGestionUsuarios;
using static Cliente.Ventanas.VntRegistrarUsuario;

namespace Cliente.Ventanas
{
    public partial class VntValidarCorreo : Page
    {
        private string codigo;
        private Usuario usuario;
        private ServicioRegistrarUsuarioClient servicio;
        private LogicaCrearJugador logica;
        private string contrasenia;
        private static Dictionary<string, DateTime> ultimaHoraDeEnvio = new Dictionary<string, DateTime>();
        private static TimeSpan tiempoEntreEnvios = TimeSpan.FromMinutes(1);

        public VntValidarCorreo(ParametrosNavegacion parametros)
        {
            try
            {
                InitializeComponent();
                logica = new LogicaCrearJugador();
                servicio = new ServicioRegistrarUsuarioClient();
                this.usuario = parametros.Usuario;
                contrasenia = usuario.Contrasenia;
                this.codigo = parametros.Codigo;
                ultimaHoraDeEnvio[usuario.Correo] = DateTime.Now;
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
        }

        private void btnReenviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txbCodigo.Text = "";
                if (ultimaHoraDeEnvio.ContainsKey(usuario.Correo) && ultimaHoraDeEnvio[usuario.Correo] + tiempoEntreEnvios < DateTime.Now)
                {
                    this.codigo = servicio.EnviarCodigoCorreo(usuario.Correo);
                    ultimaHoraDeEnvio[usuario.Correo] = DateTime.Now;
                    mostrarAlerta("Se ha enviado un nuevo código al correo proporcionado.");
                }
                else
                {
                    mostrarAlerta("Por favor, espere 1 minuto antes de volver a enviar un código al correo ingresado.");
                }
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            } 
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txbCodigo.Text.Equals(this.codigo))
                {
                    logica.crearUsuario(usuario);   
                    mostrarAlerta("Se ha registrado el usuario exitosamente.");
                    
                }
                else
                {
                    mostrarAlerta("El codigo ingresado no coincide con el enviado al correo.");
                }
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            } 
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            servicio.Close();
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
            Console.WriteLine(servicio.State.ToString());
            if (servicio.State == CommunicationState.Closed)
            {
                NavigationService.GoBack();
            }
            if (txbCodigo.Text.Equals(this.codigo))
            {
                LogicaLogin logicaLogin = new LogicaLogin();
                vntMenuPrincipal vntMenuPrincipal = new vntMenuPrincipal(logicaLogin.IniciarSesion(usuario.NombreUsuario, contrasenia));
                NavigationService.Navigate(vntMenuPrincipal);
            }
        }

        private void mostrarAlerta(string mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }
    }
}

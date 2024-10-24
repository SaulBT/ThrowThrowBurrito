using Cliente.Logica;
using Cliente.ServicioChat;
using Cliente.ServicioLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.Ventanas
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class vntLobby : Page
    {
        private Jugador jugador;
        private LogicaChat logica;

        public vntLobby(Jugador jugador)
        {
            InitializeComponent();
            this.jugador = jugador;
            logica = new LogicaChat(this, jugador.nombreUsuario);
            Unirse();
            
        }

        public void Unirse()
        {
            try
            {
                logica.Unirse(this.jugador.nombreUsuario);
            }
            catch (FaultException ex)
            {
                mostrarAlerta(ex.Message);
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");

            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
            }
            
        }

        public void mostrarAlerta(String mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
        }

        public void RecibirMensaje(string mensajeCompleto)
        {
            string chatActual = txcChat.Text;
            if (!chatActual.Equals(""))
            {
                txcChat.Text = String.Concat(chatActual, "\n", mensajeCompleto);
            }
            else
            {
                txcChat.Text = mensajeCompleto;
            }
        }
        private void btEnviarMensaje_Click(object sender, RoutedEventArgs e)
        {
            if (!tbcMensaje.Text.Equals(""))
            {
                try
                {
                    string mensaje = tbcMensaje.Text;
                    logica.EnviarMensaje(mensaje);
                    tbcMensaje.Text = "";
                }
                catch (EndpointNotFoundException ex)
                {
                    mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                }
                catch (CommunicationException ex)
                {
                    mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");

                }
                catch (Exception ex)
                {
                    mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                }
            }
        }

        public void actualizarChat(string mensaje)
        {
            string chat = txcChat.Text;

            if (!chat.Equals(""))
            {
                txcChat.Text = String.Concat(chat, "\n", mensaje);
                
            }
            else
            {
                txcChat.Text = mensaje;
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            vntMenuPrincipal vntMenuPrincipal = new vntMenuPrincipal(jugador);
            NavigationService.Navigate(vntMenuPrincipal);
        }

        private void tbcMensaje_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

﻿using Cliente.Logica;
using Cliente.ServicioJuego;
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
        private ServicioLogin.Jugador jugador;
        private LogicaChat logica;
        private Partida partidaLocal;
        private DatosJugadorPartida[] datosJugadorPartida;
        private ServicioJuegoClient servicioJuegoClient;

        public vntLobby(ServicioLogin.Jugador jugador, Partida partida)
        {
            InitializeComponent();
            this.jugador = jugador;
            this.partidaLocal = partida;
            logica = new LogicaChat(this, jugador.nombreUsuario);

            InstanceContext contexto = new InstanceContext(this);
            servicioJuegoClient = new ServicioJuego.ServicioJuegoClient(contexto);
            datosJugadorPartida = servicioJuegoClient.RetornarDatosJugador(partida.codigoPartida);
            /*
             * hago el retorno de datos directamente en el lobby mientras que el retorno de de partida lo hago desde el menú
             * esto porque ahí mismo se crea la partida (evitando ir a una ventana aparte en caso de ocurra en error) pues 
             * dicho metodo devuelve ya el objeto de la partida, de otro modo tendría que crear la partida en esta misma ventana
             * y en caso de dar error se tendría que estar navegando entre más ventanas
             */


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

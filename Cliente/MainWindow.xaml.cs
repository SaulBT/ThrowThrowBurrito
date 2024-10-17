using Cliente.Logica;
using Cliente.ServicioLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace Cliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LogicaLogin logicaLogin = new LogicaLogin();
        private LogicaChat logicaChat;
        private Jugador jugador;
        public MainWindow()
        {
            InitializeComponent();
            logicaChat = new LogicaChat(this);
        }

        private void tbUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbContrasenia_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string nombreUsuario = tbUsuario.Text;
            string contrasenia = tbContrasenia.Text;

            try
            {
                bool exitoLogeo = logicaLogin.IniciarSesion(nombreUsuario, contrasenia);
                if (exitoLogeo)
                {
                    jugador = logicaLogin.Jugador;
                    activarChat();
                    lbPrueba.Content = String.Concat("¡Bienvenido ", nombreUsuario, " !");
                    btnConectarse.IsEnabled = true;
                } else
                {
                    lbPrueba.Content = "No se encontró al usuario.";
                }
            }
            catch (FaultException ex)
            {
                
            }
        }

        private void activarChat()
        {
            tbMensaje.IsEnabled = true;
            btnEnviar.IsEnabled = true;
        }

        private void btnConectarse_Click(object sender, RoutedEventArgs e)
        {
            logicaChat.Unirse(jugador.nombreUsuario);
        }

        private void tbMensaje_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {

        }

        public void actualizarChat(string mensaje)
        {
            string textoActual = tbcChat.Text;
            tbcChat.Text = String.Concat(textoActual, "/n", mensaje);
        }
    }
}

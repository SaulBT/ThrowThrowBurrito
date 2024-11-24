using Cliente.Logica;
using Cliente.ServiciosGestionUsuarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class vntListaAmigos : UserControl
    {
        private LogicaAmigos logica;
        private ObservableCollection<Jugador> amigos;
        public string codigoPartida { get; set; }
        public string nombreUsuarioEmisor { get; set; }
        public List<int> idJugadoresInvitados { get; set; }
        public vntListaAmigos()
        {
            InitializeComponent();
        }

        public void ConfigurarVentana(LogicaAmigos logica)
        {
            this.logica = logica;
            uscAgregarAmigo.AgregarLogica(logica);
            uscNotificaciones.AgregarLogica(logica);
            uscBloqueados.AgregarLogica(logica);

        }

        public void Mostrar()
        {
            this.Visibility = Visibility.Visible;
        }

        public void CargarListaAmigos(ObservableCollection<Jugador> amigos)
        {
            this.amigos = amigos;
            if (amigos != null)
            {
                AmigosListBox.ItemsSource = amigos;
            }
        }

        public void configurarNotificaciones(bool notificaciones, ObservableCollection<Jugador> solicitudes)
        {
            BitmapImage bitmap = new BitmapImage();
            if (notificaciones)
            {
                string rutaLlena = "pack://application:,,,/Imagenes/imgCampanaLlena.png";
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(rutaLlena, UriKind.RelativeOrAbsolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }
            else
            {
                string rutaLlena = "pack://application:,,,/Imagenes/imgCampanaVacia.png";
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(rutaLlena, UriKind.RelativeOrAbsolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }

            imgCampana.Source = bitmap;
            uscNotificaciones.AgregarNotificaciones(solicitudes);
        }

        private void btnInvitar_Click(object sender, RoutedEventArgs e)
        {
            Jugador jugadorSeleccionado = AmigosListBox.SelectedItem as Jugador;
            if (jugadorSeleccionado != null)
            {
                if (!idJugadoresInvitados.Contains(jugadorSeleccionado.idJugador))
                {
                    logica.EnviarInvitacion(codigoPartida, jugadorSeleccionado.correoElectronico, nombreUsuarioEmisor);
                    idJugadoresInvitados.Add(jugadorSeleccionado.idJugador);
                }
                else
                {
                    uscError.Mostrar("Ya has invitado a este jugador");
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Jugador amigoSeleccionado = (Jugador)AmigosListBox.SelectedItem;
            if (amigoSeleccionado != null)
            {
                logica.EliminarAmigo(amigoSeleccionado.idJugador);
                amigos.Remove(amigoSeleccionado);
            }
        }

        private void btnBloqueados_Click(object sender, RoutedEventArgs e)
        {
            uscBloqueados.Visibility = Visibility.Visible;
        }

        private void btnBuscarAmigo_Click(object sender, RoutedEventArgs e)
        {
            uscAgregarAmigo.Visibility = Visibility.Visible;
        }

        private void Campana_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            uscNotificaciones.Visibility = Visibility.Visible;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}

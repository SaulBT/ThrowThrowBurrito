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

namespace Cliente.Ventanas.VentanaEmergente
{
    
    public partial class vntNotificaciones : UserControl
    {
        private LogicaAmigos logica;
        private ObservableCollection<Jugador> solicitudes;
        public vntNotificaciones()
        {
            InitializeComponent();
        }

        public void AgregarLogica(LogicaAmigos logica)
        {
            this.logica = logica;
        }

        public void AgregarNotificaciones(ObservableCollection<Jugador> notificaciones)
        {
            if(notificaciones != null)
            {
                solicitudes = notificaciones;
                lbxSolicitudes.ItemsSource = solicitudes;
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Jugador solicitudSeleccionado = (Jugador)lbxSolicitudes.SelectedItem;
            if (solicitudSeleccionado != null)
            {
                logica.AceptarSolicitudAmistad(solicitudSeleccionado.idJugador);
                solicitudes.Remove(solicitudSeleccionado);
            }
        }

        private void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            Jugador solicitudSeleccionado = (Jugador)lbxSolicitudes.SelectedItem;
            if (solicitudSeleccionado != null)
            {
                logica.RechazarSolicitudAmistad(solicitudSeleccionado.idJugador);
                solicitudes.Remove(solicitudSeleccionado);
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}

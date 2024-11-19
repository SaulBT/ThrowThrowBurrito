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
    public partial class vntBloqueados : UserControl
    {
        LogicaAmigos logica;
        ObservableCollection<Jugador> bloqueados;
        public vntBloqueados()
        {
            InitializeComponent();
        }

        public void AgregarLogica(LogicaAmigos logica)
        {
            this.logica = logica;
            uscAgregarBloqueo.AgregarLogica(logica);
        }

        public void AgregarBloqueados(ObservableCollection<Jugador> bloqueados)
        {
            this.bloqueados = bloqueados;
            if (bloqueados.Count > 0)
            {
                lbxBloqueados.ItemsSource = bloqueados;
            }
        }

        private void btnDesbloquear_Click(object sender, RoutedEventArgs e)
        {
            Jugador bloqueoSeleccionado = (Jugador)lbxBloqueados.SelectedItem;
            if (bloqueoSeleccionado != null)
            {
                logica.DesbloquearJugador(bloqueoSeleccionado.idJugador);
                bloqueados.Remove(bloqueoSeleccionado);
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            uscAgregarBloqueo.Visibility = Visibility.Visible;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}

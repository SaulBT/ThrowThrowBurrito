using Cliente.Logica;
using System;
using System.Collections.Generic;
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
    public partial class vntBloquear : UserControl
    {
        LogicaAmigos logica;
        public vntBloquear()
        {
            InitializeComponent();
        }

        public void AgregarLogica(LogicaAmigos logica)
        {
            this.logica = logica;
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxCodigo.Text))
            {
                tbcErrorCodigo.Visibility = Visibility.Hidden;
                int respuesta = logica.BloquearJugador(tbxCodigo.Text);
                switch (respuesta)
                {
                    case 0:
                        uscEmergente.Mostrar("Jugador bloqueado con éxito.");
                        break;
                    case 1:
                        uscEmergente.Mostrar("Este jugador ya está bloqueado.");
                        break;
                    case 2:
                        uscEmergente.Mostrar("No se encontró a ningún jugador con ese código.");
                        break;
                }
            }
            else
            {
                tbcErrorCodigo.Visibility = Visibility.Visible;
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}

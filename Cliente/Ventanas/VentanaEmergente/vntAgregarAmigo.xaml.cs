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
    /// <summary>
    /// Interaction logic for vntErrorAmigo.xaml
    /// </summary>
    public partial class vntAgregarAmigo : UserControl
    {
        private LogicaAmigos logica;
        public vntAgregarAmigo()
        {
            InitializeComponent();
        }

        public void Mostrar()
        {
            this.Visibility = Visibility.Visible;
        }

        public void AgregarLogica(LogicaAmigos logica)
        {
            this.logica = logica;
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (logica.EnviarSolicitudAmistad(tbxCodigo.Text))
            {
                Console.WriteLine("Se envió la solicitud a " + tbxCodigo.Text);
            }
            else
            {
                Console.WriteLine("No se encontró al jugador");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}

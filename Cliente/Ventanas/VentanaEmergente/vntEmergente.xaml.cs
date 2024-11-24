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
    public partial class vntEmergente : UserControl
    {
        public vntEmergente()
        {
            InitializeComponent();
        }

        public void Mostrar(string texto)
        {
            tbcMensajeEmergente.Text = texto;
            this.Visibility = Visibility.Visible;
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}

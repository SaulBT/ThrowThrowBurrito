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
using System.Windows.Shapes;

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para VentanaPersonalizarPerfil.xaml
    /// </summary>
    public partial class VentanaPersonalizarPerfil : Window
    {
        public VentanaPersonalizarPerfil()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSubirFoto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

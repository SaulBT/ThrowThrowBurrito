using Cliente.ServicioPersonalizarPerfil;
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
        private string claveUsuario = null;
        public VentanaPersonalizarPerfil(string claveUsuario)
        {
            ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient Proxy = new ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient();
            Perfil perfil = Proxy.ObtenerPerfil(claveUsuario);
            tbDescripcion.Text = perfil.Descripcion;
            tbNombreUsuario.Text = perfil.NombreUsuario;
            this.claveUsuario = claveUsuario;
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
            ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient Proxy = new ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient();
            if (!string.IsNullOrEmpty(tbNombreUsuario.Text))
            {
                if (Proxy.GuardarCambios(Proxy.ObtenerPerfil(claveUsuario), claveUsuario))
                {
                    Console.WriteLine("Cambios guardados con éxito");
                } else
                {
                    Console.WriteLine("no se puedo guardar el cambio");

                }

            }
            /*
            Servidor.ServicioRegistrarUsuarioClient
            Servidor.ServicioRegistrarUsuarioClient Proxy = new Servidor.ServicioRegistrarUsuarioClient();
            */
        }

        private void btnSubirFoto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

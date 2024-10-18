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
    /// Lógica de interacción para VentanaRegistroUsuario.xaml
    /// </summary>
    public partial class VentanaRegistroUsuario : Window
    {
        public VentanaRegistroUsuario()
        {
            InitializeComponent();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            ServicioRegistrarUsuario.ServicioRegistrarUsuarioClient Proxy = new ServicioRegistrarUsuario.ServicioRegistrarUsuarioClient();

            //TODO: HACER VALIDACION DE QUE NO SEAN NULL
            if (!string.IsNullOrEmpty(tbNombreUsuario.Text) && !string.IsNullOrEmpty(tbContrasenia.Text) && !string.IsNullOrEmpty(tbCorreo.Text))
            {
                ServicioRegistrarUsuario.Usuario usuario = new ServicioRegistrarUsuario.Usuario()
                {
                    NombreUsuario = tbNombreUsuario.Text,
                    Contrasenia = tbContrasenia.Text,
                    Correo = tbCorreo.Text,
                };
                if (Proxy.ValidarDatos(usuario))
                {
                    VentanaValidarCorreo ventanaValidarCorreo = new VentanaValidarCorreo(Proxy.EnviarCodigoCorreo(usuario.Correo), usuario);
                    ventanaValidarCorreo.Show();
                    this.Close();
                }
                else
                {
                    Console.WriteLine("Formato incorrecto de datos");
                    return;
                }
            } else
            {
                Console.WriteLine("Campos vacíos");
                return;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
